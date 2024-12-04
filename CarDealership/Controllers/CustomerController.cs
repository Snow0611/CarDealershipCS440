using CarDealershipAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarDealershipAPI.Controllers
{
    public class CustomerController : Controller
    {

        // Define CarFunctions object
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor for Controller
        // Initializes CarFunctions object to allow use of CarFunctions functions
        public CustomerController(IHttpClientFactory httpClientFactory)
        { 
            _httpClientFactory = httpClientFactory;
        }

        // Returns Index view for the customer
        public ActionResult Index()
        {
            return View();
        }

        // Gets inventory from CarMicroservice as Json object,
        // then deseralizes and returns the Inventory car list
        public async Task<List<Car>>? GetInventory(HttpClient httpClient)
        {

            using HttpResponseMessage response = await httpClient.GetAsync("Car/Inventory");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var cars = JsonSerializer.Deserialize<List<Car>>(jsonResponse);

            return cars;
        }

        // Gets cars from CarMicroservice using GetInventory(),
        // then returns Inventory view with cars' data
        public ActionResult Inventory()
        {
            var httpClient = _httpClientFactory.CreateClient("CarClient");

            var cars = GetInventory(httpClient);

            return View(cars);
        }

        // Gets car from CarMicroservice as Json object,
        // then deseralizes and returns the car object
        public async Task<List<Car>>? GetCar(HttpClient httpClient)
        {

            using HttpResponseMessage response = await httpClient.GetAsync("Car/Car");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var car = JsonSerializer.Deserialize<List<Car>>(jsonResponse);

            return car;
        }

        // Gets car from CarMicroservice using GetCar(),
        // then returns Car view with car's data
        public ActionResult Car(string make, string model) 
        {
            var httpClient = _httpClientFactory.CreateClient("CarClient");

            var car = GetCar(httpClient);

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
