using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace SystemInterfaces
{
    
    public interface IProcessSystemMessage : ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
        IProcessStateInfo ProcessInfo { get; }
    }

    public static class IProcessSystemMessageExtensions
    {
        public static Dictionary<string, string> GetDerivedProperties(this IProcessSystemMessage msg)
        {
            FieldInfo[] fields = msg.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            return fields.ToDictionary(f => f.Name, f => new JavaScriptSerializer().Serialize(f.GetValue(msg)));
        } 
    }
}