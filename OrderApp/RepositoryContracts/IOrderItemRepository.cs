using OrdersEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllOrderItems();

        Task<List<OrderItem>> GetAllOrderItemsForOrder(Guid orderId);
        Task<OrderItem> GetOrderItemByID(Guid orderItemId);

        
        Task<OrderItem> AddOrderItem(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItem(OrderItem orderItem);

        Task<bool> DeleteOrderItem(Guid orderItemId);
    }
}
