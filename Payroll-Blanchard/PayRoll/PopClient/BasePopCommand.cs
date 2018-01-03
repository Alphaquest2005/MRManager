using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    internal abstract class BasePopCommand : IPopCommand
    {
        protected PopConnection PopConnection { get; private set; }

        public event EventHandler<PopClientDirectionalLogEventArgs> PopClientLog;

        protected void OnPopClientLog(string line, bool isServerResponse)
        {
            if (String.IsNullOrWhiteSpace(line))
                return;

            EventHandler<PopClientDirectionalLogEventArgs> local = this.PopClientLog;
            if (local != null)
            {
                local(this, new PopClientDirectionalLogEventArgs(line, isServerResponse));
            }
        }

        public virtual bool IsMultiLineResponse
        {
            get
            {
                return false;
            }
        }

        public BasePopCommand(PopConnection popConnection)
        {
            this.PopConnection = popConnection;
        }

        protected string Response { get; private set; }

        protected string MultiLineResponse { get; private set; }

        public void Execute(params object[] arguments)
        {
            this.Execute(arguments.FirstOrDefault());
        }

        private void Execute(object argument)
        {
            this.OnPopClientLog(ExecuteInternal(argument), false);
            this.Response = this.PopConnection.ReadResponseString();
            this.OnPopClientLog(this.Response, true);
            this.CheckForErrorResponse();
            if (this.IsMultiLineResponse)
            {
                this.MultiLineResponse = this.PopConnection.ReadMultiLineResponseString();
            }

            this.ParseInternal();
        }

        protected virtual void ParseInternal()
        {            
        }

        protected abstract string ExecuteInternal(object argument);

        private void CheckForErrorResponse()
        {
            if (this.Response.StartsWith("-ERR"))
            {
                throw new PopClientException(new String(this.Response.Skip(4).ToArray()).Trim());
            }
        }
    }
}
