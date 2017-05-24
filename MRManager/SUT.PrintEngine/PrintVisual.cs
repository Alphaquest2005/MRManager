using SUT.PrintEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SUT.PrintEngine
{
    public static class PrintVisual
    {
        public static void Print(ref Grid fwe, string PrinterName)
        {
            LocalPrintServer printServer = new LocalPrintServer();


            Size visualSize = new Size(fwe.ActualWidth, fwe.ActualHeight);
            

            DrawingVisual visual = PrintControlFactory.CreateDrawingVisual(fwe, fwe.ActualWidth, fwe.ActualHeight);


            SUT.PrintEngine.Paginators.VisualPaginator page = new SUT.PrintEngine.Paginators.VisualPaginator(visual, visualSize, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 0));
            page.Initialize(false);

            PrintDialog pd = new PrintDialog();
            pd.PrintQueue = printServer.GetPrintQueue(PrinterName);


            pd.PrintDocument(page, "");
        }

        public static DrawingVisual GetVisual(ref Grid fwe)
        {
            Size visualSize = new Size(fwe.ActualWidth, fwe.ActualHeight);
            return PrintControlFactory.CreateDrawingVisual(fwe, fwe.ActualWidth, fwe.ActualHeight);
        }
    }
}
