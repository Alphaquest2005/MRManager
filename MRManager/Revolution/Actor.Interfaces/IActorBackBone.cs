using System.ComponentModel.Composition;
using System.Reflection;

namespace Actor.Interfaces
{
    
    public interface IActorBackBone
    {
        void Intialize(Assembly dbContextAssembly, Assembly entityAssembly, bool autoRun);
    }
}
