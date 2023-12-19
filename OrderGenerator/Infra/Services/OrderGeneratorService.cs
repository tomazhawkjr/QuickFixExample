using Microsoft.Extensions.Logging;
using OrderGenerator.Domain.Interfaces.Services;
using OrderGenerator.Domain.Log;
using OrderGenerator.Infra.Util;
using OrderLibrary.Domain.Interface.Services;
using OrderLibrary.Extensions;
using QuickFix;
using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderGenerator.Infra.Util.RandomUtil;

namespace OrderGenerator.Infra.Services
{
    public class OrderGeneratorService : IOrderGeneratorService
    {
        private readonly List<string> simbolos = new List<string>{ "PETR4", "VALE3", "VIIA4" };
        private readonly List<char> lados = new List<char>{ Side.BUY, Side.SELL };

        private readonly IQuickFixService _quickFixService;
        private readonly ILogger<OrderGeneratorService> _logger;

        public OrderGeneratorService(IQuickFixService quickFixService, ILogger<OrderGeneratorService> logger)
        {
            _quickFixService = quickFixService;
            _logger = logger;

            _quickFixService.SetOnMessage(OnMessageReject);
            _quickFixService.SetOnMessage(OnMessageReject);
        }

        public QuickFix.FIX44.NewOrderSingle GetNewOrderSingle()
        {
            return new QuickFix.FIX44.NewOrderSingle()
            {
                Price = new Price(GetRandomDecimalMinMax(0.01m, 9999.99m)),
                Side = new Side(GetRandomItemList<char>(lados)),
                Symbol = new Symbol(GetRandomItemList<string>(simbolos)),
                OrderQty = new OrderQty(GetRandomInt(99999, 1))
            };
        }

        public void SendNewOrderSingle(QuickFix.FIX44.NewOrderSingle order)
        {

            _logger.LogInfo(LogMessageKeys.NEW_ORDER_SINGLE_KEY, $"{order.GetSymbol()} - {order.GetValorTotal()}");

            _quickFixService.SendMessage(order);
        }

        public void OnMessageReject(QuickFix.FIX44.OrderCancelReject message, SessionID sessionID)
        {
            _logger.LogInfo(LogMessageKeys.NEW_ORDER_SINGLE_KEY_REJECT, message.Text.getValue());
        }

        public void OnMessageExecutionReport(QuickFix.FIX44.ExecutionReport message, SessionID sessionID)
        {
            _logger.LogInfo(LogMessageKeys.NEW_ORDER_SINGLE_KEY_REPORT, message.Symbol.getValue());
        }

        public void Start()
        {
            _quickFixService.StartInitiator();
        }

        public void Stop()
        {
            _quickFixService.StopInitiator();
        }
    }
}
