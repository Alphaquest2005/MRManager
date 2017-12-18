using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Input;

namespace PayrollManager.Converters
{
    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ExportToExcel
        
    {
        public DataGrid dataToPrint;
        // Excel object references.
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport(DataGrid grd)
        {
            try
            {
                dataToPrint = grd;
                if (dataToPrint != null)
                {
                    if (dataToPrint.Items.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                        MessageBox.Show("Complete");
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        public void SaveReport(string FileName)
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Items.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateNewBook();
                       
                        FillSheet();
                        SaveFile(FileName);

                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                //ReleaseObject(_books);
                //ReleaseObject(_excelApp);
            }
        }
        public void StartUp()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
           
        }
        public void ShutDown()
        {
            ReleaseObject(_sheet);
            ReleaseObject(_sheets);
            ReleaseObject(_book);
            ReleaseObject(_books);
            ReleaseObject(_excelApp);
        }

        private void SaveFile(string FileName)
        {
            _excelApp.Visible = false;
            
            _book.SaveAs(FileName, Excel.XlFileFormat.xlWorkbookNormal,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, false,
                Excel.XlSaveAsAccessMode.xlShared, false, false, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value); 
        }

        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            try
            {
                object[,] objData = new object[dataToPrint.Items.Count, header.Length];

                for (int j = 0; j < dataToPrint.Items.Count-1; j++)
                {
                    var item = dataToPrint.Items[j];
                    if (item == null) continue;
                    for (int i = 0; i < header.Length; i++)
                    {
                        var y = header[i];
                        object val;
                        var o = item as ExpandoObject;
                        if (o != null)
                        {
                            val = ((IDictionary<string, object>) item)[y.ToString().Replace(" ", "_").Replace("-", "")];
                        }
                        else
                        {
                            val = item.GetType().GetProperties().FirstOrDefault(x => x.Name.Contains(y.ToString().Replace(" ", "_").Replace("-", ""))).GetValue(item);
                        }
                        
                        objData[j, i] = (val == null) ? "" : val.ToString();
                    }
                }
                AddExcelRows("A2", dataToPrint.Items.Count, header.Length, objData);
                AutoFitColumns("A1", dataToPrint.Items.Count + 1, header.Length);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < dataToPrint.Columns.Count; n++)
            {
                objHeaders.Add(dataToPrint.Columns[n].Header);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        /// <summary>
        /// Create Excel application parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }

        private void CreateNewBook()
        {
            //_excelApp = new Excel.Application();
            //_books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            if (obj == null) return;
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
