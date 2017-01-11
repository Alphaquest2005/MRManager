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
        string Notes { get; }
    }

    [InheritedExport]
    public interface IStateEvent: IState
    {
        IStateCommand ExpectedCommand { get; }
    }

    [InheritedExport]
    public interface IStateCommand : IState
    {
       IStateEvent ExpectedEvent { get; }
    }

    
}
