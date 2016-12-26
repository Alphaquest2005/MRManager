using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using RevolutionEntities.Process;

namespace SystemMessages
{
    [Export]
    public class SystemEntityProcessStarted<TEntity> : EntityProcessSystemMessage<TEntity> where TEntity : IEntity
    {
        public SystemEntityProcessStarted(ISystemEntityProcess<TEntity> process, IProcessSystemMessage msg) : base(process, msg)
        {
        }
    }
}