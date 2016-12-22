using System.ComponentModel.Composition;
using System.Reflection;
using DataInterfaces;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IActorBackBone
    {
        void Intialize(IDataContext ctx, Assembly dbContextAssembly, Assembly entityAssembly);
    }
}
