using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Common;
using Common.DataEntites;
using RevolutionEntities.Process;

namespace RevolutionEntities
{
    public sealed class NullEntity<T> : BaseEntity, IProcessSource
    {

        private static readonly T instance;

        static NullEntity()
        {
            try
            {
                var export = BootStrapper.BootStrapper.Container.GetExport<T>();
                if (export != null)
                {
                    var itm = export.Value;
                    instance = (T)Activator.CreateInstance(itm.GetType());

                }
                else
                {
                    instance = default(T);
                }
                var entity = instance as IEntity;
                if (entity != null) entity.Id = -1;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static T Instance => instance;
        public NullEntity()
        {
            Id = -1;
        }

        public ISystemSource Source => new Source(Guid.NewGuid(), "NullEntity" + typeof(NullEntity<T>).Name, new SourceType(typeof(NullEntity<T>)),new SystemProcess(new Process.Process(1,0, "Starting System", "Prepare system for Intial Use","", new Agent("System")), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
    }


}

