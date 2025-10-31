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
    public class OrderItemUpdateRequest
    {
        public Guid OrderItemId { get; set; }


        [Required(ErrorMessage = "Order ID is required")]
        public Guid OrderID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(50, ErrorMessage = "Product name can't be longer than 50 characters")]
        public string ProductName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value")]
        public int Quantity { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Unit Price must be a positive value")]
        public decimal UnitPrice { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Total Price must be a positive value")]
        public decimal TotalPrice { get; set; }

        public OrderItem ToOrderItem()
        {
            return new OrderItem
            {
                OrderItemId = OrderItemId,
                OrderID = OrderID,
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice
            };
        }

    }
}
