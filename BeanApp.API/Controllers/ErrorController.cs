using Microsoft.AspNetCore.Mvc;

namespace BeanApp.API.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the item you requested was not found.";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "An internal server error has occured.";
                    break;
            }

            return View("NotFound");
        }
    }
}
