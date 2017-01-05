using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SystemInterfaces;
using Common.Dynamic;

namespace Common.DataEntites
{
   
    public abstract class BaseEntityView<TEntity> : IEntityView<TEntity> where TEntity:IEntity
    {
        public virtual int Id { get; set; }

        [IgnoreDataMember]
        [NotMapped]
        public Type EntityType => typeof(TEntity);

        [IgnoreDataMember]
        [NotMapped]
        public virtual RowState RowState { get; set; } = RowState.Loaded ;
        [IgnoreDataMember]
        [NotMapped]
        public virtual dynamic ComputedProperties { get; set; } = new Expando();


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

        public static bool operator ==(BaseEntityView<TEntity> a, BaseEntityView<TEntity> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(BaseEntityView<TEntity> a, BaseEntityView<TEntity> b)
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
