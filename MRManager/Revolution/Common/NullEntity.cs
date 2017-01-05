using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using SystemInterfaces;
using Common.DataEntites;


namespace DataEntites
{
    public sealed class NullEntity<T>: BaseEntity 
    {
        
        private static readonly T instance;
        static NullEntity()
        {
            var export = BootStrapper.BootStrapper.Container.GetExport<T>();
            if (export != null)
            {
                var itm = export.Value;
                instance = (T) Activator.CreateInstance(itm.GetType());
                
            }
            else
            {
                instance = default(T);
            }
            var entity = instance as IEntity;
            if (entity != null) entity.Id = -1;

        }

        public static T Instance => instance;
        public NullEntity()
        {
            Id = -1;
        }
    }
}
