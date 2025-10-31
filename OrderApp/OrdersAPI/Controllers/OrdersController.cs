using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using ServiceContracts.Orders;
using Services.Orders;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderAdderService orderAdderService,IOrderDeleterService orderDeleterService,IOrderUpdaterService orderUpdaterService, IOrderGetterService orderGetterService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
        {

            var orders = await orderGetterService.GetAllOrders();


            return Ok(orders);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id)
        {

            var order = await orderGetterService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderResponse>> AddOrder(OrderAddRequest orderRequest)
        {
        
            var addedOrder = await orderAdderService.AddOrder(orderRequest);

            return CreatedAtAction(nameof(GetOrderById), new { id = addedOrder.OrderID }, addedOrder);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponse>> UpdateOrder(Guid id, OrderUpdateRequest orderRequest)
        {
            if (id != orderRequest.OrderID)
            {
                return BadRequest();
            }

            var updatedOrder = await orderUpdaterService.UpdateOrder(orderRequest);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {

            var isDeleted = await orderDeleterService.DeleteOrder(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
