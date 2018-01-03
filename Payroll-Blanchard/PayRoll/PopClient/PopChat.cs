using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal class PopChat : IDisposable
    {
        private PopConnection popConnection;
        private PopClient client;

        internal NothingPopCommand Nothing { get; private set; }
        internal UserPopCommand User { get; private set; }
        internal PassPopCommand Pass { get; private set; }
        internal StatPopCommand Stat { get; private set; }
        internal RetrPopCommand Retr { get; private set; }
        internal QuitPopCommand Quit { get; private set; }
        internal ListPopCommand List { get; private set; }
        internal UidlPopCommand Uidl { get; private set; }
        internal DelePopCommand Dele { get; private set; }

        public PopChat(PopClient client)
        {
            this.client = client;
            popConnection = new PopConnection(client);

            Nothing = new NothingPopCommand(popConnection);
            User = new UserPopCommand(popConnection);
            Pass = new PassPopCommand(popConnection);
            Stat = new StatPopCommand(popConnection);
            Retr = new RetrPopCommand(popConnection);
            Quit = new QuitPopCommand(popConnection);
            List = new ListPopCommand(popConnection);
            Uidl = new UidlPopCommand(popConnection);
            Dele = new DelePopCommand(popConnection);

            new IPopCommand[] { Nothing, User, Pass, Stat, Retr, Quit, List, Uidl, Dele }.ToList().ForEach(pc => pc.PopClientLog += PopCommand_PopClientLog);
        }

        private void PopCommand_PopClientLog(object sender, PopClientDirectionalLogEventArgs e)
        {
            this.client.LogLine(e.Line, e.IsServerResponse);   
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                popConnection.Dispose();
            }
        }
    }
}
