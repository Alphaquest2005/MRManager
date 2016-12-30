using System;
using System.Linq;
using System.Reflection;
using Common.DataEntites;
using DataInterfaces;

namespace DataEntites
{
    public sealed class NullEntity<T>: BaseEntity where T:class,IEntity, new()
    {
        
        private static readonly T instance;
        static NullEntity()
        {

            instance = new T() {Id = -1};
                //(T) Activator.CreateInstance(Assembly.GetExecutingAssembly().ExportedTypes.FirstOrDefault(x => x.Name == typeof(T).Name.Substring(1)).GetType());
        }

        public static T Instance => instance;
        public NullEntity()
        {
            Id = -1;
        }
    }
}
