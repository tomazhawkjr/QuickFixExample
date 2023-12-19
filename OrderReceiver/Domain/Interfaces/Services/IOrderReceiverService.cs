using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderReceiver.Domain.Interfaces.Services
{
    public interface IOrderReceiverService
    {
        public void Start();

        public void Stop();

        public void ReceiveNewOrderSingle(QuickFix.FIX44.NewOrderSingle order, SessionID session);

    }
}
