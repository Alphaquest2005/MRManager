

namespace Core.Common.Data.Contracts
{
   public interface ICreateEntityFromString<T> where T: IEntity
    {
        T CreateEntityFromString(string value);
    }
}
