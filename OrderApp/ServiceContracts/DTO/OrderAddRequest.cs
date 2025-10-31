using OrdersEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class OrderAddRequest
    {
        [Required(ErrorMessage = "Order Number is required")]
        [RegularExpression(@"^(?i)ORD_\d{3}_\d+$", ErrorMessage = "Order Number should be on format ORD_XXX_X")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Order Name is required")]
        [MaxLength(50, ErrorMessage = "Order name can't be longer than 50 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        public DateTime OrderDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Total amount must be positive")]
        [Column(TypeName = "decimal")]
        public decimal TotalAmount { get; set; }

        public List<OrderItemAddRequest> OrderItems { get; set; } = new List<OrderItemAddRequest>();

        public Order ToOrder()
        {
            return new Order
            {
                CustomerName = CustomerName,
                OrderNumber = OrderNumber,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount
            };
        }
    }
}
