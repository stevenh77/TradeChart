using System.ComponentModel;

namespace TradeChart
{
    public class ChartData : INotifyPropertyChanged
    {
        private int daysleft;
        public int DaysLeft
        {
            get { return this.daysleft; }
            set
            {
                if (this.daysleft == value) return;
                this.daysleft = value;
                this.OnPropertyChanged("DaysLeft");
            }
        }

        private decimal sumOfBuys;
        public decimal SumOfBuys
        {
            get { return this.sumOfBuys; }
            set
            {
                if (this.sumOfBuys == value) return;
                this.sumOfBuys = value;
                this.OnPropertyChanged("SumOfBuys");
            }
        }

        private int countOfTrades;
        public int CountOfTrades
        {
            get { return this.countOfTrades; }
            set
            {
                if (this.countOfTrades == value) return;
                this.countOfTrades = value;
                this.OnPropertyChanged("CountOfTrades");
            }
        }

        public new string ToString()
        {
            return string.Format("DaysLeft: {0} SumOfBuys: {1} CountOfTrades: {2}", DaysLeft, SumOfBuys, CountOfTrades);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
