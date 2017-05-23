using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using System;

namespace DomainMessages
{
    [Export(typeof(IUpdatePatientEntityWithChanges<>))]
    public class UpdatePatientEntityWithChanges<TEntity> : ProcessSystemMessage, IUpdatePatientEntityWithChanges<TEntity> where TEntity : IEntity
    {
        public UpdatePatientEntityWithChanges(){}
       public UpdatePatientEntityWithChanges(int entityId, string entityName,string syntomName,string interviewName, Dictionary<string, object> changes, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source): base(processInfo, process, source)
        {
            Changes = changes;
            EntityId = entityId;
            EntityName = entityName;
           SyntomName = syntomName;
           InterviewName = interviewName;
        }

        public Dictionary<string, object> Changes { get; }
        public int EntityId { get; }
        public string EntityName { get; }
        public string SyntomName { get; }
        public string InterviewName { get; }

        public Type ViewType => typeof (TEntity);
    }
}