using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderItems
{
    public class OrderItemGetterService(IOrderItemRepository _orderItemRepository) : IOrderItemGetterService
    {
        public async Task<List<OrderItemResponse>> GetAllOrderItems()
        {
            var orderItems = await _orderItemRepository.GetAllOrderItems();

            return orderItems.ToOrderItemResponseList();
        }

        public async Task<OrderItemResponse> GetOrderItemById(Guid orderItemId)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByID(orderItemId);

            return orderItem.ToOrderItemResponse();
        }

        public async Task<List<OrderItemResponse>> GetOrderItemsForOrder(Guid orderId)
        {
            var orderItems = await _orderItemRepository.GetAllOrderItemsForOrder(orderId);

            return orderItems.ToOrderItemResponseList();
        }
    }
}
