using SUT.PrintEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace PayrollManager
{
    public class WPF2PDF
    {
        private static int PixelsPerInch = 96;
        private static double PaperWidth = 8.5;
        private static int PaperHeight = 28;

        public static string CreatePDF(ref Grid rpt, string reportName)
        {

            
            
            XpsDocumentWriter writer;
            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            DrawingVisual v = PrintVisual.GetVisual(ref rpt);
            // create XPS file based on a WPF Visual, and store it in a memorystream

            if (rpt.ActualWidth > PaperWidth)
            {


                PageContent pageCnt = new PageContent();
                FixedPage page;
                // var oldParent = RemoveChild(rpt);
                page = new FixedPage() { Height = rpt.ActualHeight, Width = rpt.ActualWidth, };// {Height = (PaperWidth*PixelsPerInch), Width = (PaperHeight*PixelsPerInch), };
                RenderTargetBitmap bmp = new RenderTargetBitmap((int) rpt.ActualWidth, (int) rpt.ActualHeight,0,0, PixelFormats.Pbgra32);
                bmp.Render(v);

                Image image = new Image();
                image.Source = bmp;
                page.Children.Add(image);
                // ((System.Windows.Markup.IAddChild) pageCnt).AddChild(page);


                writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(page);
                
                //page.Children.Remove(rpt);
                //AddChild(rpt, oldParent);
            }
            else
            {
                
                writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(v);
            }


            doc.Close();
            package.Close();

            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            string file = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), reportName + ".pdf");

            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, file, 0);

            return file;
        }

        private static void AddChild(Grid rpt, dynamic oldParent)
        {
            if (oldParent is ContentControl) oldParent.Content = rpt;
            if(oldParent is Panel) oldParent.Children.Add(rpt);
        }

        private static dynamic RemoveChild(Grid rpt)
        {
            dynamic oldParent;
            oldParent = LogicalTreeHelper.GetParent(rpt) as ContentControl;
            if (oldParent != null)
            {
                oldParent.Content = null;
                return oldParent;
            }

            oldParent = LogicalTreeHelper.GetParent(rpt) as Panel;
            if (oldParent != null)
            {
                oldParent.Children.Remove(rpt);
                return oldParent;
            }



            return null;
        }

        public static void CreateAndOpenPDF(ref Grid grd, string reportName)
        {

            var rptfile = CreatePDF(ref grd, "PayrollItemBreakDown");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = rptfile;
            process.Start();
            process.WaitForExit();
        }
    }
}
