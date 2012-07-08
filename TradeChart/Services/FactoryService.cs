using System;
using System.Globalization;
using System.Linq;
using TradeChart.Models;

namespace TradeChart.Services
{
    public class FactoryService : IFactoryService
    {
        public Trades CreateTrades()
        {
            var list = new Trades();
            var rnd = new Random();

            for (int i = 0; i < 50; i++)
            {
                var trade = new Trade()
                    {
                        Buys = rnd.Next(10000, 50000),
                        CIF = "A12345-" + i.ToString(CultureInfo.InvariantCulture),
                        DaysLeft = rnd.Next(1, 15),
                        Id = "T-987-" + i.ToString(CultureInfo.InvariantCulture),
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

        public ChartItems CreateChartData(Trades trades)
        {
            // build query for past fortnight
            var qry = (from t in trades
                       group t by new { t.DaysLeft }
                       into g
                       select new ChartItem()
                                  {
                                      DaysLeft = g.Key.DaysLeft,
                                      SumOfBuys = g.Sum(t => t.Buys),
                                      CountOfTrades = g.Count()
                                  })
                .OrderBy(x => x.DaysLeft)
                .Take(15);

            var data = qry;
            return new ChartItems(data);
        }
    }
}