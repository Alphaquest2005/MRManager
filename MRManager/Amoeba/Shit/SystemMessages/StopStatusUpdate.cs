namespace SystemMessages
{
    public class StopStatusUpdate
    {
        public string Process { get;  }
        public string Method { get; }
        
        public StopStatusUpdate(string process,string method)
        {
            Process = process;
            Method = method;
            
        }
    }
}
