using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CashSummaryManager.Annotations;

namespace CashSummaryManager.ViewModels
{
    public class CashBreakDown : INotifyPropertyChanged
    {
        private static readonly CashBreakDown instance;
        private ObservableCollection<DrawSessionDetail> _drawerSessionDetails;
        private DrawSessionDetail _drawSessionDetail;
        private ObservableCollection<DrawerCashDetail> _drawCashDetails = new ObservableCollection<DrawerCashDetail>();
        private DrawerCashDetail _drawCashDetail;
        private ObservableCollection<CashTypeComponent> _cashComponents;

        public static CashBreakDown Instance
        {
            get { return instance; }
        }
        static CashBreakDown()
        {
            instance = new CashBreakDown();
          
        }

       
        public void GetDrawerSessionDetails()
        {
            if (DrawerSelector.Instance.DrawerSession == null) return;
            DrawSessionDetail oldDrawSessionDetail = DrawSessionDetail;
            
                using (var ctx = new CashSummaryDBDataContext())

                {
                    Instance.DrawerSessionDetails = new ObservableCollection<DrawSessionDetail>(ctx.DrawSessionDetails.Where(x =>
                        x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId));
                }

            if (oldDrawSessionDetail != null)
                _drawSessionDetail = Instance.DrawerSessionDetails.FirstOrDefault(x =>
                    x.DrawSessionId == oldDrawSessionDetail.DrawSessionId && x.PayCode == oldDrawSessionDetail.PayCode);

        }



        public ObservableCollection<DrawSessionDetail> DrawerSessionDetails
        {
            get => _drawerSessionDetails;
            set
            {
                if (Equals(value, _drawerSessionDetails)) return;
                _drawerSessionDetails = value;
                OnPropertyChanged();
                
            }
        }

        private void SetCashTypeDetails()
        {
            if (DrawSessionDetail == null) return;
            using (var ctx = new CashSummaryDBDataContext())
            {
                var res = ctx.DrawerCashDetails.Where(x =>
                    x.CashTypeComponent.CashType.Name == DrawSessionDetail.PayCode &&
                    x.DrawSessionId == DrawSessionDetail.DrawSessionId.ToString()).ToList();

                CashComponents = new ObservableCollection<CashTypeComponent>(ctx.CashTypeComponents.Where(x => x.CashType.Name == DrawSessionDetail.PayCode).ToList());
                if (res.Any() || DrawerSelector.Instance.IsPosted)
                {
                    DrawCashDetails = new ObservableCollection<DrawerCashDetail>(res);
                    return;
                }
                else
                {
                    var cc = ctx.CashTypeComponents.Where(x => x.CashType.Name == DrawSessionDetail.PayCode && x.CashComponent.Name != "RECON");
                    foreach (var t in cc)
                    {
                        ctx.DrawerCashDetails.InsertOnSubmit(new DrawerCashDetail()
                        {
                            Quantity = 0,
                            CashTypeCompoentId = t.Id,
                            DrawSessionId = DrawSessionDetail.DrawSessionId.ToString()
                        });
                        ctx.SubmitChanges();
                    }
                }


            }

            RefeshDrawerCashDetails();
        }

        private void RefeshDrawerCashDetails()
        {
            if(DrawSessionDetail == null)return;
            using (var ctx = new CashSummaryDBDataContext())
            {
                DrawCashDetails = new ObservableCollection<DrawerCashDetail>(
                    ctx.DrawerCashDetails.Where(x =>
                        x.CashTypeComponent.CashType.Name == DrawSessionDetail.PayCode &&
                        x.DrawSessionId == DrawSessionDetail.DrawSessionId.ToString()).ToList());
            }

            
          RefeshDrawerTotals();
        }

        public ObservableCollection<DrawerCashDetail> DrawCashDetails
        {
            get => _drawCashDetails;
            set
            {
                if (Equals(value, _drawCashDetails)) return;
                _drawCashDetails = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DetailTotal));
            }
        }

        public double DetailTotal
        {
            get { return DrawCashDetails.Select(x => x.Total).DefaultIfEmpty(0).Sum(); }
        }

        public double SessionTotal
        {
            get { return DrawerSessionDetails.Select(x => Convert.ToDouble(x.Amount.GetValueOrDefault())).DefaultIfEmpty(0).Sum(); }
        }

        public double CashTotal
        {
            get
            {
                if (DrawerSelector.Instance.DrawerSession == null) return 0;
                using (var ctx = new CashSummaryDBDataContext())

                {
                    var res = ctx.DrawerCashDetails.Where(x =>
                        x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId.ToString());
                    return res.Any() ? res.Select(x => x.Quantity * x.CashTypeComponent.CashComponent.UnitValue).Sum() : 0;
                }
            }
        }

        public double DrawCashDifference => SessionTotal - CashTotal;
        

        public bool IsBalanced => Math.Abs(CashTotal - SessionTotal) < 0.001;

        public DrawSessionDetail DrawSessionDetail
        {
            get => _drawSessionDetail;
            set
            {
                if (Equals(value, _drawSessionDetail)) return;
                _drawSessionDetail = value;
                SetCashTypeDetails();
                OnPropertyChanged();
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void AddRow()
        {
            if (DrawSessionDetail == null)
            {
                MessageBox.Show("Please Select Pay Code");
                return;
            }
            using (var ctx = new CashSummaryDBDataContext())
            {
                var comp = ctx.CashTypeComponents.FirstOrDefault(x => x.CashType.Name == DrawSessionDetail.PayCode);
                if (comp != null)
                {
                    ctx.DrawerCashDetails.InsertOnSubmit(new DrawerCashDetail(){CashTypeCompoentId = comp.Id, DrawSessionId = DrawSessionDetail.DrawSessionId.ToString(), Quantity = 0});
                    ctx.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("No Cash Components Setup for this Pay Code");
                }
                RefeshDrawerCashDetails();

            }
        }

        public void DeleteRow()
        {
            if (DrawCashDetail == null)
            {
                MessageBox.Show("Please Select Drawer Detail");
                return;
            }
            using (var ctx = new CashSummaryDBDataContext())
            {
                var res = ctx.DrawerCashDetails.First(x => x.Id == DrawCashDetail.Id);
                ctx.DrawerCashDetails.DeleteOnSubmit(res);
                    ctx.SubmitChanges();
                DrawCashDetail = null;
                   RefeshDrawerCashDetails();

            }
        }

        public DrawerCashDetail DrawCashDetail
        {
            get => _drawCashDetail;
            set
            {
                if (Equals(value, _drawCashDetail)) return;
                _drawCashDetail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CashTypeComponent> CashComponents
        {
            get => _cashComponents;
            set
            {
                if (Equals(value, _cashComponents)) return;
                _cashComponents = value;
                OnPropertyChanged();
            }
        }

        public double CashDetailDiff => Math.Abs(DetailTotal - Convert.ToDouble(DrawSessionDetail?.Amount));
        


        public void SaveRow(DrawerCashDetail d)
        {
            if (DrawCashDetail == null || d == null)
            {
               // Application.Current.Dispatcher.Invoke(() => {MessageBox.Show("Please Select Drawer Detail"); });
                
                return;
            }
            using (var ctx = new CashSummaryDBDataContext())
            {

                var res = ctx.DrawerCashDetails.First(x => x.Id == d.Id);
                res.Comments = d.Comments;
                res.Quantity = d.Quantity;
                if(d.CashTypeComponent != null) res.CashTypeComponent = ctx.CashTypeComponents.First(x => x.Id == d.CashTypeComponent.Id);
                res.CashTypeCompoentId = d.CashTypeCompoentId;
                ctx.SubmitChanges();
               
            }
            RefeshDrawerTotals();
        }

        private void RefeshDrawerTotals()
        {
            GetDrawerSessionDetails();
            OnPropertyChanged(nameof(DetailTotal));
            OnPropertyChanged(nameof(SessionTotal));
            OnPropertyChanged(nameof(CashTotal));
            OnPropertyChanged(nameof(DrawCashDifference));
            OnPropertyChanged(nameof(IsBalanced));
            OnPropertyChanged(nameof(CashDetailDiff)); 
        }

        public void PostSession()
        {
            if (DrawerSelector.Instance.DrawerSession.Status == "Un-Posted" && DrawerSelector.Instance.User.UserPermissions.Any(x => x.Permission.Name == "Supervisor"))
            {
                DrawerSelector.Instance.DrawerSession.Status = "Posted";
                using (var ctx = new CashSummaryDBDataContext())
                {
                    ctx.DrawerSessionStatus.InsertOnSubmit(new DrawerSessionStatus()
                    {
                        DrawSessionId = DrawerSelector.Instance.DrawerSession.DrawSessionId.ToString(),
                        UserId = DrawerSelector.Instance.User.Id,
                        EntryDateTime = DateTime.Now,
                        Status = "Posted"
                    });

                    var res = ctx.DrawerCashDetails.Where(x =>
                        x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId.ToString() &&
                        x.Quantity == 0);

                   
                   ctx.DrawerCashDetails.DeleteAllOnSubmit(res);
                   

                    ctx.SubmitChanges();
                }

                DrawerSelector.Instance.GetDrawerSessions();
                RefeshDrawerCashDetails();
                DrawerSelector.Instance.NotifyDrawStatusChanged();
            }

            LoadDrawerSessionCashDetails();
        }

        private void LoadDrawerSessionCashDetails()
        {
            CashSummary.Instance.GetDrawerSessionCashDetails();
        }
    }
}