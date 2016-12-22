using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    [Export]
    public class GetEntityById<T> : BaseMessage, IGetEntityById<T> where T : IEntity
    {
        bool isReadOnly = false;
        public void Create(int entityId)
        {
            
            if(isReadOnly) return;
            EntityId = entityId;
            isReadOnly = true;
        }
       
        public int EntityId { get; private set; }

        public GetEntityById(MessageSource source) : base(source)
        {
        }

       
    }
}
