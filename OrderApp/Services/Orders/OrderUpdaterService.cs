using Repositories;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orders
{
    public class OrderUpdaterService(IOrderRepository _orderRepository) : IOrderUpdaterService
    {
        public async Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
        {
            var order = orderUpdateRequest.ToOrder();
            var updatedOrder = await _orderRepository.UpdateOrder(order);
            return updatedOrder.ToOrderResponse();
        }
    }
}
