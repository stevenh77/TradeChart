using System;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using TradeChart.Models;
using TradeChart.Services;

namespace TradeChart.ViewModels
{
    public class ShellViewModel : ObservableBase
    {
        #region Dependencies

        private readonly IFactoryService service;

        #endregion

        #region Constructor

        public ShellViewModel(IFactoryService service)
        {
            if (service == null) throw new ArgumentNullException("IFactoryService is missing");
            
            this.service = service;
            Trades = this.service.CreateTrades();
            ChartItems = this.service.CreateChartData(Trades);

            WireUpCommands();
        }

        #endregion

        #region Public Properties
        
        public Trades       Trades                  { get; private set; }
        public ChartItems   ChartItems              { get; private set; }
        public bool         ShowKnowledgeSlider     { get { return selectedItem != null; } }
        public bool         ShowAdditionalSliders   { get; set; }

        private Trade selectedItem;
        public Trade SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value) return;
                selectedItem = value;
                this.RaisePropertyChanged(p => this.SelectedItem);
                this.RaisePropertyChanged(p => this.ShowKnowledgeSlider);
            }
        }

        #endregion

        #region Commands

		public DelegateCommand SaveTradeCommand     { get; set; }
        public DelegateCommand NavBackCommand       { get; set; }
        public DelegateCommand NavForwardCommand    { get; set; }
		
        private void WireUpCommands()
        {
            this.NavBackCommand = new DelegateCommand(OnNavBackCommand);
            this.NavForwardCommand = new DelegateCommand(OnNavForwardCommand);
            this.SaveTradeCommand = new DelegateCommand(OnSaveTradeCommand);
        }

        private void OnNavBackCommand()
        {
            if (SelectedItem == null || Trades.IndexOf(SelectedItem) == 0) return;
            SelectedItem = Trades[Trades.IndexOf(SelectedItem) - 1];
        }

        private void OnNavForwardCommand()
        {
            if (SelectedItem == null || Trades.IndexOf(SelectedItem) == Trades.Count - 1) return;
            SelectedItem = Trades[Trades.IndexOf(SelectedItem) + 1];
        }

        private void OnSaveTradeCommand()
        {
            if (SelectedItem == null) return;

            UpdateChartData();

            var index = Trades.IndexOf(SelectedItem);

            Trades.Remove(SelectedItem);

            if (Trades.Count == 0)
            {
                this.RaisePropertyChanged(p => this.ShowAdditionalSliders);
                return;
            }

            if (index > Trades.Count - 1) index--;

            SelectedItem = Trades[index];
        }

        private void UpdateChartData()
        {
            var chartUnit = ChartItems.First(x => x.DaysLeft == SelectedItem.DaysLeft);
            chartUnit.CountOfTrades--;
            chartUnit.SumOfBuys = chartUnit.SumOfBuys - SelectedItem.Buys;
        }

        #endregion
    }
}
