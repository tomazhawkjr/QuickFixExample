using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderGenerator.Domain.Interfaces.Services;
using OrderGenerator.Domain.Log;
using OrderGenerator.Infra.Configs;
using OrderLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderGenerator
{
    public class OrderGeneratorWorker : BackgroundService
    {
        private readonly ILogger<OrderGeneratorWorker> _logger;
        private readonly IOrderGeneratorService _orderGeneratorService;
        private readonly OrderGeneratorConfig _configs;

        public OrderGeneratorWorker(ILogger<OrderGeneratorWorker> logger, IOrderGeneratorService orderGeneratorService, OrderGeneratorConfig configs)
        {
            _logger = logger;
            _orderGeneratorService = orderGeneratorService;
            _configs = configs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(5000, stoppingToken);    

                _orderGeneratorService.Start();

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInfo("Execu��o rodando �s: {time}", DateTimeOffset.Now);

                    var order = _orderGeneratorService.GetNewOrderSingle();

                    _orderGeneratorService.SendNewOrderSingle(order);

                    await Task.Delay(_configs.IntervaloExecucao, stoppingToken);
                }

            }
            catch (Exception e)
            {
                if(_orderGeneratorService != null)
                    _orderGeneratorService.Stop();

                _logger.LogError(LogMessageKeys.NEW_ORDER_SINGLE_KEY_ERRO, e);
            }
        }
    }
}
