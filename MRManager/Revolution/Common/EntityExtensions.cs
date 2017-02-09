using System;
using System.Collections.Generic;
using SystemInterfaces;



namespace Common
{
    public static class EntityExtensions
    {
        public static T ApplyChanges<T>(this T entity, Dictionary < string, dynamic > changeTracking) where T:IEntityId
        {
            
            foreach (var change in changeTracking)
            {
                entity.GetType().GetProperty(change.Key)?.SetValue(entity,change.Value);
            }
            return entity;
        }


        
        public static T ApplyChanges<T>(this T entity, dynamic changedEntity) where T : IEntityId
        {

            foreach (var prop in changedEntity.GetType().GetProperties())
            {
                entity.GetType().GetProperty(prop.Name).SetValue(entity, prop.GetValue(changedEntity));
            }
            return entity;
        }

        public static T DeepClone<T>(this T entity) where T:new()
        {
            var nt = Activator.CreateInstance(entity.GetType());
            foreach (var prop in entity.GetType().GetProperties())
            {
                prop.SetValue(nt,prop.GetValue(entity));
            }
            return (T) nt;
        }
    }
}
