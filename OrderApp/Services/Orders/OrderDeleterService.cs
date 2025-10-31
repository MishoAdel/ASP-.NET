using OrdersEntities;
using RepositoryContracts;
using ServiceContracts.OrderItems;
using ServiceContracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Orders
{
    public class OrderDeleterService(IOrderRepository _orderRepository) : IOrderDeleterService
    {
        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var isDeleted = await _orderRepository.DeleteOrder(orderId);

            return isDeleted;
        }
    }
}
