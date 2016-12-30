﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IDynamicViewModel<out TViewModel> : IViewModel where TViewModel : IViewModel
    {
        TViewModel Instance { get; }
    }
}
