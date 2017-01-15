using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessSystemMessage : ISystemMessage, IProcess
    {
        ISystemProcess Process { get; }
        IProcessStateInfo ProcessInfo { get; }
    }

    public static class IProcessSystemMessageExtensions
    {
        public static Dictionary<string, string> GetDerivedProperties(this IMessage msg)
        {
            FieldInfo[] fields = msg.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            return fields.ToDictionary(f => f.Name, f => JsonConvert.SerializeObject(f.GetValue(msg)));
        } 
    }
}