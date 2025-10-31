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
    public class OrderItemRepository(OrdersDBContext _db,ILogger<OrderItemRepository> _logger) : IOrderItemRepository
    {

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            _logger.LogInformation("Tring to add orderItem");

            _db.OrderItems.Add(orderItem);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order Item added successfully");

            return orderItem;

        }

        public async Task<bool> DeleteOrderItem(Guid orderItemId)
        {
            _logger.LogInformation("Tring to delete order item with ID : {ID}", orderItemId);

            var orderItem = await GetOrderItemByID(orderItemId);

            if (orderItem == null)
                return false;

            _db.OrderItems.Remove(orderItem);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order Item deleted successfully");

            return true;

        }

        public Task<List<OrderItem>> GetAllOrderItems()
        {
            _logger.LogInformation("Tring to get all orders Items");

            var orderItems = _db.OrderItems.ToListAsync();

            _logger.LogInformation("Order Items fetched successfully");

            return orderItems;

        }

        public async Task<List<OrderItem>> GetAllOrderItemsForOrder(Guid orderId)
        {
            _logger.LogInformation("Tring to get all orders Items for order : {ID}",orderId);

            var orderItems = await _db.OrderItems.Where(o => o.OrderID == orderId).ToListAsync();

            _logger.LogInformation("Order Items fetched successfully");

            return orderItems;
        }

        public async Task<OrderItem> GetOrderItemByID(Guid orderItemId)
        {
            _logger.LogInformation("Tring to get order item with ID : {ID}", orderItemId);

            var orderItem = await _db.OrderItems.FirstOrDefaultAsync(o => o.OrderItemId == orderItemId);
            if (orderItem == null)
                _logger.LogInformation("Order item not found");
            else
                _logger.LogInformation("Order Item found successfully");

            return orderItem;
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            _logger.LogInformation("Tring to update order with ID : {ID}", orderItem.OrderID);

            var existingOrderItem = await GetOrderItemByID(orderItem.OrderItemId);

            if(existingOrderItem == null)
            {
                _logger.LogInformation("Order item not found");
                return orderItem;
            }

            existingOrderItem.UnitPrice = orderItem.UnitPrice;
            existingOrderItem.TotalPrice = orderItem.TotalPrice;
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.OrderID = orderItem.OrderID;
            existingOrderItem.ProductName = orderItem.ProductName;

            await _db.SaveChangesAsync();

            _logger.LogInformation("Order Item updated successfully");

            return existingOrderItem;

        }
    }
}
