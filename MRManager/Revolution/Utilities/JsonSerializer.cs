using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonSerializer
    {
        public static string JsonSerialize(object obj)
        {
            return
            JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }
    }
}
