﻿//// <autogenerated>
////   This file was generated by T4 code generator Amoeba-Master.tt.
////   Any changes made to this file manually will be lost next time the file is regenerated.
//// </autogenerated>


//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using SystemInterfaces;
//using Core.Common.UI;
//using DataInterfaces;
//using DesignTime;
//using ViewModel.Interfaces;

//namespace ViewModels
//{
//	public partial class CacheViewModel<T> : ReadEntityViewModel<T> where T:IEntity
//	{
//	   public CacheViewModel(ISystemProcess process,
//			List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo)
//			: base(eventSubscriptions, eventPublications,commandInfo, process)
//		{
//			this.WireEvents();
//			if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime)
//			{
//				base.EntitySet.Add(DesignDataContext.SampleData<T>());
//			}
//		}

//		public void HandleEntitySetLoaded(IEnumerable<T> entities)
//		{
//			System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
//			{
//				base.EntitySet.Clear();
//				foreach (var itm in entities)
//				{
//					EntitySet.Add(itm);
//				}
//				//((IReactiveObject) new BaseViewModel()).RaisePropertyChanged();
//			}));
//		}

//		public void HandleCurrentEntityUpdated(T entity)
//		{
//			System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
//			{
//				CurrentEntity = entity;
			   
//			}));
//		}

//		public void HandleEntityCreated(T entity)
//		{
//			System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
//			{
//				EntitySet.Add(entity);

//			}));
//		}

//		public void HandleEntityDeleted(int entityId)
//		{
//			System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
//			{
//				var entity = EntitySet.FirstOrDefault(x => x.Id == entityId);
//				if (entity == null) return;
//				EntitySet.Remove(entity);

//			}));
//		}

//		public void HandleCurrentEntityChanged(int entityId)
//		{
//			System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
//			{
//				CurrentEntity = EntitySet.FirstOrDefault(x => x.Id == entityId);
//			}));
//		}



	   
//	}
//}
