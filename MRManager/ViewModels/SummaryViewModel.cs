﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-ViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using SystemInterfaces;
using SystemMessages;
using Core.Common.UI;
using DataInterfaces;
using EventAggregator;
using EventMessages;
using Entity.Expressions;
using ViewModelInterfaces;

namespace ViewModels
{
	public partial class SummaryListViewModel<T> : BaseReadEntityViewModel<T>, ISummaryViewModel<T> where T:IEntity
        
	{
		//private static readonly SummaryListViewModel<T> _instance;
		// static SummaryListViewModel()
		//{
		//	_instance = null;
		//}

		//public static SummaryListViewModel<T> Instance
		//{
		//	get { return _instance; }
		//}
		

		private SummaryListViewModel(List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublications, List<IEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process):base(eventSubscriptions,eventPublications, commandInfo,process)
		{
			EventMessageBus.Current.GetEvent<EntitySetLoaded<T>>(MsgSource).Subscribe(x => HandleEntitySetUpdated(x.Entities));
			EventMessageBus.Current.GetEvent<ServiceStarted<LoadEntityView<T>>>(MsgSource)
			   .Subscribe(x => EventMessageBus.Current.Publish(new LoadEntityView<T>(AddressesExpressions.AddressesAutoViewExpression,typeof(T),x.Process, MsgSource), MsgSource));
		}

	}

}
