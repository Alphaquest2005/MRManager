using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net.Security;
using System.Collections;

namespace PopClient
{
    internal class PopConnection : IDisposable
    {
        private TcpClient tcpClient;
        private Stream stream;

        public PopConnection(PopClient popClient)
        {
            tcpClient = new TcpClient(popClient.Host, popClient.Port) { SendTimeout = popClient.Timeout, ReceiveTimeout = popClient.Timeout };

            if (popClient.EnableSsl)
            {
                var secureStream = new SslStream(tcpClient.GetStream());
                secureStream.AuthenticateAsClient(popClient.Host);
                stream = secureStream;
            }
            else
            {
                stream = tcpClient.GetStream();
            }
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
                stream.Dispose();
                (tcpClient as IDisposable).Dispose();
            }
        }

        public string ReadResponseString()
        {
            byte[] bytes;
            int count = this.ReadResponse(out bytes);
            return bytes.GetString(count);
        }

        public string ReadMultiLineResponseString()
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes;

            do
            {
                int count = this.ReadResponse(out bytes);
                sb.Append(bytes.GetString(count));                
            }
            while( !CheckForEndOfData(sb) );

            return sb.ToString();
        }

        private char[] endMarker = { '\r', '\n', '.', '\r', '\n' };

        private bool CheckForEndOfData(StringBuilder sb)
        {
            if (sb.Length < 5)
                return false;

            char[] compare = new char[5];
            sb.CopyTo(sb.Length - 5, compare, 0, 5);

            return (endMarker as IStructuralEquatable).Equals(compare, EqualityComparer<char>.Default);
        }

        public int ReadResponse(out byte[] bytes)
        {
            int bufferSize = tcpClient.ReceiveBufferSize;
            bytes = new byte[bufferSize];
            return stream.Read(bytes, 0, bufferSize);
        }

        private string SendCommandInternal(string command)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(command);
            stream.Write(bytes, 0, bytes.Length);
            return command;
        }

        public string SendQuit()
        {
            return SendCommandInternal("QUIT\r\n");
        }

        public string SendStat()
        {
            return SendCommandInternal("STAT\r\n");
        }

        public string SendList(int messageId)
        {
            return SendCommandInternal(String.Format("LIST {0}\r\n", messageId));
        }

        public string SendDele(int messageId)
        {
            return SendCommandInternal(String.Format("DELE {0}\r\n", messageId));
        }

        public string SendUidl(int messageId)
        {
            return SendCommandInternal(String.Format("UIDL {0}\r\n", messageId));
        }

        public string SendRetr(int messageId)
        {
            return SendCommandInternal(String.Format("RETR {0}\r\n", messageId));
        }

        public string SendUser(string user)
        {
            return SendCommandInternal(String.Format("USER {0}\r\n", user));
        }

        public string SendPass(string pass)
        {
            SendCommandInternal(String.Format("PASS {0}\r\n", pass));
            return "PASS ********\r\n";
        }
    }
}
