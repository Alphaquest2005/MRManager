﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Common.UI;
using CommonMessages;
using EventAggregator;
using EventMessages;
using Interfaces;
using Utilities;

namespace ViewModels
{
	public partial class SettingsViewModel_AutoGen 
	{
		MessageSource msgSource => new MessageSource(this.ToString());
		protected virtual void WireEvents()
		{
			EventMessageBus.Current.GetEvent<CurrentEntityChanged<IApplications>>(msgSource).Subscribe(x => handleApplicationIdChanged(x.EntityId));      
			EventMessageBus.Current.GetEvent<CurrentEntityChanged<ILayers>>(msgSource).Subscribe(x => handleLayerIdChanged(x.EntityId));      
			EventMessageBus.Current.GetEvent<CurrentEntityChanged<IProjects>>(msgSource).Subscribe(x => handleProjectIdChanged(x.EntityId));      
                  
		}
		private void handleApplicationIdChanged(int entityId)
		{
					
			if(entityId != EntityStates.NullEntity)
			if(CurrentEntity.Id == EntityStates.NullEntity || CurrentEntity.Id != entityId)
			{
				CurrentEntity = CreateNullEntity();
				FilterExpression = new List<Expression<Func<ISettings, bool>>>() {x => x.ApplicationId == entityId};
			}
		}
		private void handleLayerIdChanged(int entityId)
		{
					
			if(entityId != EntityStates.NullEntity)
			if(CurrentEntity.Id == EntityStates.NullEntity || CurrentEntity.Id != entityId)
			{
				CurrentEntity = CreateNullEntity();
				FilterExpression = new List<Expression<Func<ISettings, bool>>>() {x => x.LayerId == entityId};
			}
		}
		private void handleProjectIdChanged(int entityId)
		{
					
			if(entityId != EntityStates.NullEntity)
			if(CurrentEntity.Id == EntityStates.NullEntity || CurrentEntity.Id != entityId)
			{
				CurrentEntity = CreateNullEntity();
				FilterExpression = new List<Expression<Func<ISettings, bool>>>() {x => x.ProjectId == entityId};
			}
		}
 
	}
}
