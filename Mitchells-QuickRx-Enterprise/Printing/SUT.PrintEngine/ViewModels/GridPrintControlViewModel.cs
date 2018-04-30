using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using SUT.PrintEngine.Paginators;
using SUT.PrintEngine.Views;

namespace SUT.PrintEngine.ViewModels
{
    public class GridPrintControlViewModel : ItemsPrintControlViewModel, IGridPrintControlViewModel
    {
        public GridPrintControlViewModel(PrintControlView view, IUnityContainer unityContainer)
            : base(view, unityContainer)
        {
        }
        protected override void CreatePaginator(DrawingVisual visual, Size printSize)
        {
            if (PrintUtility != null)
                Paginator = new DataGridPaginator(visual, printSize, PrintUtility.GetPageMargin(CurrentPrinterName), PrintTableDefination);
        }
    }
}