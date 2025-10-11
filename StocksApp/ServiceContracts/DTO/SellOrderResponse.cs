using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderId { get; set; }

        [Required(ErrorMessage = "Stock Symbol can not be empty or null")]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can not be empty or null")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Maximum amount of shares per order is 100000 and minimum is 1")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Maximum price of stock is 10000 and minimum is 1")]
        public double Price { get; set; }

        public double TradeAmount { get; set; }
    }

    public static class SellOrderExtension
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderId = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Price * sellOrder.Quantity,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
            };
        }
    }
}
