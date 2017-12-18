using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PrintableListView
{
    public class DocumentPaginatorSource : IDocumentPaginatorSource
    {
        public DocumentPaginator DocumentPaginator { get; private set; }

        public DocumentPaginatorSource(DocumentPaginatorExtention documentPaginator)
        {
            this.DocumentPaginator = documentPaginator;
        }
    }

    public class DocumentPaginatorExtention : DocumentPaginator
    {
        protected double pageHeight;
        protected Thickness PageMargin;
        protected DocumentPaginatorSource documentPaginatorSource;
        protected Size headerSize;
        protected Size footerSize;
        protected PrintableListView printableListView;
        protected CollectionViewSource source;
        protected ListCollectionView sourceView;
        protected int groupIndex;
        protected int itemIndex;
        protected List<DocumentPage> Pages;
        protected int currentGroupPageCount;
        //protected double PrintablePageWidth;
        //protected double PrintablePageHeight;

        public DocumentPaginatorExtention(PrintableListView listview, Thickness margin, Size printPageArea)
        {
            printableListView = listview;
            PageMargin = margin;
            pageHeight = printPageArea.Height - PageMargin.Top - PageMargin.Bottom;
            PageSize = printPageArea;


            headerSize = listview.HeaderSize;
            footerSize = listview.FooterSize;

            if (listview.PageHeaderTemplate != null)
            {
                pageHeight -= headerSize.Height;
            }

            if (listview.PageFooterTemplate != null)
            {
                pageHeight -= footerSize.Height;
            }
           
            
            if (listview.ReportBody == null)
                    listview.ReportBody = UIUtility.CreateDeepCopy(listview.View);
            for (int index = 0; index < (listview.View as GridView).Columns.Count; index++)
            {
                var col = (listview.View as GridView).Columns[index];
                (listview.ReportBody as GridView).Columns[index].Width = col.ActualWidth;
            }
            ListCollectionView originalData = new ListCollectionView((IList)listview.ItemsSource);// (ListCollectionView)listview.ItemsSource;
            CollectionViewSource source = new CollectionViewSource();
            source.Source = originalData.SourceCollection;//(listview.ItemsSource as ListCollectionView)
            if (printableListView.Group != null && printableListView.Group.GroupDescriptions != null)
            {
                foreach (GroupDescription groupDescription in printableListView.Group.GroupDescriptions)
                {
                    source.GroupDescriptions.Add(groupDescription);
                }
            }

            if (printableListView.Group != null)
            {
                printableListView.Group.groupIndex = 0;
                printableListView.Group.groupPage = 0;
            }

            sourceView = source.View as ListCollectionView;
            createPages();
            pageCount = Pages.Count;
            isPageCountValid = true;
            documentPaginatorSource = new DocumentPaginatorSource(this);

            sourceView.MoveCurrentToFirst();
            groupIndex = 0;
            currentGroupPageCount = 0;
        }

        public override bool IsPageCountValid
        {
            get { return isPageCountValid; }
        }
        bool isPageCountValid = false;

        public override int PageCount
        {
            get { return pageCount; }
        }
        private int pageCount;

        public override Size PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private Size pageSize;

        public override IDocumentPaginatorSource Source
        {
            get { return documentPaginatorSource; }
        }

        protected virtual void createPages()
        {
            Pages = new List<DocumentPage>();

            int count = 0;

            if (printableListView.Group != null && printableListView.Group.GroupDescriptions != null)
            {
                foreach (CollectionViewGroup group in sourceView.Groups)
                {
                    while (itemIndex <= group.ItemCount - 1 && printableListView.Group.groupIndex < sourceView.Groups.Count)
                    {
                        DocumentPage page = CreatePage(count);
                        Pages.Add(page);
                        count++;
                    }
                }
            }
            else
            {
                while (itemIndex <= sourceView.Count - 1)
                {
                    DocumentPage page = CreatePage(count);
                    Pages.Add(page);
                    count++;
                }
            }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            return Pages[pageNumber];
        }

        protected virtual DocumentPage CreatePage(int pageNumber)
        {
            // The index of header in the Grid
            int pageHeaderRowIndex = 0;
            int listViewRowIndex = 1;
            int pageFooterRowIndex = 2;

            // Create Page Layout
            Page page = new Page();
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.RowDefinitions[pageHeaderRowIndex].Height = new GridLength(headerSize.Height);
            grid.RowDefinitions[listViewRowIndex].Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions[pageFooterRowIndex].Height = new GridLength(footerSize.Height);

            // Add ListView
            BindingList<object> list;
            Rect rect = new Rect(0, 0, PageSize.Width - PageMargin.Left - PageMargin.Right, pageHeight);
            if (printableListView.Group != null && printableListView.Group.GroupDescriptions != null)
            {
                FrameworkElement body = printableListView.Group.Render(printableListView.DataContext, sourceView, printableListView.ReportBody, 50, ref itemIndex, rect, out list);
                grid.Children.Add(body);
                Grid.SetRow(body, listViewRowIndex);
            }
            else
            {
                IList sourceList = sourceView.SourceCollection.Cast<object>().ToList();
                FrameworkElement body = UIUtility.createListView(sourceList, printableListView.ReportBody, 50, ref itemIndex, rect, out list);
                grid.Children.Add(body);
                Grid.SetRow(body, listViewRowIndex);
            }

            // Add Page Header
            ContentControl header = new ContentControl();
            header.Height = headerSize.Height;
            header.ContentTemplate = printableListView.PageHeaderTemplate;
            grid.Children.Add(header);
            Grid.SetRow(header, pageHeaderRowIndex);

            // Add Page Footer
            ContentControl footer = new ContentControl();
            footer.Height = footerSize.Height;
            footer.ContentTemplate = printableListView.PageFooterTemplate;
            grid.Children.Add(footer);
            Grid.SetRow(footer, pageFooterRowIndex);
            page.Content = grid;

            HeaderFooterDataContext headerFooterDataContext = new HeaderFooterDataContext(printableListView.DataContext, pageNumber + 1, list);
            header.Content = headerFooterDataContext;
            footer.Content = headerFooterDataContext;

            page.Arrange(new Rect(PageMargin.Left, PageMargin.Top, PageSize.Width - PageMargin.Left - PageMargin.Right, PageSize.Height - PageMargin.Top - PageMargin.Bottom));

            return new DocumentPage(page);
        }
    }
}
