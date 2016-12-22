using System.Linq;

namespace DesignTimeData
{
    public partial class DesignDataContext
    {
        public static T SampleData<T>()
        {
            var res = typeof(DesignDataContext).GetProperties().FirstOrDefault(x => x.PropertyType == typeof(T));
            if (res == null) return default(T);
            return (T)res.GetValue(null, null);
        }
    }
}
