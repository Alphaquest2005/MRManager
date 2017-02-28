using System;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Common.Dynamic;
using Utilities;



namespace Common
{
    public static class EntityExtensions
    {
        public static T ApplyChanges<T>(this T entity, Dictionary < string, dynamic > changeTracking)// where T:IEntityId
        {
            return ApplyChanges(entity, changeTracking.ToList());
        }

        public static T ApplyChanges<T>(this T entity, List<KeyValuePair<string, dynamic>> changeTracking) //where T : IEntityId
        {
            foreach (var change in changeTracking)
            {
                ApplyChanges(entity, change);
            }
            return entity;
        }

        public static void ApplyChanges<T>(this T entity, KeyValuePair<string, dynamic> change) //where T : IEntityId
        {
            var prop = entity.GetType().GetProperty(change.Key.Replace(" ",""));
            if (prop == null) return;
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                prop?.SetValue(entity, NullableExtensions.ToNullable(change.Value, prop.PropertyType));
            }
            else
            {
                prop?.SetValue(entity, Convert.ChangeType(change.Value, prop.PropertyType));
            }
                
        }


        public static T ApplyChanges<T,TS>(this T entity, TS changedEntity)// where T : IEntityId where TS : IEntityId
        {

            foreach (var prop in changedEntity.GetType().GetProperties())
            {
                entity.GetType().GetProperty(prop.Name).SetValue(entity, prop.GetValue(changedEntity));
            }
            return entity;
        }

        public static Expando ApplyChanges<T>(this Expando entity, List<KeyValuePair<string, dynamic>> changeTracking) //where T : IEntityId
        {
            foreach (var change in changeTracking)
            {
                entity[change.Key] = change.Value;
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
