using OrdersEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> GetOrderByID(Guid orderId);

        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);

        Task<bool> DeleteOrder(Guid orderId);
    }
}
