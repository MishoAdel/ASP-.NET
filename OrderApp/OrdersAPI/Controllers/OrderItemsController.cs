using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using ServiceContracts.OrderItems;
using Services.OrderItems;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController(IOrderItemAdderService orderItemAdderService, IOrderItemDeleterService orderItemDeleterService, IOrderItemUpdaterService orderItemUpdaterService, IOrderItemGetterService orderItemGetterService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItemsByOrderId(Guid orderId)
        {

            var orderItems = await orderItemGetterService.GetOrderItemsForOrder(orderId);


            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemResponse?>> GetOrderItemById(Guid id)
        {

            var orderItem = await orderItemGetterService.GetOrderItemById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemResponse>> AddOrderItem(Guid orderId, OrderItemAddRequest orderItemRequest)
        {

            var addedOrderItem = await orderItemAdderService.AddOrderItem(orderItemRequest);


            return CreatedAtAction(nameof(GetOrderItemById), new { id = addedOrderItem.OrderItemId }, addedOrderItem);
        }


        /// <summary>
        /// Updates an existing order item.
        /// </summary>
        /// <param name="id">The ID of the order item.</param>
        /// <param name="orderItemRequest">The request containing the updated order item data.</param>
        /// <returns>The updated order item, or 400 Bad Request if the ID doesn't match the request.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItemResponse>> UpdateOrderItem(Guid id, OrderItemUpdateRequest orderItemRequest)
        {
            if (id != orderItemRequest.OrderItemId)
            {
                return BadRequest();
            }


            var updatedOrderItem = await orderItemUpdaterService.UpdateOrderItem(orderItemRequest);

            return Ok(updatedOrderItem);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(Guid orderId, Guid id)
        {

            var isDeleted = await orderItemDeleterService.DeleteOrderItem(id);

            if (!isDeleted)
            {
                return NotFound();
            }


            return NoContent();
        }
    }
}
