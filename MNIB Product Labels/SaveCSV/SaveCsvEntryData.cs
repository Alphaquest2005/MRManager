using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Core.Common.CSV;



namespace SaveCSV
{
    public class SaveCsvEntryData
    {
        private static readonly SaveCsvEntryData instance;

        static SaveCsvEntryData()
        {
            instance = new SaveCsvEntryData();
        }

        public static SaveCsvEntryData Instance
        {
            get { return instance; }
        }

        public async Task<bool> ExtractEntryData(string fileType, string[] lines, string[] headings,
            bool overWriteExisting)
        {

            var mapping = new Dictionary<string, int>()
            {
                {"TransactionNo",0},
                {"LotNumber",1},
                {"Quantity",2},
                {"TransactionDate",3},
                {"TransactionTime",4}
            };
            
            var eslst = GetCSVDataSummayList(lines, mapping);

            if (eslst == null) return true;


            if (await ImportEntryData(fileType, eslst, overWriteExisting).ConfigureAwait(false)) return true;
            return false;
        }



        private async Task<bool> ImportEntryData(string fileType, List<CSVDataSummary> eslst, bool overWriteExisting)
        {
            var lst =
                eslst.Select(x => new TransactionDetail()
                {
                    TransactionNo = x.TransactionNo,
                    LotNumber = x.LotNumber,
                    Quantity = x.Quantity,
                    TransactionDateTime = x.TransactionDateTime,
                        

                });

            using (var ctx = new MNIBLabelsDBDataContext())
            {
                try
                {
                        ctx.TransactionDetails.InsertAllOnSubmit(lst);
                        ctx.SubmitChanges(ConflictMode.ContinueOnConflict); //
                    
                }
                catch (ChangeConflictException e)
                {
                    Console.WriteLine(e.Message);
                    foreach (ObjectChangeConflict occ in ctx.ChangeConflicts)
                    {
                        if (overWriteExisting)
                        {
                            occ.Resolve(RefreshMode.KeepCurrentValues);
                        }
                        else
                        {
                            occ.Resolve(RefreshMode.OverwriteCurrentValues);
                        }
                    }
                }
                catch (SqlException sqe)
                {
                    if (sqe.Number == 2627)
                    {
                      throw new ApplicationException("Duplicate Data in File! Delete then try again");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }

    return false;
    }


        private List<CSVDataSummary> GetCSVDataSummayList(string[] lines, Dictionary<string, int> mapping)
        {
            var eslst = new List<CSVDataSummary>();
            for (var i = 1; i < lines.Count(); i++)
            {
                var d = GetCSVDataFromLine(lines[i], mapping);
                if (d != null)
                {
                    eslst.Add(d);
                }
            }
            return eslst;
        }



        private CSVDataSummary GetCSVDataFromLine(string line, Dictionary<string, int> mapping)
        {
            try
            {
                var splits = line.CsvSplit();
                if (splits[mapping["TransactionNo"]] != "" && splits[mapping["LotNumber"]] != "")
                {
                    var cs = new CSVDataSummary();
                    cs.TransactionNo = splits[mapping["TransactionNo"]];
                    cs.LotNumber = splits[mapping["LotNumber"]];
                    cs.Quantity = Convert.ToInt16(splits[mapping["Quantity"]]);

                    DateTime d = DateTime.MinValue;
                   
                        DateTime.TryParseExact(splits[mapping["TransactionDate"]] + splits[mapping["TransactionTime"]],
                            "ddMMyyHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                    if (d != DateTime.MinValue)
                    {
                        cs.TransactionDateTime = d;
                    }
                    else
                    {
                        throw new ApplicationException(string.Format("{0} - {1} can not be converted to Date",
                            splits[mapping["TransactionDate"]],
                            splits[mapping["TransactionTime"]]));
                    }

                    return cs;


                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return null;
        }

       
      

    }

    internal class CSVDataSummary
    {
        public string TransactionNo { get; set; }
        public string LotNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDateTime { get; set; }
       
    }
}