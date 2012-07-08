using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace TradeChart
{
    public class TradeChartViewModel : INotifyPropertyChanged
    {
        #region Private Statics Fields

        private static ObservableCollection<Trade> tradeData { get; set; }
        private static ObservableCollection<ChartData> chartData { get; set; }

        #endregion

        #region Public Properties
        
        public ObservableCollection<Trade> TradeData
        { 
            get { return tradeData ?? (tradeData = CreateTrades()); }
        }

        public ObservableCollection<ChartData> ChartData
        {
            get { return chartData ?? (chartData = CreateChartData()); }
        }

        private Trade selectedItem = null;
        public Trade SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value) return;
                selectedItem = value;
                this.OnPropertyChanged("SelectedItem");
            }
        }

        #endregion

        #region Commands

		public DelegateCommand          SaveTradeCommand        { get; set; }
        public DelegateCommand          NavBackCommand          { get; set; }
        public DelegateCommand          NavForwardCommand       { get; set; }
		
        private void WireUpCommands()
        {
            this.NavBackCommand = new DelegateCommand(() =>
                {
                    if (SelectedItem == null || TradeData.IndexOf(SelectedItem)==0) return;
                    SelectedItem = TradeData[TradeData.IndexOf(SelectedItem)-1];
                });

            this.NavForwardCommand = new DelegateCommand(() =>
            {
                if (SelectedItem == null || TradeData.IndexOf(SelectedItem) == TradeData.Count-1) return;
                SelectedItem = TradeData[TradeData.IndexOf(SelectedItem) + 1];
            });

            this.SaveTradeCommand = new DelegateCommand(() =>
            {
                if (SelectedItem == null) return;

                UpdateChartData();

                var index = TradeData.IndexOf(SelectedItem);
                
                TradeData.Remove(SelectedItem);
                
                if (TradeData.Count == 0) return;
                
                if (index > TradeData.Count - 1) index--;
                
                SelectedItem = TradeData[index];                
            });
        }

        private void UpdateChartData()
        {
            var chartUnit = ChartData.First(x => x.DaysLeft == SelectedItem.DaysLeft);
            chartUnit.CountOfTrades--;
            chartUnit.SumOfBuys = chartUnit.SumOfBuys - SelectedItem.Buys;
        }

        #endregion

        #region Constructor

        public TradeChartViewModel()
        {
			WireUpCommands();
        }

        #endregion

        #region Factories

        public static ObservableCollection<Trade> CreateTrades()
        {
            // create the data
            var list = new ObservableCollection<Trade>();
            var rnd = new Random();

            for (int i = 0; i < 50; i++)
            {
                var trade = new Trade()
                {
                    Buys = rnd.Next(10000, 50000),
					CIF = "A12345-" + i.ToString(),
                    DaysLeft = rnd.Next(1, 15),
                    Id = "T-987-" + i.ToString(),
                    IsAdvised = Convert.ToBoolean(i % 2),
                    Sells = rnd.Next(10000, 50000),
                    Spot = (decimal)0.25,
                    SpotDate = DateTime.Now.AddDays(rnd.Next(1, 15)),
                    TradeDate = DateTime.Now
                };
                list.Add(trade);
            }

            return list;
        }

        private ObservableCollection<ChartData> CreateChartData()
        {
            // build query for last fortnight
            var qry = (from t in TradeData
                       group t by new { t.DaysLeft }
                           into g
                           select new ChartData()
                           {
                               DaysLeft = g.Key.DaysLeft,
                               SumOfBuys = g.Sum(t => t.Buys),
                               CountOfTrades = g.Count()
                           })
                .OrderBy(x => x.DaysLeft)
                .Take(15);

            var data = qry;
            return new ObservableCollection<ChartData>(data);
        }

        #endregion

        #region INPC
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion 
    }
}
