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
	/// Interaction logic for AccountDetails.xaml
	/// </summary>
	public partial class InstitutionDetails : UserControl
	{
        public InstitutionDetails()
		{
			this.InitializeComponent();
            im = (InstitutionDetailsModel)this.FindResource("InstitutionDetailsModelDataSource");
            // Insert code required on object creation below this point.
        }

        InstitutionDetailsModel im;
        private void NewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.NewInstitution();
        }

        private void SaveBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
                im.SaveInstitution();
            
        }

        private void DeleteBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            im.DeleteInstition();
        }

     
	}
}