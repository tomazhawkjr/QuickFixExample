using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderLibrary.Domain.Log;
using System.Text.Json;

namespace OrderLibrary.Extensions
{
    public static class LoggerExtersion
    {
        public static void LogInfo(this ILogger logger, string key, object dados)
        {
            string logInfo = JsonSerializer.Serialize(new LogMessage
            {
                Chave = key,
                Dados = dados,
                DataHora = DateTime.Now,
            });

            logger.LogInformation(logInfo);
        }

        public static void LogError(this ILogger logger, string key, Exception e, object dados = null)
        {
            string logInfo = JsonSerializer.Serialize(new LogMessage
            {
                Chave = key,
                Dados = dados,
                DataHora = DateTime.Now,
                Erro = $"{e.Message} - {e.StackTrace}"
            });

            logger.LogError(logInfo);
        }
    }
}
