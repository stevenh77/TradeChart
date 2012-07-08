using TradeChart.Services;
using TradeChart.ViewModels;

namespace TradeChart.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new ShellViewModel(new FactoryService());
        }
    }
}
