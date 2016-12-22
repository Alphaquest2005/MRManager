namespace SystemMessages
{
    public class StatusError
    {
        public string Process { get;  }
        public string Method { get; }
        public string Error { get; }

        public StatusError(string process,string method,string error)
        {
            Process = process;
            Method = method;
            Error = error;
        }
    }
}
