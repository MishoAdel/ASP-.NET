using Repositories;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using Services.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orders
{
    public class OrderGetterService(IOrderRepository _orderRepository,IOrderItemGetterService _orderItemGetterService) : IOrderGetterService
    {
        public async Task<List<OrderResponse>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            var orderResponses = orders.ToOrderResponseList();
            foreach (var orderResponse in orderResponses)
            {
                orderResponse.OrderItems = await _orderItemGetterService.GetOrderItemsForOrder(orderResponse.OrderID);
            }

            return orderResponses;

        }

        public async Task<OrderResponse> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByID(orderId);
            var orderResponse = order?.ToOrderResponse();
            orderResponse.OrderItems = await _orderItemGetterService.GetOrderItemsForOrder(orderResponse.OrderID);

            return orderResponse;
        }
    }
}
