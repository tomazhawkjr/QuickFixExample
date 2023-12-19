using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Domain.Log
{
    public class LogMessage
    {
        public string Chave { get; set; }      
        public object Dados { get; set; }
        public string Erro { get; set; }
        public DateTime DataHora { get; set; }

    }
}
