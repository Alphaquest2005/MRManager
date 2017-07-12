using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Input;

namespace WaterNut.Converters
{
    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Excel.Application _ExcelApp = null;
        private Workbooks _books = null;
        private _Workbook _book = null;
        private Sheets _sheets = null;
        private _Worksheet _sheet = null;
        private Range _range = null;
        private Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
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
                ReleaseObject(_ExcelApp);
            }
        }
        public void SaveReport(string FileName)
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
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
                //ReleaseObject(_ExcelApp);
            }
        }
        public void StartUp()
        {
            _ExcelApp = new Excel.Application();
            _books = (Workbooks)_ExcelApp.Workbooks;
            _book = (_Workbook)(_books.Add(_optionalValue));
            _sheets = (Sheets)_book.Worksheets;
           
        }
        public void ShutDown()
        {
            ReleaseObject(_sheet);
            ReleaseObject(_sheets);
            ReleaseObject(_book);
            ReleaseObject(_books);
            ReleaseObject(_ExcelApp);
        }

        private void SaveFile(string FileName)
        {
            _ExcelApp.Visible = false;
            
            _book.SaveAs(FileName, XlFileFormat.xlWorkbookNormal,
                Missing.Value, Missing.Value, false, false,
                XlSaveAsAccessMode.xlShared, false, false, Missing.Value,
                Missing.Value, Missing.Value); 
        }

        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _ExcelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            var header = CreateHeader();
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
                var objData = new object[dataToPrint.Count, header.Length];

                for (var j = 0; j < dataToPrint.Count; j++)
                {
                    var item = dataToPrint[j];
                    if (item == null) continue;
                    for (var i = 0; i < header.Length; i++)
                    {
                        var y = typeof(T).InvokeMember(header[i].ToString(), BindingFlags.GetProperty, null, item, null);
                        objData[j, i] = (y == null) ? "" : y.ToString();
                    }
                }
                AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
                AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
            }
            catch (Exception Ex)
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
            var headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            var objHeaders = new List<object>();
            for (var n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
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
        /// Method to add an Excel rows
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
            _ExcelApp = new Excel.Application();
            _books = (Workbooks)_ExcelApp.Workbooks;
            _book = (_Workbook)(_books.Add(_optionalValue));
            _sheets = (Sheets)_book.Worksheets;
            _sheet = (_Worksheet)(_sheets.get_Item(1));
        }

        private void CreateNewBook()
        {
            //_ExcelApp = new Excel.Application();
            //_books = (Excel.Workbooks)_ExcelApp.Workbooks;
            _book = (_Workbook)(_books.Add(_optionalValue));
            _sheets = (Sheets)_book.Worksheets;
            _sheet = (_Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception Ex)
            {
                obj = null;
                MessageBox.Show(Ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
