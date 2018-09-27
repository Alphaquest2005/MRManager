using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CashSummaryManager.Annotations;

namespace CashSummaryManager.ViewModels
{
    public class CashSummary : INotifyPropertyChanged
    {
        private static readonly CashSummary instance;
        public static CashSummary Instance => instance;

        static CashSummary()
        {
            instance = new CashSummary();

        }

        public void GetDrawerSessionCashDetails()
        {
            using (var ctx = new CashSummaryDBDataContext())
            {

                //DrawSessionDetails = new ObservableCollection<DrawSessionDetail>(ctx.DrawSessionDetails.Where(x => x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId).GroupJoin(
                //    ctx.DrawerCashDetails,
                //    s => new { DrawSessionId = s.DrawSessionId.ToString(), s.PayCode },
                //    c => new { c.DrawSessionId, PayCode = c.CashTypeComponent.CashType.Name},
                //    (s, c) => new DrawSessionDetail()
                //    {
                //        DrawSessionId = s.DrawSessionId,
                //        DrawId = s.DrawId,
                //        PayCode = s.PayCode,
                //        Amount = s.Amount,
                //        ActualAmount = s.ActualAmount,
                //        Difference = s.Difference,
                //        CashDetails = c.ToList()
                //    }).ToList());
                var res = ctx.DrawSessionDetails
                    .Where(x => x.DrawSessionId == DrawerSelector.Instance.DrawerSession.DrawSessionId).ToList();
                foreach (var s in res)
                {
                    s.CashDetails = ctx.DrawerCashDetails.Where(x => x.DrawSessionId == s.DrawSessionId.ToString() && x.CashTypeComponent.CashType.Name == s.PayCode).ToList();
                }

                DrawSessionDetails = new ObservableCollection<DrawSessionDetail>(res.Where(x => x.CashDetails.Any()).ToList());
            }
        }

        public string DebitEntry => $@"Debit: {DrawerSelector.Instance.DrawerSession.DebitAccountNumber}  {DrawerSelector.Instance.DrawerSession.DebitAccountDescription}  {CashBreakDown.Instance.CashTotal:C}";
        public string CreditEntry => $@"Credit: {DrawerSelector.Instance.DrawerSession.CreditAccountNumber}  {DrawerSelector.Instance.DrawerSession.CreditAccountDescription}  {CashBreakDown.Instance.CashTotal:C}";

        public ObservableCollection<DrawSessionDetail> DrawSessionDetails { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

       
    }
}