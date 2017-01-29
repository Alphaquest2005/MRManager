using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SystemInterfaces;
using Common.Dynamic;
using ReactiveUI;

namespace Common.DataEntites
{
   
    public abstract class EntityView<TEntity> :ReactiveObject, IEntityView<TEntity> where TEntity: IEntity
    {
        public Type EntityType => typeof(TEntity);

        private int _id;
        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        private readonly Guid _entityGuid = Guid.NewGuid();
        

        public override bool Equals(object obj)
        {
            
            var other = obj as BaseEntity;
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if(GetType() != other.GetType()) return false;
            if (Id == 0 || other.Id == 0) return false;
            return Id == other.Id;
        }

        public static bool operator ==(EntityView<TEntity> a, EntityView<TEntity> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(EntityView<TEntity> a, EntityView<TEntity> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once cuz of nhibernate
            return (_entityGuid.ToString()).GetHashCode();
        }

        
    }
}
