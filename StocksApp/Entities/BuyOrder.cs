using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BuyOrder
    {
        [Required]
        public Guid BuyOrderID { get; set; }

        [Required(ErrorMessage = "Stock Symbol can not be empty or null")]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock name can not be empty or null")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000 ,ErrorMessage = "Maximum amount of shares is 100000 per order and minimum is 1")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Maximum price of stock is 10000 and minimum is 1")]
        public double Price { get; set; }
    }
}
