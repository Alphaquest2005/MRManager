namespace SystemMessages
{
    public class StatusTimer
    {
        public string Process { get;  }
        public string Method { get; }
        public string Message { get; }

        public StatusTimer(string process,string method,string message)
        {
            Process = process;
            Method = method;
            Message = message;
        }
    }
}
