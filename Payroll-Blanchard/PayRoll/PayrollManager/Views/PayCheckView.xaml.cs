using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for PayCheckView.xaml
	/// </summary>
    public partial class PayCheckView : UserControl
    {
        public PayCheckView()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }



        private void PrintReport(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement rpt = (FrameworkElement)LayoutRoot;
            PrintClass.Print(ref rpt);
        }
    }
}