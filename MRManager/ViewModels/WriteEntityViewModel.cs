﻿//// <autogenerated>
////   This file was generated by T4 code generator MRManger-ViewModels.tt.
////   Any changes made to this file manually will be lost next time the file is regenerated.
//// </autogenerated>

//using System;
//using System.Collections.Generic;
//using SystemInterfaces;
//using Core.Common.UI;
//
//using ValidationSets;
//using ViewModel.Interfaces;

//namespace ViewModels
//{
//	public partial class WriteEntityViewModel<TEntity> : BaseWriteEntityViewModel<TEntity, WriteEntityViewModel<TEntity>>, IViewModel where TEntity : IEntity
//	{
	    
	   
//        public WriteEntityViewModel(Func<TEntity> createEntityAction, Func<TEntity> createNullEntityAction, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptionsActions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process): base(new EntityValidator<TEntity>(), process, eventSubscriptionsActions,eventPublications,commandInfo)
//		{
            
//            CreateEntityAction = createEntityAction;
//		    CreateNullEntityAction = createNullEntityAction;
//			CurrentEntity = CreateNullEntity();
//            this.WireEvents();

//            OnCreated();        
//			OnTotals();
//		}
			
//		partial void OnCreated();
//		partial void OnTotals();

//        private readonly Func<TEntity> CreateEntityAction = null;
//        private readonly Func<TEntity> CreateNullEntityAction = null;

//        protected override TEntity CreateEntity()
//        {

//            return (TEntity)CreateEntityAction.Invoke();
//        }

//        protected override sealed TEntity CreateNullEntity()
//        {
//            return (TEntity)CreateNullEntityAction.Invoke();
//        }

//    }
//}
