namespace CommonMessages
{
    public class BaseMessage
    {
        public BaseMessage(MessageSource source)
        {
            Source = source;
        }

        public MessageSource Source { get; }
    }
}
