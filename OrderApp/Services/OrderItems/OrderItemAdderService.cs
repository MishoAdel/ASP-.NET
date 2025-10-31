using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderItems
{
    public class OrderItemAdderService(IOrderItemRepository _orderItemRepository) : IOrderItemAdderService
    {
        public async Task<OrderItemResponse> AddOrderItem(OrderItemAddRequest orderItemAddRequest)
        {
            var orderItem = orderItemAddRequest.ToOrderItem();
            orderItem.OrderID = Guid.NewGuid();
            var addedOrderItem = await _orderItemRepository.AddOrderItem(orderItem);
            return addedOrderItem.ToOrderItemResponse();
        }
    }
}
