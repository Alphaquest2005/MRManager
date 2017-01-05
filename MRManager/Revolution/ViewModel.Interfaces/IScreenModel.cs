using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IScreenModel : IScreenLayoutViewModel
    {
    }
}
