using Microsoft.AspNetCore.Mvc;

namespace CarDealership.BusinessLogicLayer.Controllers
{
    public class IndexController : Controller
    {

        // Returns default Index page for the web api
        public IActionResult Index()
        {
            return View();
        }
    }
}