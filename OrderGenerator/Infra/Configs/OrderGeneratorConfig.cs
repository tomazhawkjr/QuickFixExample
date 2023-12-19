using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator.Infra.Configs
{
    public class OrderGeneratorConfig
    {
        public QuickFixConfigs QuickFixConfigs { get; set; }
        public int IntervaloExecucao { get; set; }
    }

    public class QuickFixConfigs
    {
        public string ConfigPath { get; set; }
    }
}
