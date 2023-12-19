using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderReceiver.Infra.Configs
{
    public class OrderReceiverConfig
    {
        public QuickFixConfigs QuickFixConfigs { get; set; }
        public int ExposicaoLimiteMaximo { get; set; }
    }

    public class QuickFixConfigs
    {
        public string ConfigPath { get; set; }
    }
}
