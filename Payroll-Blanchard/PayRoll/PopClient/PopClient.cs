using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;

namespace PopClient
{
    public class PopClient : IDisposable
    {
        const int StandardPopPort = 25;

        #region Fields

        private BackgroundWorker worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
        private Mutex threadMutex = new Mutex();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PopClient class.
        /// </summary>
        /// <param name="host">The name or IP address of the host server</param>
        /// <param name="port">The port to be used</param>
        public PopClient(string host, int port)
        {
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;

            this.Host = host;
            this.Port = port;
        }

        /// <summary>
        /// Initializes a new instance of the PopClient class.
        /// </summary>
        /// <param name="host">The name or IP address of the host server</param>
        public PopClient(string host)
            : this(host, PopClient.StandardPopPort)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PopClient class.
        /// </summary>
        public PopClient()
            : this(String.Empty, PopClient.StandardPopPort)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name or IP address of the POP3 host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port used for the POP3 connection
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the POP3 username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the POP3 password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Specify whether the PopClient uses a Secure Sockets Layer connection
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Specify whether email is deleted after fetch
        /// </summary>
        public bool DeleteMailAfterPop { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the amount of time after the connection times out.
        /// </summary>
        public int Timeout { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the asynchronous fetch completes
        /// </summary>
        public event EventHandler<MailPopCompletedEventArgs> MailPopCompleted;

        /// <summary>
        /// Occurs when a mail is fetched
        /// </summary>
        public event EventHandler<MailPoppedEventArgs> MailPopped;

        /// <summary>
        /// Occurs when summary info for the fetch operation is available
        /// </summary>
        public event EventHandler<MailPopInfoFetchedEventArgs> QueryPopInfoCompleted;

        /// <summary>
        /// Occurs when a POP3 command is sent to the server
        /// </summary>
        public event EventHandler<PopClientLogEventArgs> ChatCommandLog;

        /// <summary>
        /// Occurs when a POP3 response is received from the server
        /// </summary>
        public event EventHandler<PopClientLogEventArgs> ChatResponseLog;

        #endregion

        /// <summary>
        /// Releases all resources used by the PopClient class.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                worker.Dispose();
                threadMutex.Dispose();
            }
        }

        /// <summary>
        /// Begins an asynchronous fetch operation
        /// </summary>
        public void PopMail()
        {
            if (worker.IsBusy)
            {
                throw new PopClientException() { PopClientBusy = true };
            }

            if (String.IsNullOrWhiteSpace(this.Host))
            {
                throw new InvalidOperationException("Host cannot be null or empty.");
            }

            threadMutex.WaitOne();
            worker.RunWorkerAsync();
            threadMutex.ReleaseMutex();
        }

        /// <summary>
        /// Cancels the asynchronous fetch operation
        /// </summary>
        public void Cancel()
        {
            threadMutex.WaitOne();
            worker.CancelAsync();
            threadMutex.ReleaseMutex();
        }


        /// <summary>
        /// Indicates whether a fetch operation is under way
        /// </summary>
        /// <returns>True if a fetch is on, False otherwise</returns>
        public bool IsWorking()
        {
            return worker.IsBusy;
        }

        protected void OnMailPopInfoFetched(int count, int size)
        {
            EventHandler<MailPopInfoFetchedEventArgs> local = this.QueryPopInfoCompleted;
            if (local != null)
            {
                local(this, new MailPopInfoFetchedEventArgs(count, size));
            }
        }


        protected void OnMailPopped(int index, MailMessage message, int size, string uidl, DateTime receivedTime)
        {
            EventHandler<MailPoppedEventArgs> local = this.MailPopped;
            if (local != null)
            {
                local(this, new MailPoppedEventArgs(index, message, size, uidl, receivedTime));
            }
        }

        protected void OnMailPopCompleted()
        {
            EventHandler<MailPopCompletedEventArgs> local = this.MailPopCompleted;
            if (local != null)
            {
                local(this, new MailPopCompletedEventArgs());
            }
        }

        protected void OnMailPopAborted(Exception ex)
        {
            EventHandler<MailPopCompletedEventArgs> local = this.MailPopCompleted;
            if (local != null)
            {
                local(this, new MailPopCompletedEventArgs(ex));
            }
        }

        protected void OnChatCommandLog(string line)
        {
            EventHandler<PopClientLogEventArgs> local = this.ChatCommandLog;
            if (local != null)
            {
                local(this, new PopClientLogEventArgs(line));
            }
        }

        protected void OnChatResponseLog(string line)
        {
            EventHandler<PopClientLogEventArgs> local = this.ChatResponseLog;
            if (local != null)
            {
                local(this, new PopClientLogEventArgs(line));
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (PopChat chat = new PopChat(this))
            {
                chat.Nothing.Execute();
                chat.User.Execute(this.Username);
                chat.Pass.Execute(this.Password);
                chat.Stat.Execute();

                this.OnMailPopInfoFetched(chat.Stat.Count, chat.Stat.Size);

                for (int fetchIndex = 1; fetchIndex <= chat.Stat.Count; fetchIndex++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    chat.List.Execute(fetchIndex);
                    chat.Uidl.Execute(fetchIndex);
                    chat.Retr.Execute(fetchIndex);
                    this.OnMailPopped(fetchIndex, chat.Retr.Message, chat.List.Size, chat.Uidl.Uidl, chat.Retr.ReceivedTime);

                    if (this.DeleteMailAfterPop)
                    {
                        chat.Dele.Execute(fetchIndex);
                    }
                }

                chat.Quit.Execute();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.OnMailPopAborted(new PopClientException() { PopClientUserCancelled = true });
                return;
            }

            if (e.Error != null)
            {
                this.OnMailPopAborted(e.Error);
                return;
            }

            this.OnMailPopCompleted();
        }

        internal void LogLine(string line, bool isServerResponse)
        {
            if (isServerResponse)
            {
                this.OnChatResponseLog(line);
            }
            else
            {
                this.OnChatCommandLog(line);
            }
        }
    }
}
