using SystemInterfaces;

namespace CommonMessages
{
   public class MessageSource: IMessageSource
    {
       public MessageSource(string source)
       {
           Source = source;
       }

       public string Source { get; }
    }
}
