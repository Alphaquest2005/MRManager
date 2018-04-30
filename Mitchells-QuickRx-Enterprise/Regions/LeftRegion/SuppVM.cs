using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using RMSDataAccessLayer;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism;
using SalesRegion;
using SimpleMvvmToolkit;


namespace LeftRegion
{
    public class SuppVM : ViewModelBase<SuppVM>
    {
         private static readonly SuppVM _instance;
         static SuppVM()
        {
            _instance = new SuppVM();
            SalesVM.Instance.PropertyChanged += SalesVm_PropertyChanged;
        }

        private static void SalesVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TransactionData")
            {
                if (SalesVM.Instance.TransactionData is Prescription p)
                {
                    if (p.ParentTransactionId > 0 )
                    {
                        //if(Instance.SearchResults.All(x => x.TransactionId != p.ParentTransactionId)) SuppVM.Instance.SearchResults = new ObservableCollection<Prescription>(){p.ParentTransaction};
                        //if(SuppVM.Instance?.TransactionData?.TransactionId != p?.ParentTransaction?.TransactionId) SuppVM.Instance.TransactionData = p.ParentTransaction;
                    }
                }
            }
        }

        public static SuppVM Instance
        {
            get { return _instance; }
        }

        string _searchText;
        
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                try
                {
                    _searchText = value;
                    NotifyPropertyChanged(x => x.SearchText);
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        
        public void AutoRepeat(TransactionBase transactionData)
        {
            SalesVM.Instance.AutoRepeat(transactionData);
        }

        
        public void CopyPrescription()
        {
            var t = SalesVM.Instance.CopyCurrentTransaction(false);
            SalesVM.Instance.TransactionData = t;
            //if (t != null) SalesVM.Instance.GoToTransaction(t.TransactionId);
        }

        public void SearchTransactions()
        {
            SearchTransactions(SearchText);
        }

        public void SearchTransactions(string searchTxt)
        {
            try
            {

                SearchResults = new ObservableCollection<Prescription>(GetTransactions(searchTxt));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Prescription> GetTransactions(string searchTxt)
        {
            using (var ctx = new RMSModel())
            {
                IQueryable<Prescription> Transactions;
                if (Int32.TryParse(searchTxt, out int num))
                {
                    Transactions = ctx.TransactionBase.OfType<Prescription>().AsNoTracking()
                        .Where(x => x.TransactionId.ToString().Contains(searchTxt));
                }
                else
                {
                    Transactions = ctx.TransactionBase.OfType<Prescription>().AsNoTracking()
                        .Where(x => (x.Patient.FirstName + " " + x.Patient.LastName).Contains(searchTxt) );
                }

                

                var list = Transactions.OfType<Prescription>()
                    .OrderByDescending(x => x.TransactionId)
                    .Where(x => x.ParentTransactionId == null)
                    .OrderByDescending(x => x.Time)
                    .Select(x => new
                    {
                        TransactionId = x.TransactionId,
                        Time = x.Time,
                       
                        Patient = new
                        {
                            FirstName = x.Patient.FirstName,
                            LastName = x.Patient.LastName,
                        },
                        Doctor = new
                        {
                            FirstName = x.Doctor.FirstName,
                            LastName = x.Doctor.LastName,
                        },
                        TransactionEntries = x.TransactionEntries
                            .Select(z => new
                            {
                                Quantity = z.Quantity,
                                Price = z.Price,
                                SalesTaxPercent = z.SalesTaxPercent,
                                Discount = z.Discount,
                                TransactionEntryItem = new
                                {
                                    ItemName = z.TransactionEntryItem.ItemName
                                }
                            }).ToList(),
                        Transactions = x.Transactions.OfType<Prescription>().Select(pp => new
                        {
                            TransactionId = pp.TransactionId,
                            Time = pp.Time,
                            Patient = new
                            {
                                FirstName = pp.Patient.FirstName,
                                LastName = pp.Patient.LastName,
                            },
                            Doctor = new
                            {
                                FirstName = pp.Doctor.FirstName,
                                LastName = pp.Doctor.LastName,
                            },
                            TransactionEntries = pp.TransactionEntries
                                .Select(zz => new
                                {
                                    Quantity = zz.Quantity,
                                    Price = zz.Price,
                                    SalesTaxPercent = zz.SalesTaxPercent,
                                    Discount = zz.Discount,
                                    TransactionEntryItem = new
                                    {
                                        ItemName = zz.TransactionEntryItem.ItemName
                                    }
                                }),
                        })

                    })
                    .Take(listCount)
                    .ToList();
                var prescriptions = list
                    .Select(x => new Prescription()
                    {
                        TransactionId = x.TransactionId,
                        Time = x.Time,
                        Patient = new Patient()
                        {
                            FirstName = x.Patient.FirstName,
                            LastName = x.Patient.LastName,
                        },
                        Doctor = new Doctor()
                        {
                            FirstName = x.Doctor.FirstName,
                            LastName = x.Doctor.LastName,
                        },
                        TransactionEntries = new ObservableCollection<TransactionEntryBase>(
                            x.TransactionEntries
                                .Select(z => new PrescriptionEntry()
                                {
                                    Quantity = z.Quantity,
                                    Price = z.Price,
                                    SalesTaxPercent = z.SalesTaxPercent,
                                    Discount = z.Discount,
                                    TransactionEntryItem = new TransactionEntryItem()
                                    {
                                        ItemName = z.TransactionEntryItem.ItemName
                                    }
                                })),
                        Transactions = new ObservableCollection<TransactionBase>(x.Transactions.Select(pp =>
                            new Prescription()
                            {
                                TransactionId = pp.TransactionId,
                                Time = pp.Time,
                                
                                Patient = new Patient()
                                {
                                    FirstName = pp.Patient.FirstName,
                                    LastName = pp.Patient.LastName,
                                },
                                Doctor = new Doctor()
                                {
                                    FirstName = pp.Doctor.FirstName,
                                    LastName = pp.Doctor.LastName,
                                },
                                TransactionEntries = new ObservableCollection<TransactionEntryBase>(
                                    pp.TransactionEntries
                                        .Select(q => new PrescriptionEntry()
                                        {
                                            Quantity = q.Quantity,
                                            Price = q.Price,
                                            SalesTaxPercent = q.SalesTaxPercent,
                                            Discount = q.Discount,
                                            TransactionEntryItem = new TransactionEntryItem()
                                            {
                                                ItemName = q.TransactionEntryItem.ItemName
                                            }
                                        })),
                            
                            }).ToList())

                    }).ToList();
                return prescriptions;
            }
        }

        static ObservableCollection<Prescription> _searchResults = new ObservableCollection<Prescription>();
        
        public ObservableCollection<Prescription> SearchResults
        {
            get
            {
                return _searchResults;
            }
            set
            {
                _searchResults = value;
                NotifyPropertyChanged(x => x.SearchResults);
            }
        }

        //+ ToDo: Replace this with your own data fields
        private RMSDataAccessLayer.TransactionBase transactionData;
        private int listCount = 10;
        
        public RMSDataAccessLayer.TransactionBase TransactionData
        {
            get { return transactionData; }
            set
            {
                try
                {
                    if (!object.Equals(transactionData, value))
                    {
                        transactionData = value;
                        if (value != null)
                        {
                            if (SalesVM.Instance.TransactionData is Prescription trans)
                            {
                                //if (trans.TransactionId != transactionData.TransactionId && trans.Transactions.All(x => x.TransactionId != transactionData.TransactionId))
                                    SalesVM.Instance.GoToTransaction(transactionData.TransactionId);
                            }
                            else
                            {
                                SalesVM.Instance.GoToTransaction(transactionData.TransactionId);
                            }
                            
                        }

                        //if(this.regionManager.Regions["HeaderRegion"] != null) this.regionManager.Regions["HeaderRegion"].Context = transactionData;
                       //if(this.regionManager.Regions["CenterRegion"] != null) this.regionManager.Regions["CenterRegion"].Context = transactionData;
                        NotifyPropertyChanged(x => x.TransactionData);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
