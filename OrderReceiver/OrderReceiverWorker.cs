using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderReceiver.Domain.Interfaces.Services;
using OrderReceiver.Domain.Log;
using OrderReceiver.Infra.Configs;
using OrderLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderReceiver
{
    public class OrderReceiverWorker : BackgroundService
    {
        private readonly ILogger<OrderReceiverWorker> _logger;
        private readonly IOrderReceiverService _orderReceiverService;
        private readonly OrderReceiverConfig _configs;

        public OrderReceiverWorker(ILogger<OrderReceiverWorker> logger, IOrderReceiverService OrderReceiverService, OrderReceiverConfig configs)
        {
            _logger = logger;
            _orderReceiverService = OrderReceiverService;
            _configs = configs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Execução rodando às: {time}", DateTimeOffset.Now);

                _orderReceiverService.Start();

                while (!stoppingToken.IsCancellationRequested);

            }
            catch (Exception e)
            {
                if(_orderReceiverService != null)
                    _orderReceiverService.Stop();

                _logger.LogError(LogMessageKeys.NEW_ORDER_SINGLE_KEY_RECEIVED_ERRO, e);
            }
        }
    }
}
