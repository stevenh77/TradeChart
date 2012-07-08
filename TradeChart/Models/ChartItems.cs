using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TradeChart.Models
{
    public class ChartItems : ObservableCollection<ChartItem>
    {
        public ChartItems(IEnumerable<ChartItem> collection) : base(collection) { }
    }
}
