using System.Windows.Input;
using PayrollManager.Views;
using SUT.PrintEngine.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xaml;
using SUT.PrintEngine.ViewModels;
using XamlWriter = System.Windows.Markup.XamlWriter;

namespace PayrollManager
{
    public class PrintClass
    {
        public static void Print(ref FrameworkElement fwe)
        {
            
            DrawingVisual visual = PrintControlFactory.CreateDrawingVisual(fwe, fwe.ActualWidth, fwe.ActualHeight);
            SetUpPrint(fwe, visual);
        }

        private static void SetUpPrint(FrameworkElement fwe, DrawingVisual visual)
        {
            Size printSize;
            if (fwe.ActualWidth > 1056 )//&& fwe.ActualWidth <= 1344
            {
                printSize = new Size(1344, 816);
            }
            else if (fwe.ActualWidth > 816 && fwe.ActualWidth <= 1056)
            {
                printSize = new Size(1056, 816);
            }
            else
            {
                printSize = new Size(816, 1056);
            }

            var page = new SUT.PrintEngine.Paginators.VisualPaginator(visual, printSize,
                new Thickness(0, 50, 0, 50), new Thickness(0, 0, 0, 0));
            page.Initialize(false);

            var pd = new PrintDialog();

            if (pd.ShowDialog() == true)
            {
                if (fwe.ActualWidth > 816)
                {
                    pd.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
                }
                pd.PrintDocument(page, "");
            }
        }

        public static void PrintGrid(ref DataGrid fwe, string header)
        {
            var columnWidths = GetColumnWidths(fwe);

            var ht = new HeaderTemplate();
            var headerTemplate = XamlWriter.Save(ht);
            headerTemplate = headerTemplate.Replace("TitleHeader", header);
            var dt = Grid2Dt(fwe);
            

            var printControl = PrintControlFactory.Create(dt, columnWidths, headerTemplate);
            printControl.ShowPrintPreview();
           // var visual = printControl.DrawingVisual;

           // SetUpPrint(fwe,visual);
        }

        private static List<double> GetColumnWidths(DataGrid dataGrid)
        {
            var l = new List<double>();
            foreach (var c in dataGrid.Columns)
            {
                l.Add(c.ActualWidth);
            }
            return l;
        }

        private static DataTable Grid2Dt(DataGrid dataGrid)
        {
            dataGrid.SelectAllCells();
            dataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGrid);
            dataGrid.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;
        }


    }
}
