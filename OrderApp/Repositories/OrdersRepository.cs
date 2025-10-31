using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrdersEntities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrdersRepository(OrdersDBContext _db, ILogger<OrdersRepository> _logger) : IOrderRepository
    {
        public async Task<Order> AddOrder(Order order)
        {
            _logger.LogInformation("Tring to add order");

            _db.Orders.Add(order);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order added successfully");

            return order;
            
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            _logger.LogInformation("Tring to delete order with ID : {ID}",orderId);

            var order = await GetOrderByID(orderId);

            if(order == null) 
                return false;

            _db.Orders.Remove(order);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order deleted successfully");

            return true;
        }

        public Task<List<Order>> GetAllOrders()
        {
            _logger.LogInformation("Tring to get all orders");

            var orders = _db.Orders.ToListAsync();

            _logger.LogInformation("Orders fetched successfully");

            return orders;

        }

        public async Task<Order> GetOrderByID(Guid orderId)
        {
            _logger.LogInformation("Tring to get order with ID : {ID}",orderId);

            var order = await _db.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);

            if(order != null)            
                _logger.LogInformation("Order found successfully");
            else
                _logger.LogInformation("Order not found ");
            return order; 
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _logger.LogInformation("Tring to update order with ID : {ID}", order.OrderID );

            var existingOrder = await GetOrderByID(order.OrderID);

            if(existingOrder == null)
            {
                _logger.LogInformation("Order not found ");
                return order;
            }


            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.TotalAmount = order.TotalAmount;

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order updated successfully");

            return existingOrder;

        }
    }
}
