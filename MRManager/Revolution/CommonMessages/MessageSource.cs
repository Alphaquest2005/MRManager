using System.ComponentModel.Composition;
using SystemInterfaces;

namespace CommonMessages
{

    [Export(typeof(IMessageSource))]
   public class MessageSource: IMessageSource
    {
        public MessageSource() { }
       public MessageSource(string source)
       {
           Source = source;
       }

       public string Source { get; }
    }
}
