using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orders
{
    public class OrderAdderService(IOrderRepository _orderRepository, IOrderItemRepository _orderItemRepository) : IOrderAdderService
    {
        public async Task<OrderResponse> AddOrder(OrderAddRequest orderAddRequest)
        {
            var order = orderAddRequest.ToOrder();
            order.OrderID = Guid.NewGuid();

            var addedOrder = await _orderRepository.AddOrder(order);
            var addedOrderResponse = addedOrder.ToOrderResponse();

            foreach (var item in orderAddRequest.OrderItems)
            {
                var orderItem = item.ToOrderItem();
                orderItem.OrderItemId = Guid.NewGuid();
                orderItem.OrderID = addedOrder.OrderID;

                var addedOrderItem = await _orderItemRepository.AddOrderItem(orderItem);

                addedOrderResponse.OrderItems.Add(addedOrderItem.ToOrderItemResponse());

            }

            return addedOrderResponse;

        }
    }
}
