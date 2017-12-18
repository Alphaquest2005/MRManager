using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;

namespace PrintableListView
{
    public class ReportGroup : DependencyObject
    {
        public ReportGroup()
        {
            GroupDescriptions = new ObservableCollection<GroupDescription>();
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get
            {
                return (ObservableCollection<GroupDescription>)this.GetValue(GroupDescriptionsProperty);
            }
            set
            {
                this.SetValue(GroupDescriptionsProperty, value);
            }
        }
        public static DependencyProperty GroupDescriptionsProperty =
            DependencyProperty.Register("GroupDescriptions", typeof(ObservableCollection<GroupDescription>), typeof(ReportGroup), new PropertyMetadata());

        public DataTemplate GroupFooter
        {
            get
            {
                return (DataTemplate)this.GetValue(GroupFooterProperty);
            }
            set
            {
                this.SetValue(GroupFooterProperty, value);
            }
        }
        public static DependencyProperty GroupFooterProperty =
            DependencyProperty.Register("GroupFooter", typeof(DataTemplate), typeof(ReportGroup), new PropertyMetadata());

        public DataTemplate GroupHeader
        {
            get
            {
                return (DataTemplate)this.GetValue(GroupHeaderProperty);
            }
            set
            {
                this.SetValue(GroupHeaderProperty, value);
            }
        }
        public static DependencyProperty GroupHeaderProperty =
            DependencyProperty.Register("GroupHeader", typeof(DataTemplate), typeof(ReportGroup), new PropertyMetadata());

        public DataTemplate GroupPageFooter
        {
            get
            {
                return (DataTemplate)this.GetValue(GroupPageFooterProperty);
            }
            set
            {
                this.SetValue(GroupPageFooterProperty, value);
            }
        }
        public static DependencyProperty GroupPageFooterProperty =
            DependencyProperty.Register("GroupPageFooter", typeof(DataTemplate), typeof(ReportGroup), new PropertyMetadata());

        public DataTemplate GroupPageHeader
        {
            get
            {
                return (DataTemplate)this.GetValue(GroupPageHeaderProperty);
            }
            set
            {
                this.SetValue(GroupPageHeaderProperty, value);
            }
        }
        public static DependencyProperty GroupPageHeaderProperty =
            DependencyProperty.Register("GroupPageHeader", typeof(DataTemplate), typeof(ReportGroup), new PropertyMetadata());

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
            DependencyProperty.Register("HeaderSize", typeof(Size), typeof(ReportGroup), new PropertyMetadata());

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
            DependencyProperty.Register("FooterSize", typeof(Size), typeof(ReportGroup), new PropertyMetadata());

        public Size PageHeaderSize
        {
            get
            {
                return (Size)this.GetValue(PageHeaderSizeProperty);
            }
            set
            {
                this.SetValue(PageHeaderSizeProperty, value);
            }
        }
        public static DependencyProperty PageHeaderSizeProperty =
            DependencyProperty.Register("PageHeaderSize", typeof(Size), typeof(ReportGroup), new PropertyMetadata());

        public Size PageFooterSize
        {
            get
            {
                return (Size)this.GetValue(PageFooterSizeProperty);
            }
            set
            {
                this.SetValue(PageFooterSizeProperty, value);
            }
        }
        public static DependencyProperty PageFooterSizeProperty =
            DependencyProperty.Register("PageFooterSize", typeof(Size), typeof(ReportGroup), new PropertyMetadata());

        internal int groupIndex;
        internal int groupPage;

        internal FrameworkElement Render(object parentDataContext, CollectionView sourceView, ViewBase view, int itemsApproximation, ref int itemIndex, Rect size, out BindingList<object> list)
        {
            // The index of the grid row of the ListView
            int listViewRowIndex = 0;
            double pageHeight = size.Height;
            
            // The layout of the group
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions[listViewRowIndex].Height = new GridLength(1, GridUnitType.Star);

            CollectionViewGroup group = sourceView.Groups[groupIndex] as CollectionViewGroup;

            // Add header
            if (groupPage == 0 && GroupHeader != null)
            {
                ContentControl groupHeaderControl = new ContentControl();
                groupHeaderControl.ContentTemplate = GroupHeader;
                groupHeaderControl.Height = HeaderSize.Height;
                pageHeight -= HeaderSize.Height;
                groupHeaderControl.Content = new HeaderFooterDataContext(parentDataContext, group.Items);

                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(HeaderSize.Height, GridUnitType.Pixel);
                grid.RowDefinitions.Insert(0, row);
                grid.Children.Add(groupHeaderControl);
                Grid.SetRow(groupHeaderControl, 0);
                listViewRowIndex++;
            }

            // Page header
            if (GroupPageHeader != null)
            {
                pageHeight -= this.PageHeaderSize.Height;
            }

            // Page footer
            if (GroupPageFooter != null)
            {
                pageHeight -= this.PageFooterSize.Height;
            }

            list = new BindingList<object>();
            ListView listview = UIUtility.createListView(group.Items, UIUtility.CreateDeepCopy<ViewBase>(view), itemsApproximation, ref itemIndex, new Rect(size.Left, size.Top, size.Width, pageHeight), out list);

            HeaderFooterDataContext headerFooterContent = new HeaderFooterDataContext(parentDataContext, list);
            headerFooterContent.IsFirstPage = groupPage == 0;

            // group page header
            if (GroupPageHeader != null)
            {
                ContentControl groupPageHeaderControl = new ContentControl();
                groupPageHeaderControl.ContentTemplate = GroupPageHeader;
                groupPageHeaderControl.Height = this.PageFooterSize.Height;
                pageHeight -= this.PageHeaderSize.Height;
                groupPageHeaderControl.Content = headerFooterContent;

                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(PageHeaderSize.Height, GridUnitType.Pixel);
                grid.RowDefinitions.Insert(listViewRowIndex, row);
                grid.Children.Add(groupPageHeaderControl);
                Grid.SetRow(groupPageHeaderControl, listViewRowIndex);
                listViewRowIndex++;
            }

            // group page footer
            if (GroupPageFooter != null)
            {
                ContentControl groupPageFooterControl = new ContentControl();
                groupPageFooterControl.ContentTemplate = GroupPageFooter;
                groupPageFooterControl.Height = this.PageFooterSize.Height;
                pageHeight -= this.PageHeaderSize.Height;
                groupPageFooterControl.Content = headerFooterContent;

                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(PageFooterSize.Height, GridUnitType.Pixel);
                grid.RowDefinitions.Insert(listViewRowIndex + 1, row);
                grid.Children.Add(groupPageFooterControl);
                Grid.SetRow(groupPageFooterControl, listViewRowIndex + 1);
            }

            // Group footer
            if (itemIndex >= group.ItemCount - 1)
            {
                headerFooterContent.IsLastPage = true;
                if (GroupFooter != null &&
                    listview.ActualHeight + FooterSize.Height <= pageHeight)
                {
                    groupPage = 0;
                    groupIndex++;
                    itemIndex = 0;
                    ContentControl groupFooterControl = new ContentControl();
                    groupFooterControl.ContentTemplate = GroupHeader;
                    groupFooterControl.Height = FooterSize.Height;
                    pageHeight -= HeaderSize.Height;
                    groupFooterControl.Content = new HeaderFooterDataContext(parentDataContext, group.Items);

                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(FooterSize.Height, GridUnitType.Pixel);
                    grid.RowDefinitions.Add(row);
                    grid.Children.Add(groupFooterControl);
                    Grid.SetRow(groupFooterControl, grid.RowDefinitions.Count - 1);
                }
                else
                {
                    list.Remove(list[list.Count - 1]);
                    itemIndex--;
                    (listview.ItemsSource as CollectionView).Refresh();
                    groupPage++;
                }
            }
            else
            {
                groupPage++;
            }

            grid.Children.Add(listview);
            Grid.SetRow(listview, listViewRowIndex);

            return grid;
        }
    }
}
