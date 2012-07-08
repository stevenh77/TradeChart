using TradeChart.Services;
using TradeChart.ViewModels;

namespace TradeChart.Views
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = new ShellViewModel(new FactoryService());
        }
    }
}
