using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewRowStateChanged : IProcessSystemMessage
    {
        IViewModel ViewModel { get; }
        RowState RowState { get; }
    }
}
