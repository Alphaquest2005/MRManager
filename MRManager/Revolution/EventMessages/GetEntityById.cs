using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    [Export]
    public class GetEntityById<T> : ProcessSystemMessage, IGetEntityById<T> where T : IEntity
    {
        bool isReadOnly = false;
        public void Create(int entityId)
        {
            
            if(isReadOnly) return;
            EntityId = entityId;
            isReadOnly = true;
        }
       
        public int EntityId { get; private set; }

        public GetEntityById(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
        }

       
    }
}
