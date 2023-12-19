using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator.Domain.Interfaces.Services
{
    public interface IOrderGeneratorService
    {
        public void Start();

        public void Stop();

        public void SendNewOrderSingle(QuickFix.FIX44.NewOrderSingle order);

        public QuickFix.FIX44.NewOrderSingle GetNewOrderSingle();

    }
}
