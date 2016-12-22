using System;
using System.Collections.Concurrent;

namespace PropertyExtensions
{
    public class ValueStore<T>
    {
        ConcurrentDictionary<ExtensionProperty<T>, T> Values { get; } =
            new ConcurrentDictionary<ExtensionProperty<T>, T>();

        public T Add(ExtensionProperty<T> property, T value)
        {
            if (!Values.TryAdd(property, value))
                throw new RedefinedPropertyException(property);

            return value;
        }

        public T Get(ExtensionProperty<T> property)
        {
            T value;
            if (!Values.TryGetValue(property, out value))
                return default(T);//throw new UndefinedPropertyException(property);

            return value;
        }

        public T GetOrAdd(ExtensionProperty<T> property, Func<T> valueFactory) =>
            Values.GetOrAdd(property, p => valueFactory());
    }

    public class ValueStore
    {
        ConcurrentDictionary<Type, object> Stores { get; } =
            new ConcurrentDictionary<Type, object>();

        ValueStore<T> Store<T>() =>
            (ValueStore<T>)Stores.GetOrAdd(typeof(T), t => new ValueStore<T>());

        public T Add<T>(
            ExtensionProperty<T> property,
            T value) =>
                Store<T>()
                    .Add(property, value);

        public T Get<T>(
            ExtensionProperty<T> property) =>
                Store<T>()
                    .Get(property);

        public T GetOrAdd<T>(
            ExtensionProperty<T> property,
            Func<T> valueFactory) =>
                Store<T>()
                    .GetOrAdd(property, valueFactory);
    }
}