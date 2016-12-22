using System.ComponentModel.Composition;


namespace DataInterfaces
{
    [InheritedExport]
    public interface IGetEntityById<T>
    {
       void Create(int entityId);
    }
}
