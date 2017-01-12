using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SystemInterfaces;
using Common;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Utils
{
    public static class ComplexEventLogsExtensions
    {
        public static List<IComplexEventLog> CreatEventLogs(this ImmutableList<IProcessSystemMessage> msgList, ISystemSource source)
        {
            try
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
                            message: ""
                            // ToDo: Check out circular differnece
                            //new JavaScriptSerializer().Serialize(new
                            //{
                            //    d = xevent.GetDerivedProperties()
                            //})
                            ,
                            processInfo: new JavaScriptSerializer().Serialize(xevent.ProcessInfo)))
                    .Cast<IComplexEventLog>()
                    .ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public static List<IComplexEventLog> CreatEventLogs(this IList<IProcessExpectedEvent> msgList, Dictionary<string,IProcessSystemMessage> inMessages, ISystemSource source)
        {
            try
            {
                 return msgList.Select(
                    xevent =>
                        new ComplexEventLog(operation: $"Expected Message:<{xevent.EventType}>",
                            status: inMessages.ContainsKey(xevent.Key) ? "Raised" : "NotRaised",
                            time: inMessages.ContainsKey(xevent.Key)?inMessages[xevent.Key].MessageDateTime:DateTime.MinValue,
                            sourceGuid: inMessages.ContainsKey(xevent.Key) ? inMessages[xevent.Key].Source.SourceId.ToString(): "",
                            sourceName: inMessages.ContainsKey(xevent.Key) ? inMessages[xevent.Key].Source.SourceName:"",
                            source: inMessages.ContainsKey(xevent.Key) ? inMessages[xevent.Key].Source.SourceType.Source_Type.GetFriendlyName(): "",
                            expectedSource: xevent.ExpectedSourceType.Source_Type.GetFriendlyName(),
                            message:"",// new JavaScriptSerializer().Serialize(inMessages[xevent.Key]),
                            processInfo: new JavaScriptSerializer().Serialize(xevent.ProcessInfo)))
                    .Cast<IComplexEventLog>()
                    .ToList();
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
