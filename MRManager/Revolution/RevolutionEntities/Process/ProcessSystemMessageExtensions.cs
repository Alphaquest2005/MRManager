using System.Collections.Concurrent;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public static class ProcessSystemMessageExtensions
    {
        private static ConcurrentDictionary<IProcessSystemMessage, IProcessExpectedEvent> MessageExpectedEvents { get; }
            = new ConcurrentDictionary<IProcessSystemMessage, IProcessExpectedEvent>();

        public static bool IsValid(this IProcessSystemMessage msg)
        {
            return MessageExpectedEvents.ContainsKey(msg);
        }

        public static void ValidatedBy(this IProcessSystemMessage msg, IProcessExpectedEvent expectedEvent)
        {
            MessageExpectedEvents.AddOrUpdate(msg,expectedEvent, (x, v) => expectedEvent);
        }
    }
}