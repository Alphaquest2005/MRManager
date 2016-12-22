using System;
using System.Linq;
using System.Reflection;
using Common.DataEntites;

namespace DataEntites
{
    public sealed class NullEntity<T>: BaseEntity
    {
        
        private static readonly T instance;
        static NullEntity()
        {
            
            instance =(T) Activator.CreateInstance(Assembly.GetExecutingAssembly().ExportedTypes.FirstOrDefault(x => x.Name == typeof(T).Name.Substring(1)).GetType());
        }

        public static T Instance => instance;
        public NullEntity()
        {
            Id = -1;
        }
    }
}
