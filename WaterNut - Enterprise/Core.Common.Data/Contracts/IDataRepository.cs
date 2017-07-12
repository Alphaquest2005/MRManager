


namespace Core.Common.Contracts
{
    public interface IDataRepository
    {
        bool SaveChanges<T>(T obj) ;
        void Delete<T>(T obj);
    }
}
