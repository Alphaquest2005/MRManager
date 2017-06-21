using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace DomainMessages
{
    [Export(typeof(IUpdatePatientEntityListWithChanges<>))]
    public class UpdatePatientEntityListWithChanges<TEntity> : ProcessSystemMessage, IUpdatePatientEntityListWithChanges<TEntity> where TEntity : IEntity
    {
        public UpdatePatientEntityListWithChanges() { }
        public UpdatePatientEntityListWithChanges(int entityId, string entityName, string attribute, string syntomName, string interviewName, Dictionary<string, object> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            Changes = changes;
            EntityId = entityId;
            EntityName = entityName;
            Attribute = attribute;
            SyntomName = syntomName;
            InterviewName = interviewName;
        }

        public Dictionary<string, object> Changes { get; }
        public int EntityId { get; }
        public string EntityName { get; }
        public string Attribute { get; }
        public string SyntomName { get; }
        public string InterviewName { get; }

        public Type ViewType => typeof(TEntity);
    }
}