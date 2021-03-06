﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using SystemMessages;
using CommonMessages;
using EventAggregator;
using EventMessages;
using Interfaces;
using EF.Entities;
using Entity.Expressions;
using JB.Collections.Reactive;
using ReactiveUI;
using ViewModelInterfaces;

namespace ViewModels
{
	public partial class LayoutSummaryListViewModel : ReactiveObject, ISummaryViewModel<ILayoutAutoView>
	{
		private static readonly LayoutSummaryListViewModel _instance;
		 static LayoutSummaryListViewModel()
		{
			_instance = new LayoutSummaryListViewModel();
		}

		public static LayoutSummaryListViewModel Instance
		{
			get { return _instance; }
		}
		MessageSource msgSource => new MessageSource(this.ToString());

		private LayoutSummaryListViewModel()
		{
			
			EventMessageBus.Current.GetEvent<EntitySetLoaded<ILayoutAutoView>>(msgSource).Subscribe(x => handleEntitySetUpdated(x.Entities));
			EventMessageBus.Current.GetEvent<LoadEntityViewDataServiceStarted<ILayout>>(msgSource)
			   .Subscribe(x => EventMessageBus.Current.Publish(new LoadEntityView<ILayout>(LayoutExpressions.LayoutAutoViewExpression,typeof(ILayoutAutoView), typeof(LayoutAutoView), msgSource), msgSource));
		}

		private void handleEntitySetUpdated(IList<ILayoutAutoView> entities)
		{
			EntitySet = new ObservableList<ILayoutAutoView>(entities);
		}

		private ObservableList<ILayoutAutoView> _EntitySet = new ObservableList<ILayoutAutoView>();
		public ObservableList<ILayoutAutoView> EntitySet
		{
			get
			{
				return _EntitySet;
			}
			set
			{
				this.RaiseAndSetIfChanged(ref _EntitySet, value ?? new ObservableList<ILayoutAutoView>());
			}
		}

		private ILayoutAutoView _currentEntity;

		public ILayoutAutoView CurrentEntity
		{
			get { return _currentEntity; }
			set
			{
				if (!Equals(_currentEntity, value))
				{
					this.RaiseAndSetIfChanged(ref _currentEntity, value); //value == null? CreateEntity():value
					if (_currentEntity != null) EventMessageBus.Current.Publish(new CurrentEntityChanged<ILayout>(_currentEntity.Id,msgSource), msgSource);
				}
			}
		}

	}

}
