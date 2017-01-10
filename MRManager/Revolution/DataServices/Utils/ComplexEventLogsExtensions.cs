using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SystemInterfaces;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Utils
{
    public static class ComplexEventLogsExtensions
    {
        public static List<IComplexEventLog> CreatEventLogs(this List<IProcessSystemMessage> msgList, ISystemSource source)
        {
           return msgList.Select(
                    xevent =>
                        new ComplexEventLog(operation: $"Output Message:<{xevent.GetType().GetFriendlyName()}>",
                            status: "Raised",
                            time: xevent.MessageDateTime,
                            sourceGuid: xevent.Source.SourceId.ToString(),
                            sourceName: xevent.Source.SourceName,
                            source: xevent.Source.SourceType.Source_Type.GetFriendlyName(),
                            expectedSource: source.SourceType.Source_Type.GetFriendlyName(),
                            message: new JavaScriptSerializer().Serialize(xevent),
                            processInfo: new JavaScriptSerializer().Serialize(xevent.ProcessInfo)))
                    .Cast<IComplexEventLog>()
                    .ToList();
        }

        public static List<IComplexEventLog> CreatEventLogs(this IList<IProcessExpectedEvent> msgList, Dictionary<string,IProcessSystemMessage> inMessages, ISystemSource source)
        {
            return msgList.Select(
                    xevent =>
                        new ComplexEventLog(operation: $"Expected Message:<{xevent.EventType}>",
                            status: xevent.Raised() ? "Raised" : "NotRaised",
                            time: inMessages[xevent.Key].MessageDateTime,
                            sourceGuid: inMessages[xevent.Key].Source.SourceId.ToString(),
                            sourceName: inMessages[xevent.Key].Source.SourceName,
                            source: inMessages[xevent.Key].Source.SourceType.Source_Type.GetFriendlyName(),
                            expectedSource: xevent.ExpectedSourceType.Source_Type.GetFriendlyName(),
                            message: new JavaScriptSerializer().Serialize(inMessages[xevent.Key]),
                            processInfo: new JavaScriptSerializer().Serialize(xevent.ProcessInfo)))
                    .Cast<IComplexEventLog>()
                    .ToList();
        }
    }
}
