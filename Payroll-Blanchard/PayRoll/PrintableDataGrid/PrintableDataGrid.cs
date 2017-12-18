using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PrintableListView
{
    public class PrintableListView : ListView
    {
        #region Properties

        public ReportGroup Group
        {
            get
            {
                return (ReportGroup)this.GetValue(GroupProperty);
            }
            set
            {
                this.SetValue(GroupProperty, value);
            }
        }
        public static DependencyProperty GroupProperty =
            DependencyProperty.Register("Group", typeof(ReportGroup), typeof(PrintableListView), new PropertyMetadata());

        public ViewBase ReportBody
        {
            get
            {
                return (ViewBase)this.GetValue(ReportBodyProperty);
            }
            set
            {
                this.SetValue(ReportBodyProperty, value);
            }
        }
        public static DependencyProperty ReportBodyProperty =
            DependencyProperty.Register("ReportBody", typeof(ViewBase), typeof(PrintableListView), new PropertyMetadata());

        public DataTemplate PageHeaderTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(PageHeaderTemplateProperty);
            }
            set
            {
                this.SetValue(PageHeaderTemplateProperty, value);
            }
        }
        public static DependencyProperty PageHeaderTemplateProperty =
            DependencyProperty.Register("PageHeaderTemplate", typeof(DataTemplate), typeof(PrintableListView), new PropertyMetadata());

        public DataTemplate PageFooterTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(PageFooterTemplateProperty);
            }
            set
            {
                this.SetValue(PageFooterTemplateProperty, value);
            }
        }
        public static DependencyProperty PageFooterTemplateProperty =
            DependencyProperty.Register("PageFooterTemplate", typeof(DataTemplate), typeof(PrintableListView), new PropertyMetadata());

        public Size PageSize
        {
            get
            {
                return (Size)this.GetValue(PageSizeProperty);
            }
            set
            {
                this.SetValue(PageSizeProperty, value);
            }
        }
        public static DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(Size), typeof(PrintableListView), new PropertyMetadata());

        public Size HeaderSize
        {
            get
            {
                return (Size)this.GetValue(HeaderSizeProperty);
            }
            set
            {
                this.SetValue(HeaderSizeProperty, value);
            }
        }
        public static DependencyProperty HeaderSizeProperty =
            DependencyProperty.Register("HeaderSize", typeof(Size), typeof(PrintableListView), new PropertyMetadata());

        public Size FooterSize
        {
            get
            {
                return (Size)this.GetValue(FooterSizeProperty);
            }
            set
            {
                this.SetValue(FooterSizeProperty, value);
            }
        }
        public static DependencyProperty FooterSizeProperty =
            DependencyProperty.Register("FooterSize", typeof(Size), typeof(PrintableListView), new PropertyMetadata());

        public ICommand PrintCommand
        {
            get
            {
                return printCommand;
            }
            set
            {
                printCommand = value;
            }
        }
        private ICommand printCommand;

        #endregion

        #region Constructor

        public PrintableListView()
            : base()
        {
            PrintCommand = new DelegateCommand<object>(print);
        }

        #endregion

        #region Private Members

        private void print(object parameter)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                //if (PageSize == null)
                //{
                //    PageSize = new Size((int)printDialog.PrintableAreaWidth, (int)printDialog.PrintableAreaHeight);
                //}
               
                if (this.ActualWidth > 1056 && ActualWidth <= 1344)
                {
                    PageSize = new Size(1344, 816);
                    printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLegal,816,1344);
                    printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
                    

                }
                else if (ActualWidth > 816 && ActualWidth <= 1056)
                {
                    PageSize = new Size(1056, 816);
                }
                else
                {
                    PageSize = new Size(816, 1056);
                }

                DocumentPaginatorExtention documentPaginatorExtention = new DocumentPaginatorExtention(this, new Thickness(5), PageSize);
                printDialog.PrintDocument(documentPaginatorExtention, "");
            }
        }

        #endregion
    }
}
