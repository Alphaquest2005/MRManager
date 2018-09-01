using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CheckManager;
using CheckManager.Annotations;
using Core.Common.UI;
using MoreLinq;


namespace MNIB_Distribution_Manager
{
    public class CheckViewModel : INotifyPropertyChanged
    {
        private static readonly CheckViewModel instance;
        private Cheque _currentCheque;
        private string _chequeStatus;
        private ObservableCollection<Cheque> _cheques;
        private ObservableCollection<Payee> _payees;
        private DateTime _chequeDate;
        private string _distributionAccount;
        private ObservableCollection<Account> _accounts = new ObservableCollection<Account>();
        private string _cashAccount;

        public ObservableCollection<Cheque> Cheques
        {
            get => _cheques;
            private set
            {
                if (Equals(value, _cheques)) return;
                _cheques = value;
                OnPropertyChanged();
            }
        }

        static CheckViewModel()
        {
            instance = new CheckViewModel();
            

        }

        public static CheckViewModel Instance
        {
            get { return instance; }
        }

        public string ChequeStatus
        {
            get => _chequeStatus;
            set
            {
                _chequeStatus = value;
                GetCheques();
            }
        }

        public void GetCheques()
        {
            var oldCheque = CurrentCheque;
            using (var ctx = new ChequeDBDataContext())
            {
                var sourceCheques = SearchTxt == ""
                    ? ctx.Cheques.OrderByDescending(x => x.ChequeDate)
                    : ctx.Cheques.OrderByDescending(x => x.ChequeDate).Where(x => x.ChequeNumber.ToString().Contains(SearchTxt)
                                                                                  || x.InvoiceNumber.Contains(SearchTxt)
                                                                                  || x.Vendor.Contains(SearchTxt));
                if (ChequeDate != DateTime.Parse("1/1/1753 12:00:00 AM"))
                {
                    sourceCheques = sourceCheques.Where(x => x.ChequeDate.Value.Date == ChequeDate.Date);
                }

                var res = (from cheque in string.IsNullOrEmpty(CashAccount) ? sourceCheques : sourceCheques.Where(x => x.CashAccount == CashAccount)
                    join v in ctx.Vouchers on cheque.VoucherNumber.ToString() equals v.VoucherNumber into comps
                    from voucher in comps.DefaultIfEmpty()
                    join d in string.IsNullOrEmpty(DistributionAccount) ? ctx.Distributions: ctx.Distributions.Where(x => x.AccountNumber == DistributionAccount) on cheque.VoucherNumber equals d.VoucherNumber into prods
                    from distribution in prods
                         select new  Cheque()
                    {
                        Amount = cheque.Amount,
                        ChequeDate = cheque.ChequeDate,
                        ChequeNumber = cheque.ChequeNumber,
                        InvoiceNumber = cheque.InvoiceNumber,
                        InvoiceDate = cheque.InvoiceDate,
                        PONumber = cheque.PONumber,
                        VendorNumber = cheque.VendorNumber,
                        VendorName = cheque.VendorName,
                        TransactionReference = cheque.TransactionReference,
                        Vendor = cheque.Vendor,
                        Voucher = voucher ?? new Voucher(),
                        Distribution = prods.ToList(),
                        VoucherNumber = cheque.VoucherNumber,
                        CashAccount = cheque.CashAccount
                    }).DistinctBy(z => z.VoucherNumber).Take(25).ToList();

                switch (ChequeStatus)
                {
                    case "UnPrepared":
                        res = res.Where(x => x.Voucher == null || x.Voucher.Prepared == null).ToList();
                        break;
                    case "UnAuthorized":
                        res = res.Where(x => x.Voucher != null && x.Voucher.Prepared != null && x.Voucher.Prepared.Signatures != x.Voucher.Authorizeds.Count).ToList();
                        break;
                    //case "Authorized":
                    //    res = res.Where(x => x.Voucher != null && x.Voucher.Prepared != null && x.Voucher.Prepared.Signatures == x.Voucher.Authorizeds.Count && x.Voucher.Disbursed == null).ToList();
                    //    break;
                    case "UnDisbursed":
                        res = res.Where(x => x.Voucher != null && x.Voucher.Prepared != null && x.Voucher.Prepared.Signatures == x.Voucher.Authorizeds.Count && (x.Voucher.Disbursed == null || x.Voucher.Disbursed != null && x.Voucher.Disbursed.PayeeId == 0)).ToList();
                        break;
                    case "Disbursed":
                        res = res.Where(x => x.Voucher != null && x.Voucher.Prepared != null && x.Voucher.Prepared.Signatures == x.Voucher.Authorizeds.Count && x.Voucher.Disbursed != null && x.Voucher.Disbursed.PayeeId != 0).ToList();
                        break;
                }

                Cheques = new ObservableCollection<Cheque>(res);
                if(oldCheque != null)
                CurrentCheque = Cheques.FirstOrDefault(x => x.VoucherNumber == oldCheque.VoucherNumber);
            }
        }


        public Cheque CurrentCheque
        {
            get => _currentCheque;
            set
            {
                _currentCheque = value;
                LoadPayees();
                OnPropertyChanged();
            } 
        }

        private void LoadPayees()
        {
            if (CurrentCheque == null) return;
            using (var ctx = new ChequeDBDataContext())
            {
                Payees = new ObservableCollection<Payee>(ctx.Payees.Where(x => x.VendorNumber == CurrentCheque.VendorNumber));
            }
        }

        public User User { get; set; }


        public CheckViewModel()
        {


            Application.Current.Dispatcher.Invoke(() =>
            {
                using (var ctx = new ChequeDBDataContext())
                {
                    Accounts = new ObservableCollection<Account>(ctx.Accounts.ToList());
                }
            });

            Cheques = new ObservableCollection<Cheque>();
            ChequeDate = DateTime.Now.Date;
            GetCheques();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Search(string searchTxt)
        {
            SearchTxt = searchTxt;
            GetCheques();
            
        }

        public string SearchTxt { get; set; } = "";

        public ObservableCollection<Payee> Payees
        {
            get => _payees;
            set
            {
                if (Equals(value, _payees)) return;
                _payees = value;
                OnPropertyChanged();
            }
        }

        public DateTime ChequeDate
        {
            get => _chequeDate;
            set
            {
                if (Equals(value, _chequeDate)) return;
                _chequeDate = value;
                GetCheques();
                OnPropertyChanged();
            }
        }

        public string DistributionAccount
        {
            get => _distributionAccount;
            set
            {
                if (Equals(value, _distributionAccount)) return;
                _distributionAccount = value;
                GetCheques();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set
            {
                if (Equals(value, _accounts)) return;
                _accounts = value;
                OnPropertyChanged();
            }
        }

        public string CashAccount
        {
            get => _cashAccount;
            set
            {
                if (Equals(value, _cashAccount)) return;
                _cashAccount = value;
                GetCheques();
                OnPropertyChanged();
            }
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void ProcessCheque()
        {
            if (CheckViewModel.Instance.User.UserPermissions.All(x =>
                x.Permission.Name != null && x.Permission.Name != CurrentCheque.Voucher.Action.ToString())) return;

            switch (CurrentCheque.Voucher.Action)
            {
                case "Prepare":
                    using (var ctx = new ChequeDBDataContext())
                    {
                        var voucher = new Voucher()
                        {
                            ChequeNumber = CurrentCheque.ChequeNumber.GetValueOrDefault().ToString(),
                            VoucherNumber = CurrentCheque.VoucherNumber.ToString(),
                            Prepared = new Prepared()
                            {
                                DatePrepared = DateTime.Now,
                                PreparedBy = User.Id,
                                Signatures = 1,
                                
                            }
                        };
                        ctx.Vouchers.InsertOnSubmit(voucher);
                        ctx.SubmitChanges();
                        CurrentCheque.Voucher = voucher;
                        CurrentCheque.Voucher.Prepared.User = User;
                    }
                   break;
                case "Authorize":
                    using (var ctx = new ChequeDBDataContext())
                    {
                        var authorize = CurrentCheque.Voucher.Authorizeds.FirstOrDefault(x => x.AuthorizedBy == User.Id);
                        if (authorize == null)
                        {
                            authorize = new Authorized()
                            {
                                AuthorizedBy = User.Id,
                                DateAuthorized = DateTime.Now,
                                VoucherId = CurrentCheque.Voucher.Id,
                            };
                           
                            ctx.Authorizeds.InsertOnSubmit(authorize);
                            ctx.SubmitChanges();
                            authorize.User = User;
                            authorize.Voucher = CurrentCheque.Voucher;
                            CurrentCheque.Voucher.Authorizeds.Add(authorize);
                        }


                    }
                    break;
                case "Disburse":
                    using (var ctx = new ChequeDBDataContext())
                    {
                        var disbursed = CurrentCheque.Voucher.Disbursed;
                        if (disbursed == null)
                        {
                            disbursed = new Disbursed()
                            {
                               PayeeId = 0,
                                Date = DateTime.Now,
                                DisbursedBy = User.Id,
                               Id = CurrentCheque.Voucher.Id
                            };

                            var vendorPayee = ctx.Payees.FirstOrDefault(x =>
                                x.VendorNumber == CurrentCheque.VendorNumber && x.Name == CurrentCheque.VendorName);
                            if (vendorPayee == null)
                            {
                                vendorPayee = new Payee()
                                {
                                    IdNumber = "N/A",
                                    Name = CurrentCheque.VendorName,
                                    VendorNumber = CurrentCheque.VendorNumber
                                };
                                ctx.Payees.InsertOnSubmit(vendorPayee);
                            }

                            ctx.Disburseds.InsertOnSubmit(disbursed);
                            ctx.SubmitChanges();
                            disbursed.User = User;
                            disbursed.Voucher = CurrentCheque.Voucher;
                            CurrentCheque.Voucher.Disbursed = disbursed;
                        }

                        Payees = new ObservableCollection<Payee>(ctx.Payees.Where(x => x.VendorNumber == CurrentCheque.VendorNumber));

                    }
                    break;
            }
            GetCheques();


        }

        public void SavePayee()
        {
            using (var ctx = new ChequeDBDataContext())
            {
                var payee = CurrentCheque.Voucher.Disbursed.Payee;
                var dbPayee = ctx.Payees.FirstOrDefault(x => x.Name == payee.Name && (x.IdNumber == payee.IdNumber || x.IdNumber == "N/A") && x.VendorNumber == CurrentCheque.VendorNumber);
                if (dbPayee == null)
                {
                    dbPayee = new Payee()
                    {
                        IdNumber =  payee.IdNumber,
                        Name = payee.Name,
                        VendorNumber = CurrentCheque.VendorNumber
                    };
                    var disbursed = ctx.Disburseds.First(x => x.Id == CurrentCheque.Voucher.Disbursed.Id);
                    disbursed.Payee = dbPayee;
                    disbursed.DisbursedBy = User.Id;
                    disbursed.Date = DateTime.Now;
                    ctx.SubmitChanges();
                    Payees.Add(dbPayee);
                }
                else
                {
                    var disbursed = ctx.Disburseds.First(x => x.Id == CurrentCheque.Voucher.Disbursed.Id);
                    disbursed.DisbursedBy = User.Id;
                    disbursed.Date = DateTime.Now;
                    if (dbPayee.IdNumber == "N/A" && payee.IdNumber != "N/A") dbPayee.IdNumber = payee.IdNumber;
                    disbursed.Payee = dbPayee;
                    ctx.SubmitChanges();
                }
                
                
            }
            GetCheques();
        }

        public void UpdateSignatures()
        {
            using (var ctx = new ChequeDBDataContext())
            {
                var res = ctx.Prepareds.First(x => x.Id == CurrentCheque.Voucher.Prepared.Id);
                res.Signatures = CurrentCheque.Voucher.Prepared.Signatures;
                ctx.SubmitChanges();
            }
        }

        public void SaveNotes()
        {
            using (var ctx = new ChequeDBDataContext())
            {
                if (CurrentCheque.Voucher.Prepared != null)
                {
                    var res = ctx.Prepareds.First(x => x.Id == CurrentCheque.Voucher.Prepared.Id);
                    if (res.Notes != CurrentCheque.Voucher.Prepared.Notes)
                        res.Notes = CurrentCheque.Voucher.Prepared.Notes;
                   
                }

                foreach (var authorized in CurrentCheque.Voucher.Authorizeds)
                {
                    var res = ctx.Authorizeds.First(x => x.Id == authorized.Id);
                    if (res.Notes != authorized.Notes)
                        res.Notes = authorized.Notes;
                }

                if (CurrentCheque.Voucher.Disbursed != null)
                {
                    var res = ctx.Disburseds.First(x => x.Id == CurrentCheque.Voucher.Disbursed.Id);
                    if (res.Notes != CurrentCheque.Voucher.Disbursed.Notes)
                        res.Notes = CurrentCheque.Voucher.Disbursed.Notes;

                }
                ctx.SubmitChanges();
            }
        }
    }
}
