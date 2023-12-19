using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Domain.Interface.Services
{
    public interface IQuickFixService
    {
        public void SendMessage(Message message);
        public void SetFromAdmin(Action<Message, SessionID> fromAdmin);  
        public void SetOnLogon(Action<SessionID> onLogon);
        public void SetOnLogout(Action<SessionID> onLogout);
        public void SetToAdmin(Action<Message, SessionID> toAdmin);
        public void SetToApp(Action<Message, SessionID> toApp);
        public void SetOnMessage(Action<QuickFix.FIX44.NewOrderSingle, SessionID> onMessage);
        public void SetOnMessage(Action<QuickFix.FIX44.OrderCancelReject, SessionID> onMessage);
        public void SetOnMessage(Action<QuickFix.FIX44.ExecutionReport, SessionID> onMessage);
       

        public void StartAcceptor();
        public void StopAcceptor();
        public void StartInitiator();
        public void StopInitiator();
        
    }
}

