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
    public class OrderItemUpdaterService(IOrderItemRepository _orderItemRepository) : IOrderItemUpdaterService
    {
        public async Task<OrderItemResponse> UpdateOrderItem(OrderItemUpdateRequest orderItemUpdateRequest)
        {
            var orderItem = orderItemUpdateRequest.ToOrderItem();
            var updatedOrderItem = await _orderItemRepository.UpdateOrderItem(orderItem);

            return updatedOrderItem.ToOrderItemResponse();
        }
    }
}
