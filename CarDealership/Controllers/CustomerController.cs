using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    public class CustomerController : Controller
    {

        // Define CarFunctions object
        private readonly CarFunctions _carFunctions;

        // Constructor for Controller
        // Initializes CarFunctions object to allow use of CarFunctions functions
        public CustomerController(CarFunctions carFunctions)
        { 
            _carFunctions = carFunctions;
        }

        // Returns Index view for the customer
        public ActionResult Index()
        {
            return View();
        }

        // Gets cars from database using getCars() function,
        // then returns Inventory view with cars' data
        public ActionResult Inventory()
        {
            var cars = _carFunctions.getCars();

            return View(cars);
        }

        // Gets car from database by Make and Model using getCarByMakeModel() function,
        // then returns car detail view with car's data
        [HttpGet]
        public ActionResult Car(string make, string model) 
        {
            var car = _carFunctions.getCarByMakeModel(make, model);

            return View(car);
        }

        // Returns Buy view
        public ActionResult Buy()
        {
            return View();
        }

        // Returns Search view
        public ActionResult Search() 
        {
            return View();
        }
    }
}
