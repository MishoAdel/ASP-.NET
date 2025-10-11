using Microsoft.AspNetCore.Mvc;
using ModelBindingAssignment.Models;

namespace ModelBindingAssignment.Controllers
{
    public class StoreController : Controller
    {
        [Route("/order")]
        public IActionResult GetOrder([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))] Order order)
        {
            if (ModelState.IsValid) 
            { 
                Random random = new Random();
                int orderNum = random.Next(1,9999);
                return Json(new { orderNumber = orderNum });
            }
            string messages = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(messages);
        }
    }
}
