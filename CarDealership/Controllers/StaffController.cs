using CarDealershipAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace CarDealershipAPI.Controllers
{
    public class StaffController : Controller
    {
        
        // Defining IHttpClientFactory object
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor for Controller
        // Initializes CarFunctions and StaffFunctions objects,
        // to allow use of their functions
        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Returns Index view for the staff
        public ActionResult Index()
        {
            return View();
        }

        // Creates a new car object to be posted to microservices
        public Car createCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID) 
        {
            Car car = new Car();
            car.PlateNumber = plateNum;
            car.LotID = lotID;
            car.Make = make;
            car.Model = model;
            car.ModelYear = modelYear;
            car.Price = price;
            car.Color = color;
            car.CarStatus = carStatus;
            car.SalesmanID = salesmanID;

            return car;
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

        // Returns Search view
        public ActionResult Search()
        {
            return View();
        }

        // Returns Sell view
        public ActionResult Sell()
        {
            return View();
        }

        // Sells car by posting plate number to Car and Lot 
        // Microservices' SellCar() Functions
        public async Task Sold(HttpClient httpClient, int plateNum)
        {
            var carClient = _httpClientFactory.CreateClient("CarClient");
            var lotClient = _httpClientFactory.CreateClient("LotClient");

            var stringCar = new StringContent(JsonSerializer.Serialize(plateNum));

            await carClient.PostAsync("https://localhost:7180/Car/SellCar", stringCar);
            await lotClient.PostAsync("https://localhost:7182/Lot/SellCar", stringCar);
        }

        // Returns CarForm view
        public ActionResult CarForm() 
        {
            return View();
        }

        // Creates a new car and serializes it as String content
        // of a Json object then posts the Json object to the 
        // Car and Lot Microservices AddCar() functions
        public async Task Add(HttpClient httpClient, int plateNum, int lotID, string make,
            string model, int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            var car = createCar(plateNum, lotID, make, model, modelYear, price, color, 
                carStatus, salesmanID);

            var carClient = _httpClientFactory.CreateClient("CarClient");
            var lotClient = _httpClientFactory.CreateClient("LotClient");

            var stringCar = new StringContent(JsonSerializer.Serialize(car));

            await carClient.PostAsync("https://localhost:7180/Car/AddCar", stringCar);
            await lotClient.PostAsync("https://localhost:7182/Lot/AddCar", stringCar);

        }

        // Returns UpdateSearch view
        public ActionResult UpdateSearch()
        {
            return View();
        }

        // Creates a new car and serializes it as String content
        // of a Json object then posts the Json object to the 
        // Car and Lot Microservices' Update() functions
        public async Task Update(HttpClient httpClient, int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID) 
        {
            var car = createCar(plateNum, lotID, make, model, modelYear, price, color,
                carStatus, salesmanID);

            var carClient = _httpClientFactory.CreateClient("CarClient");
            var lotClient = _httpClientFactory.CreateClient("LotClient");

            var stringCar = new StringContent(JsonSerializer.Serialize(car));

            await carClient.PostAsync("https://localhost:7180/Car/UpdateCar", stringCar);
            await lotClient.PostAsync("https://localhost:7182/Lot/UpdateCar", stringCar);
        }

        // Returns MoveCarSearch view
        public ActionResult MoveCarSearch()
        {
            return View();
        }

        // Moves car between lots by creating new car object,
        // then posting it to Lot Microservice
        public async Task MoveCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            var car = createCar(plateNum, lotID, make, model, modelYear, price, color,
                 carStatus, salesmanID);

            var lotClient = _httpClientFactory.CreateClient("LotClient");

            var stringCar = new StringContent(JsonSerializer.Serialize(car));

            await lotClient.PostAsync("https://localhost:7182/Lot/MoveCar", stringCar);
        }

        // Returns MaintenanceForm view
        public ActionResult MaintenanceForm() 
        { 
            return View();
        }

        // Returns AssignSalesmanSearch view
        public ActionResult AssignSalesmanSearch() 
        {
            return View();
        }

        // Returns SalesReportSearch view
        public ActionResult SalesReportSearch()
        {
            return View();
        }
    }
}
