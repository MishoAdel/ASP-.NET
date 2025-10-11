using Microsoft.AspNetCore.Mvc;

namespace StocksApp
{
    public class TradingOptions 
    {

        public string? Top25PopularStocks { get; set; }
        public uint? DefaultOrderQuantity { get; set; }
        
    }
}
