using Common.DataEntites;

namespace DataEntites
{
    public sealed class NullEntity<T>: BaseEntity where T : new()
    {
        private static readonly T instance;
        static NullEntity()
        {
            instance = new T();
        }

        public static T Instance => instance;
        public NullEntity()
        {
            Id = -1;
        }
    }
}
