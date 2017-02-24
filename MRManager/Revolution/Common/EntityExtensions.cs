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
                ApplyChanges(entity, change);
            }
            return entity;
        }

        public static void ApplyChanges<T>(this T entity, KeyValuePair<string, dynamic> change) where T : IEntityId
        {
            var prop = entity.GetType().GetProperty(change.Key);
            prop?.SetValue(entity, Convert.ChangeType(change.Value, prop.PropertyType));
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
