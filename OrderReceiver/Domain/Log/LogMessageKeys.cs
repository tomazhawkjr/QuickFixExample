using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderReceiver.Domain.Log
{
    public static class LogMessageKeys
    {
        public static string NEW_ORDER_SINGLE_KEY_RECEIVED = "ORDERRECEIVER.NEWORDERSINGLE";
        public static string NEW_ORDER_SINGLE_KEY_RECEIVED_ERRO = "ORDERRECEIVER.NEWORDERSINGLE.ERRO";
        public static string NEW_ORDER_SINGLE_KEY_RECEIVED_RESPONSE_ACEITO = "ORDERRECEIVER.NEWORDERSINGLE.RESPONSE.ACEITO";
        public static string NEW_ORDER_SINGLE_KEY_RECEIVED_RESPONSE_REJEITADO = "ORDERRECEIVER.NEWORDERSINGLE.RESPONSE.REJEITADO";
    }
}
