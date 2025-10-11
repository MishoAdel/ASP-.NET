using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Stock Symbol can not be empty or null")]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can not be empty or null")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Maximum amount of shares per order is 100000 and minimum is 1")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Maximum price of stock is 10000 and minimum is 1")]
        public double Price { get; set; }
    
    
        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price,
                StockName = StockName,
                StockSymbol = StockSymbol
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateAndTimeOfOrder < DateTime.Parse("01-01-2000"))
            {
                yield return new ValidationResult("Order date should not be older than Jan 01,2000");
            }
        }
    }
}
