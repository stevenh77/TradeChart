using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeChart
{
    public class Trade
    {
        public decimal Buys { get; set; }
        public string CIF { get; set; }
        public int DaysLeft { get; set; }
        public string Id { get; set; }		
		public bool IsAdvised { get; set; }
		public decimal Sells { get; set; }
        public decimal Spot { get; set;}
		public DateTime SpotDate { get; set; }
        public DateTime TradeDate { get; set; }
    }
}
