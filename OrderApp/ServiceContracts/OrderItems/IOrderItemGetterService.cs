using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.OrderItems
{
    public interface IOrderItemGetterService
    {
        Task<List<OrderItemResponse>> GetAllOrderItems();
        Task<OrderItemResponse> GetOrderItemById(Guid orderItemId);

        Task<List<OrderItemResponse>> GetOrderItemsForOrder(Guid orderId);
    }
}
