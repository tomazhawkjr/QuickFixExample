using OrderLibrary.Domain.Interface.Services;
using QuickFix;
using QuickFix.Transport;
using System;

namespace OrderLibrary.Services
{
    public class QuickFixService : IQuickFixService
    {
        private readonly SessionSettings settings;
        private readonly QuickFixApplication application;
        private readonly IMessageStoreFactory storeFactory;
        private readonly ILogFactory logFactory;
        private ThreadedSocketAcceptor acceptor;
        private SocketInitiator initiator;
        private SessionID sessionID;
        private Session session;

        public QuickFixService(string configPath)
        {
            this.settings = new SessionSettings(configPath);

            this.application = new QuickFixApplication(IniciarSession);

            this.storeFactory = new FileStoreFactory(settings);
            this.logFactory = new FileLogFactory(settings);

        }

        private void IniciarSession(SessionID sessionId)
        {
            this.sessionID = sessionId;
            this.session = Session.LookupSession(sessionID);
        }

        public void SendMessage(Message message)
        {
            try
            {
                session.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} - {e.StackTrace}");
            }
           
        }

        #region Sets

        public void SetFromAdmin(Action<Message, SessionID> fromAdmin)
        {
            this.application.FromAdminMethod = fromAdmin;
        }   

        public void SetOnLogon(Action<SessionID> onLogon)
        {
            this.application.OnLogonMethod = onLogon;
        }

        public void SetOnLogout(Action<SessionID> onLogout)
        {
            this.application.OnLogoutMethod = onLogout;
        }

        public void SetToAdmin(Action<Message, SessionID> toAdmin)
        {
            this.application.ToAdminMethod = toAdmin;
        }

        public void SetToApp(Action<Message, SessionID> toApp)
        {
            this.application.ToAppMethod = toApp;
        }

        public void SetOnMessage(Action<QuickFix.FIX44.NewOrderSingle, SessionID> onMessage)
        {
            this.application.OnMessageMethod = onMessage;
        }

        public void SetOnMessage(Action<QuickFix.FIX44.OrderCancelReject, SessionID> onMessage)
        {
            this.application.OnMessageMethodOrderCancelReject = onMessage;
        }

        public void SetOnMessage(Action<QuickFix.FIX44.ExecutionReport, SessionID> onMessage)
        {
            this.application.OnMessageMethodExecutionReport = onMessage;
        }

        #endregion

        #region Starts/Stops

        public void StartAcceptor()
        {
            if (acceptor == null)
                acceptor = new ThreadedSocketAcceptor(
                    application,
                    storeFactory,
                    settings,
                    logFactory);

            acceptor.Start();
        }

        public void StopAcceptor()
        {
            if (acceptor != null)
                acceptor.Stop();
        }

        public void StartInitiator()
        {
            if(initiator == null)
                initiator = new SocketInitiator(
                   application,
                   storeFactory,
                   settings,
                   logFactory);
            
            initiator.Start();
        }

        public void StopInitiator()
        {
            if (initiator != null)
                initiator.Stop();
        }

        #endregion
    }
}
