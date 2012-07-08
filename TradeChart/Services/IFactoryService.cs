using TradeChart.Models;

namespace TradeChart.Services
{
    public interface IFactoryService
    {
        Trades CreateTrades();
        ChartItems CreateChartData(Trades trades);
    }
}