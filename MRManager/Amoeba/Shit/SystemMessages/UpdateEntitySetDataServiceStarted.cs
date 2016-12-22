using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;


namespace SystemMessages
{
    [Export]
    public class UpdateEntitySetDataServiceStarted<T> : BaseMessage where T : IEntity
    {
        public UpdateEntitySetDataServiceStarted(MessageSource source) : base(source)
        {
        }
    }
}
