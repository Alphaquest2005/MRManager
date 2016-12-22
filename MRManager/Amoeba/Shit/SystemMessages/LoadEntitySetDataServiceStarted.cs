using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;


namespace SystemMessages
{
    [Export]
    public class LoadEntitySetDataServiceStarted<T> : BaseMessage where T : IEntity
    {
        public LoadEntitySetDataServiceStarted(MessageSource source) : base(source)
        {
        }
    }
}
