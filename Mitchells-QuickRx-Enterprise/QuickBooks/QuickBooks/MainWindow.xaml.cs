using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //QBPOS qb = new QBPOS();
        private void InventoryItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            QBPOS.Instance.GetInventoryItemQuery();

        }

        private void SalesReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            QBPOS.Instance.DoSalesReceipt();
        }

        private void AddSalesReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            SalesReceipt s = new SalesReceipt();
            s.TxnDate = DateTime.Parse("1/4/2013");
            s.TxnState = "1";
            s.Workstation = "02";
            s.StoreNumber = "1";
            s.SalesReceiptNumber = "RX2353";
            s.Comments = "Joseph Bartholomew \n RX# 2353 \n Doctor: T.A. Marryshow";
            s.Associate = "Dispensary";
            s.SalesReceiptType = "0";

            s.SalesReceiptDetails.Add(new SalesReceiptDetail { ItemListID = "-5038534191369780991", QtySold = 10 });//340

            QBPOS.Instance.AddSalesReceipt(s);
        }


    }
}
