using TradeChart.ViewModels;

namespace TradeChart.Models
{
    public class ChartItem : ObservableBase
    {
        private int         daysleft;
        private decimal     sumOfBuys;
        private int         countOfTrades;
        
        public int DaysLeft
        {
            get { return this.daysleft; }
            set { if (this.daysleft == value) return; this.daysleft = value; this.RaisePropertyChanged(p => this.DaysLeft); }
        }

        public decimal SumOfBuys
        {
            get { return this.sumOfBuys; }
            set { if (this.sumOfBuys == value) return; this.sumOfBuys = value; this.RaisePropertyChanged(p => this.SumOfBuys); }
        }

        public int CountOfTrades
        {
            get { return this.countOfTrades; }
            set { if (this.countOfTrades == value) return; this.countOfTrades = value; this.RaisePropertyChanged(p => this.CountOfTrades); }
        }

        public override string ToString() { return string.Format("DaysLeft: {0} SumOfBuys: {1} CountOfTrades: {2}", DaysLeft, SumOfBuys, CountOfTrades); }
    }
}