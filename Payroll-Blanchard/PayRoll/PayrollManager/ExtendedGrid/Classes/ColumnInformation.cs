using System.ComponentModel;
using System.Windows.Data;
using ExtendedGrid.Microsoft.Windows.Controls;

namespace ExtendedGrid.Classes
{
    public struct  ColumnInformation
    {
        public ColumnInformation(DataGridColumn column)
        {
            Header = column.Header;
            if (!(column is DataGridTemplateColumn))
            {
                if (column is DataGridComboBoxColumn)
                {
                    PropertyPath = column.SortMemberPath;
                }
                else
                {
                    PropertyPath = ((Binding)((DataGridBoundColumn)column).Binding).Path.Path;
                }
            }
            else
            {
                PropertyPath = column.SortMemberPath;
            }
            WidthValue = column.Width.DisplayValue;
            WidthType = column.Width.UnitType;
            SortDirection = column.SortDirection;
            DisplayIndex = column.DisplayIndex;
            SortMemberPath = column.SortMemberPath;
        }
        public void Apply(DataGridColumn column, int gridColumnCount, SortDescriptionCollection sortDescriptions)
        {
            column.Width = new DataGridLength(WidthValue, WidthType);
            column.SortDirection = SortDirection;
            if (SortDirection != null)
            {
                sortDescriptions.Add(new SortDescription(PropertyPath, SortDirection.Value));
            }
            if (column.DisplayIndex != DisplayIndex)
            {
                var maxIndex = (gridColumnCount == 0) ? 0 : gridColumnCount - 1;
                column.DisplayIndex = (DisplayIndex <= maxIndex) ? DisplayIndex : maxIndex;
            }
            column.SortMemberPath = SortMemberPath;
        }
        public object Header;
        public string PropertyPath;
        public ListSortDirection? SortDirection;
        public int DisplayIndex;
        public double WidthValue;
        public DataGridLengthUnitType WidthType;
        public string SortMemberPath;
    }
}
