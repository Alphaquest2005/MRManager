using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace SystemMessages
{
    [Export]
    public class SystemEntityProcessCompleted<TEntity> : EntityProcessSystemMessage<TEntity> where TEntity : IEntity
    {
        public SystemEntityProcessCompleted(ISystemEntityProcess<TEntity> process, IProcessSystemMessage msg) : base(process, msg)
        {
        }
    }
}