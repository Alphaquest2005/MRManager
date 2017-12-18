using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections;

namespace PrintableListView
{
    public class UIUtility
    {
        public static T CreateDeepCopyFromResource<T>(string key) where T : UIElement
        {
            T originalElement = Application.Current.FindResource(key) as T;

            return CreateDeepCopy<T>(originalElement);
        }

        public static T CreateDeepCopy<T>(T originalElement) where T : DependencyObject
        {
            // Create a deep copy of the original Path, we have to do so, because we cannot use the original path
            // more than once.
            string str = XamlWriter.Save(originalElement);
            System.IO.StringReader stringReader = new System.IO.StringReader(str);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            T copyElement = XamlReader.Load(xmlReader) as T;

            return copyElement;
        }

        public static ListView createListView(IList sourceView, ViewBase view, int itemsApproximation, ref int itemIndex, Rect size, out BindingList<object> list)
        {
            ListView listview = new ListView();

            // Setting view
            if (view != null)
            {
                listview.View = UIUtility.CreateDeepCopy<ViewBase>(view);
                listview.UpdateLayout();
            }

            // Create Itemssource
            list = new BindingList<object>();
            BindingListCollectionView collectionViewSource = new BindingListCollectionView(list);

            listview.ItemsSource = collectionViewSource;
            //listview.GroupStyle.Add(printableListView.PrintGroupStyle);

            StackPanel body = new StackPanel();
            body.Children.Add(listview);

            // Add the estimated items to the listview
            int currentPosition = itemIndex;
            for (int i = 0; i < itemsApproximation && i + currentPosition < sourceView.Count; i++)
            {
                object item = sourceView[itemIndex];
                list.Add(item);
                itemIndex++;
            }
            collectionViewSource.Refresh();

            // Calculate the size of listview
            listview.Measure(new Size());
            body.Arrange(new Rect());

            // Recorrect the items inside listview
            while (listview.ActualHeight < size.Height && itemIndex < sourceView.Count)
            {
                object item = sourceView[itemIndex];
                list.Add(item);
                itemIndex++;
                collectionViewSource.Refresh();
                listview.Measure(new Size());
                body.Arrange(size);
            }

            while (listview.ActualHeight > size.Height && itemIndex >= 0)
            {
                list.Remove(list[list.Count - 1]);
                itemIndex--;
                collectionViewSource.Refresh();
                listview.Measure(new Size());
                body.Arrange(size);
            }
            body.Children.Clear();

            return listview;

        }
    }
}
