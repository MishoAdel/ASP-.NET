

using OrdersEntities;

namespace ServiceContracts.DTO
{
    public class OrderUpdateRequest
    {
        public Guid OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Order ToOrder()
        {
            return new Order
            {
                OrderID = OrderID,
                OrderNumber = OrderNumber,
                CustomerName = CustomerName,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount,
            };
        }
    }
}
