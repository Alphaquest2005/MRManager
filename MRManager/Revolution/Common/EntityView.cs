using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SystemInterfaces;
using Common.Dynamic;
using ReactiveUI;

namespace Common.DataEntites
{
   
    public abstract class EntityView<TEntity>: IEntityView<TEntity> where TEntity: IEntity
    {
        public Type EntityType => typeof(TEntity);

       
        public int Id { get; set; }
        public DateTime EntryDateTime { get; private set; } = DateTime.Now;


        private readonly Guid _entityGuid = Guid.NewGuid();


        public bool Equals(TEntity other)
        {
            return Id == other?.Id;
        }
        public bool Equals(EntityView<TEntity> other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityView<TEntity>) obj);
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
            return Id;
        }


        

        
    }
}
