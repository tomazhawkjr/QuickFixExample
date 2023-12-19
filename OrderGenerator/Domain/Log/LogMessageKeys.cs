using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator.Domain.Log
{
    public static class LogMessageKeys
    {
        public static string NEW_ORDER_SINGLE_KEY = "ORDERGENERATOR.NEWORDERSINGLE.MENSAGEMENVIADA";
        public static string NEW_ORDER_SINGLE_KEY_ERRO = "ORDERGENERATOR.NEWORDERSINGLE.MENSAGEMENVIADA.ERRO";
        public static string NEW_ORDER_SINGLE_KEY_REPORT = "ORDERGENERATOR.NEWORDERSINGLE.RESPOSTARECEBIDA.ACEITO";
        public static string NEW_ORDER_SINGLE_KEY_REJECT = "ORDERGENERATOR.NEWORDERSINGLE.RESPOSTARECEBIDA.REJEITADO";
    }
}
