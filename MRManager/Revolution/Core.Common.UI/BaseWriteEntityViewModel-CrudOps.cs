//using System;
//using System.Linq;
//using System.Reactive;
//using System.Windows;
//using Common;
//
//using EventAggregator;
//using EventMessages;
//using ReactiveUI;

//namespace Core.Common.UI
//{
//    public abstract partial  class BaseWriteEntityViewModel<TEntity,TViewModel> where TEntity:IEntity
//    {
       
//        public ReactiveCommand NewEntity { get; protected set; }
//        public ReactiveCommand DeleteEntity { get; protected set; }
//        public ReactiveCommand SaveChanges { get; protected set; }

//        public ReactiveCommand EditEntity { get; protected set; }


        

//        partial void CrudeOpsContructor()
//        {
//            ReactiveCommand<Unit, Unit> reactiveCommand = ReactiveCommand.Create(HandleSaveChanges,ChangeTracking.WhenAny(x => x.Count, x => x.Value > 0));
//            SaveChanges = reactiveCommand;//
           

            
//            NewEntity = ReactiveCommand.Create(HandleCreateEntity);//this.WhenAny(x => x.entity.Id, x => x.Value != 0)
          

//            EditEntity = ReactiveCommand.Create(HandleEditEntity);//this.WhenAny(x => x.entity.Id, x => x.Value != 0)
           

//            DeleteEntity = ReactiveCommand.Create(HandleDeleteEntity,this.WhenAny(x => x.CurrentEntity.Id, x => x.Value != 0));//
            
//        }

//        private void HandleEditEntity()
//        {
//            if (CurrentEntity == null) return;
//            RowState = RowState != RowState.Modified?RowState.Modified: RowState.Unchanged;
//            this.RaisePropertyChanged(nameof(RowState));
//        }

//        private RowState _rowState;
//        public RowState RowState
//        {
//            get { return _rowState; }
//            set { this.RaiseAndSetIfChanged(ref _rowState,value); }
//        }

//        private void HandleSaveChanges()
//        {
//            try
//            {
//                if (!ChangeTracking.Any()) return;

//                CurrentEntityWithChanges.ApplyChanges(ChangeTracking.ToDictionary(x => x.Key, x => x.Value));
//                ValidationResults = Validator.Validate(CurrentEntityWithChanges);
//                if (ValidationResults != null && !ValidationResults.IsValid) return;
//                if (CurrentEntity.RowState == RowState.Added)
//                {
//                    EventMessageBus.Current.Publish(new EntityCreated<TEntity>(CurrentEntityWithChanges,Process, MsgSource), MsgSource);
//                }
//                else
//                    EventMessageBus.Current.Publish(
//                        new EntityChanges<TEntity>(CurrentEntity.Id, ChangeTracking.ToDictionary(x => x.Key, x => x.Value),
//                            Process, MsgSource), MsgSource);
//                //ChangeTracking.Clear();
//            }
//            catch (Exception)
//            {

//                throw;
//            }

//        }

//        private void HandleCreateEntity()
//        {
//            try
//            {
//                HandleSaveChanges();
//                var itm = CreateEntity();
//                EntitySet.Add(itm);
//                CurrentEntity = itm;
                
//            }
//            catch (Exception)
//            {

//                throw;
//            }


//        }

//        private void HandleEntityCreated(TEntity entity)
//        {
//            try
//            {
//                if (EntitySet == null) return;
//                var oldEntity = EntitySet.FirstOrDefault(x => x.Id == 0 || x.RowState == RowState.Added);
//                if (oldEntity == null) return;
//                Application.Current.Dispatcher.Invoke(() =>
//                {
//                    ReplaceItemInEntitySet(entity, oldEntity);
//                    //ce.ApplyChanges(entity);
//                    CurrentEntity = entity;
//                });

//            }
//            catch (Exception)
//            {

//                throw;
//            }
            
//        }

//        void HandleDeleteEntity()
//        {
//            EventMessageBus.Current.Publish(new DeleteEntity<TEntity>(CurrentEntity.Id,Process, MsgSource), MsgSource);
//        }

//        protected abstract TEntity CreateEntity();
//        protected abstract TEntity CreateNullEntity();

//        private void HandleEntityDeleted(int entityId)
//        {
//            try
//            {

//                if (EntitySet == null) return;
//                var ce = EntitySet.FirstOrDefault(x => x.Id == entityId);
//                if (ce == null) return;
//                Application.Current.Dispatcher.Invoke(new Action(() =>
//                {
//                     EntitySet.Remove(ce);
//                    if (CurrentEntity.Id == entityId) CurrentEntity = default(TEntity);
                   
//                }));

//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
