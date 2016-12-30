using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataInterfaces
{
    [InheritedExport]
    public interface IActorBackBone
    {
        void Intialize(IDataContext ctx, Assembly dbContextAssembly, Assembly entityAssembly);
    }
}
