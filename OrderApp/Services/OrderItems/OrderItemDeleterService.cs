using RepositoryContracts;
using ServiceContracts.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderItems
{
    public class OrderItemDeleterService(IOrderItemRepository _orderItemRepository) : IOrderItemDeleterService
    {
        public async Task<bool> DeleteOrderItem(Guid orderItemId)
        {
            var isDeleted = await _orderItemRepository.DeleteOrderItem(orderItemId);
            return isDeleted;
        }
    }
}
