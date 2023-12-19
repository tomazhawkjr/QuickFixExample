using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Services
{
    public class QuickFixApplication : MessageCracker, IApplication
    {
        public QuickFixApplication(Action<SessionID> onCreateMethod)
        {
            this.OnCreateMethod = onCreateMethod;
        }
      
        public Action<Message, SessionID> FromAdminMethod { private get; set; }     
        public Action<SessionID> OnCreateMethod { private get; set; }
        public Action<SessionID> OnLogonMethod { private get; set; }
        public Action<SessionID> OnLogoutMethod { private get; set; }
        public Action<Message, SessionID> ToAdminMethod { private get; set; }
        public Action<Message, SessionID> ToAppMethod { private get; set; }
        public Action<QuickFix.FIX44.NewOrderSingle, SessionID> OnMessageMethod { private get; set; }
        public Action<QuickFix.FIX44.OrderCancelReject, SessionID> OnMessageMethodOrderCancelReject { private get; set; }
        public Action<QuickFix.FIX44.ExecutionReport, SessionID> OnMessageMethodExecutionReport { private get; set; }

        public void FromAdmin(Message message, SessionID sessionID)
        {
            if (FromAdminMethod == default)
                return;

            FromAdminMethod(message, sessionID);
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine($"Recebido mensagem de aplicativo: {message} na sessão {sessionID}");
            Crack(message, sessionID);
        }

        public void OnCreate(SessionID sessionID)
        {
            if (OnCreateMethod == default)
                return;

            OnCreateMethod(sessionID);
        }

        public void OnLogon(SessionID sessionID)
        {
             if (OnLogonMethod == default)
                return;

            OnLogonMethod(sessionID);
        }

        public void OnLogout(SessionID sessionID)
        {
             if (OnLogoutMethod == default)
                return;

            OnLogoutMethod(sessionID);
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
             if (ToAdminMethod == default)
                return;

            ToAdminMethod(message, sessionID);
        }

        public void ToApp(Message message, SessionID sessionID)
        {
             if (ToAppMethod == default)
                return;

            ToAppMethod(message, sessionID);
        }

        public void OnMessage(QuickFix.FIX44.NewOrderSingle message, SessionID sessionID)
        {
            if (OnMessageMethod == default)
                return;

            OnMessageMethod(message, sessionID);
        }

        public void OnMessage(QuickFix.FIX44.OrderCancelReject message, SessionID sessionID)
        {
            if (OnMessageMethodOrderCancelReject == default)
                return;

            OnMessageMethodOrderCancelReject(message, sessionID);
        }

        public void OnMessage(QuickFix.FIX44.ExecutionReport message, SessionID sessionID)
        {
            if (OnMessageMethodExecutionReport == default)
                return;

            OnMessageMethodExecutionReport(message, sessionID);
        }

    }
}
