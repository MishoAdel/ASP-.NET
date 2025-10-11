using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ControllersAssignment.Controllers
{


    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank\r\n\r\n");
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            Account account = new Account(1001, "Misho", 5000);

            return Json(account);
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("~/dummy.pdf","application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult CurrentBalance()
        {
            if (HttpContext.Request.RouteValues.ContainsKey("accountNumber"))
            {
                object acc;
                if( HttpContext.Request.RouteValues.TryGetValue("accountNumber", out acc) && acc is string accountNumber){

                    if (string.IsNullOrEmpty(accountNumber))
                    {
                        return NotFound("Account Number should be supplied");
                    }

                    int accNum = int.Parse(accountNumber);
                    if (accNum == 1001)
                    {
                        return Content(5000.ToString());
                    }
                    else
                    {
                        return StatusCode(400, "Account Number should be 1001\r\n\r\n");
                    }
                }
                else
                {
                    return BadRequest("Invalid Account Number format");
                }

            }
            else
            {
                return StatusCode(404, "Account Number should be supplied");
            }
                
        }
        
    }


}

