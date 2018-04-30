using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesTaskPad
{
    /// <summary>
    /// Interaction logic for SalesTaskPadView.xaml
    /// </summary>
    public partial class SalesTaskPadView : UserControl
    {
        public SalesTaskPadView(SalesTaskPadVM salesTaskPadVM)
        {
            InitializeComponent();

            // Setup the view model context
            DataContext = salesTaskPadVM;
        }
    }
}
