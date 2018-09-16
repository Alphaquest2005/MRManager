using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CashSummaryManager.Annotations;

namespace CashSummaryManager.ViewModels
{
    public class CashBreakDown : INotifyPropertyChanged
    {
        private static readonly CashBreakDown instance;
        private ObservableCollection<DrawSessionDetail> _drawerSessionDetails;
        private DrawSessionDetail _drawSessionDetail;
        private ObservableCollection<DrawerCashDetail> _drawCashDetails;

        public static CashBreakDown Instance
        {
            get { return instance; }
        }
        static CashBreakDown()
        {
            instance = new CashBreakDown();
          
        }

        public CashBreakDown()
        {
            
        }

        public void GetDrawerSessionDetails()
        {
            
                using (var ctx = new CashSummaryDBDataContext())

                {
                    Instance.DrawerSessionDetails = new ObservableCollection<DrawSessionDetail>(ctx.DrawSessionDetails.Where(x =>
                        x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId));
                }
            
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


                if (res.Any())
                {
                    DrawCashDetails = new ObservableCollection<DrawerCashDetail>(res);
                    return;
                }
                else
                {
                    var cc = ctx.CashTypeComponents.Where(x => x.CashType.Name == DrawSessionDetail.PayCode);
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

            using (var ctx = new CashSummaryDBDataContext())
            {
                DrawCashDetails = new ObservableCollection<DrawerCashDetail>(
                    ctx.DrawerCashDetails.Where(x =>
                        x.CashTypeComponent.CashType.Name == DrawSessionDetail.PayCode &&
                        x.DrawSessionId == DrawSessionDetail.DrawSessionId.ToString()).ToList());
            }
        }

        public ObservableCollection<DrawerCashDetail> DrawCashDetails
        {
            get => _drawCashDetails;
            set
            {
                if (Equals(value, _drawCashDetails)) return;
                _drawCashDetails = value;
                OnPropertyChanged();
            }
        }

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

        

    }
}