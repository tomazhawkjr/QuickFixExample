using OrderReceiver.Domain.Interfaces.Services;
using OrderReceiver.Infra.Util;
using OrderLibrary.Domain.Interface.Services;
using QuickFix.Fields;
using QuickFix;
using Microsoft.Extensions.Logging;
using OrderLibrary.Extensions;
using OrderReceiver.Domain.Log;
using System.Collections.Generic;
using OrderReceiver.Infra.Configs;

namespace OrderReceiver.Infra.Services
{
    public class OrderReceiverService : IOrderReceiverService
    {
        private Dictionary<string, decimal> exposicoesFinanceiras;

        private readonly IQuickFixService _quickFixService;
        private readonly ILogger<OrderReceiverService> _logger;
        private readonly OrderReceiverConfig _configs;

        public OrderReceiverService(IQuickFixService quickFixService, ILogger<OrderReceiverService> logger, OrderReceiverConfig configs)
        {
            exposicoesFinanceiras = new Dictionary<string, decimal>();

            _quickFixService = quickFixService;
            _logger = logger;
            _configs = configs;

            _quickFixService.SetOnMessage((order, session) => ReceiveNewOrderSingle(order, session));
            
        }

        public void ReceiveNewOrderSingle(QuickFix.FIX44.NewOrderSingle order, SessionID session)
        {
            var simbolo = order.GetSymbol();

            _logger.LogInfo(LogMessageKeys.NEW_ORDER_SINGLE_KEY_RECEIVED, $"{simbolo}");

            decimal exposicaoAtual = GetValorExposicao(simbolo);

            var novoValor = CalcularExposicaoFinanceira(order, exposicaoAtual);
            bool orderValido = ValidarLimitesExposicao(novoValor);

            if (orderValido)
                SetValorExposicao(novoValor, simbolo);

            Message response = GetResponseMessage(order, orderValido);

            _quickFixService.SendMessage(response);            

            LogValoresAtuais();
        }

        public void Start()
        {
            _quickFixService.StartAcceptor();
        }

        public void Stop()
        {
            _quickFixService.StartAcceptor();
        }

        private void SetValorExposicao(decimal valor, string simbolo)
        {
            exposicoesFinanceiras[simbolo] = valor;
        }

        private decimal GetValorExposicao(string simbolo)
        {
            exposicoesFinanceiras.TryGetValue(simbolo, out decimal retorno);
            return retorno;
        }      

        private decimal CalcularExposicaoFinanceira(QuickFix.FIX44.NewOrderSingle order, decimal exposicaoAtual)
        {           
            return exposicaoAtual + order.GetValorTotal();
        }

        private bool ValidarLimitesExposicao(decimal valor)
        {
            if (valor > _configs.ExposicaoLimiteMaximo || valor < 0)
                return false;

            return true;
        }

        private void LogValoresAtuais()
        {
            foreach (var item in exposicoesFinanceiras)
            {
                _logger.LogInfo(item.Key, item.Value);
            }
        }

        private Message GetResponseMessage(QuickFix.FIX44.NewOrderSingle order, bool orderValido)
        {
            return orderValido ? GetExecutionReport(order) : GetOrderCancelReject(order, "Limite de exposição atingido");
        }

        private QuickFix.FIX44.ExecutionReport GetExecutionReport(QuickFix.FIX44.NewOrderSingle newOrderSingle)
        {
            QuickFix.FIX44.ExecutionReport executionReport = new QuickFix.FIX44.ExecutionReport();

            executionReport.SetField(newOrderSingle.Symbol);
            executionReport.SetField(newOrderSingle.OrderQty);
            executionReport.SetField(newOrderSingle.Price);
            executionReport.SetField(newOrderSingle.Side);

            LogResposta(LogMessageKeys.NEW_ORDER_SINGLE_KEY_RECEIVED_RESPONSE_ACEITO, newOrderSingle);

            return executionReport;
        }

        private QuickFix.FIX44.OrderCancelReject GetOrderCancelReject(QuickFix.FIX44.NewOrderSingle newOrderSingle, string motivoRejeicao)
        {
            QuickFix.FIX44.OrderCancelReject orderCancelReject = new QuickFix.FIX44.OrderCancelReject {
                OrdStatus = new OrdStatus(OrdStatus.REJECTED),
                Text = new Text(motivoRejeicao)
            };
         
            orderCancelReject.SetField(newOrderSingle.Symbol);
            orderCancelReject.SetField(newOrderSingle.OrderQty);
            orderCancelReject.SetField(newOrderSingle.Price);
            orderCancelReject.SetField(newOrderSingle.Side);

            LogResposta(LogMessageKeys.NEW_ORDER_SINGLE_KEY_RECEIVED_RESPONSE_REJEITADO, newOrderSingle);

            return orderCancelReject;

        }

        private void LogResposta(string mensagem, QuickFix.FIX44.NewOrderSingle order)
        {
            _logger.LogInfo(mensagem, $"{order.GetSymbol()} - {order.GetValorTotal()}");
        }

    }
}
