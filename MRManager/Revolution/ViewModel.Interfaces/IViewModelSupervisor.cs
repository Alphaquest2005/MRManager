using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartUp.Messages;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewModelSupervisor : IService<IViewModelSupervisor>
    {
    }
}
