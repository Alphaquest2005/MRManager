using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace SystemMessages
{
    [Export]
    public class SystemStarted : SystemProcessMessage
    {
        public SystemStarted(ISystemProcess process,MessageSource source ) : base(process,source){}

        
        
    }
}
