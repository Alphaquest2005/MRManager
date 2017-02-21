using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Core.Common.CSV;


namespace SaveCSV
{
    public partial class SaveCSVModel
    {
        private static readonly SaveCSVModel instance;
        static SaveCSVModel()
        {
            instance = new SaveCSVModel();
        }

        public static SaveCSVModel Instance
        {
            get { return instance; }
        }

        public  async Task ProcessDroppedFile(string droppedFilePath, string fileType, bool overWriteExisting)
        {
            try
            {
                await SaveCSV(droppedFilePath, fileType,  overWriteExisting).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                throw new ApplicationException(string.Format("Problem importing File '{0}'. - Error: {1}", droppedFilePath, Ex.Message));
            }

        }

        private  async Task SaveCSV(string droppedFilePath, string fileType,  bool overWriteExisting)
        {
            try
            {
                var lines = File.ReadAllLines(droppedFilePath);
                // identify header
                var headings = "TransactionNo,LotNo,Quantity,Date,Time".CsvSplit();

              
                    if (await SaveCsvEntryData.Instance.ExtractEntryData(fileType, lines, headings, overWriteExisting).ConfigureAwait(false)) return;
                
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
     


    }
}
