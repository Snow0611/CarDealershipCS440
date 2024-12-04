using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;

namespace CarDealership.Controllers
{
    public class CarController : Controller
    {

        // Define CarFunctions object
        private readonly CarFunctions _carFunctions;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor for Controller
        // Initializes CarFunctions object to allow use of CarFunctions functions
        public CarController(CarFunctions carFunctions, IHttpClientFactory httpClientFactory)
        {
            _carFunctions = carFunctions;
            _httpClientFactory = httpClientFactory;
        }

        // Serializes List of cars into Json,
        // then posts inventory to api as StringContent
        public async Task PostInventory(HttpClient httpClient, List<Car> cars)
        {

            var stringCars = new StringContent(JsonSerializer.Serialize(cars));

            await httpClient.PostAsync("https://localhost:7179/Customer/Inventory", stringCars);

        }

        // Serializes car into Json,
        // then posts car to api as StringContent
        public async Task PostCar(HttpClient httpClient, List<Car> car)
        {

            var stringCar = new StringContent(JsonSerializer.Serialize(car));

            await httpClient.PostAsync("https://localhost:7179/Customer/Car", stringCar);

        }

        // Returns Index view for the customer
        public ActionResult Index()
        {
            return View();
        }

        // Gets cars from database using getCars() function,
        // then posts cars to api
        public async Task Inventory()
        {
            var cars = _carFunctions.getCars();

            var httpClient = _httpClientFactory.CreateClient("ApiClient");

            await PostInventory(httpClient, cars);
        }

        // Gets car from database by Make and Model using getCarByMakeModel() function,
        // then posts cars to api
        public async Task Car(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            var httpClient = _httpClientFactory.CreateClient("ApiClient");

            await PostCar(httpClient, car);
        }

        // Updates car using updateCar() function,
        // gets car object from api's post,
        // uses car's fields are arguments
        public async void updateCar(HttpClient httpClient)
        {

            // Gets response from api's staff controller's UpdateCar function
            using HttpResponseMessage response = await httpClient.GetAsync("Staff/UpdateCar");

            // Reads the json object received from the api
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializes the json response back to original car object
            var car = JsonSerializer.Deserialize<Car>(jsonResponse);

            // deletes car to database using
            // LotFunctions' deleteCar() function
            _carFunctions.updateCar(car.PlateNumber, car.LotID, car.Make, car.Model,
                car.ModelYear, car.Price, car.Color, car.CarStatus, car.SalesmanID);
        }


        // Sells car by plate number using sellCar() function,
        // gets car's plate number from api's post
        public async void SellCar(HttpClient httpClient)
        {

            // Gets response from api's staff controller's SellCar function
            using HttpResponseMessage response = await httpClient.GetAsync("Staff/SellCar");

            // Reads the json object received from the api
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializes the json response back to original car object
            var car = JsonSerializer.Deserialize<Car>(jsonResponse);

            // deletes car to database using
            // LotFunctions' deleteCar() function
            _carFunctions.sellCar(car.PlateNumber);
        }

    }
}
