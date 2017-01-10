using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IState
    {
        string Name { get; }
        string Status { get; }
    }

    [InheritedExport]
    public interface IStateWithNotes: IState
    {
        string Notes { get; }
    }

}
