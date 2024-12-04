using CarDealershipLot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarDealershipLot.Controllers
{
    public class LotController : Controller
    {

        // Defining LotFunctions and IHttpClientFactory objects
        private readonly LotFunctions _lotFunctions;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor for Controller
        // Initializes LotFunctions and IHttpClientFactory objects,
        // to allow use of their functions
        public LotController(LotFunctions lotFunctions, IHttpClientFactory httpClientFactory)
        {
            _lotFunctions = lotFunctions;
            _httpClientFactory = httpClientFactory;
        }

        // Serializes car into Json,
        // then posts car to api as StringContent
        public async Task PostCar(HttpClient httpClient, List<Car> car)
        {

            var stringCar = new StringContent(JsonSerializer.Serialize(car));

            await httpClient.PostAsync("https://localhost:7179/Customer/Car", stringCar);

        }

        // Gets car from database by plate number using getCarByPlate() function,
        // then posts cars to api
        public async Task Car(int plateNum)
        {
            var car = _lotFunctions.getCarByPlate(plateNum);

            var httpClient = _httpClientFactory.CreateClient("ApiClient");

            await PostCar(httpClient, car);
        }

        // Deletes by plate number using sellCar() function,
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
            // LotFunctions' sellCar() function
            _lotFunctions.sellCar(car.PlateNumber);
        }

        // Adds car to database using addCar() function,
        // gets arguments from api's post
        public async void Add(HttpClient httpClient)
        {

            // Gets response from api's staff controller's AddCar function
            using HttpResponseMessage response = await httpClient.GetAsync("Staff/AddCar");

            // Reads the json object received from the api
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializes the json response back to original car object
            var car = JsonSerializer.Deserialize<Car>(jsonResponse);

            // Adds new car to database using
            // LotFunctions' addCar() function
            _lotFunctions.addCar(car.PlateNumber, car.LotID);
        }

        // Updates car's LotID using moveCar() function,
        // gets arguments from api's post
        public async void MoveCar(HttpClient httpClient)
        {

            // Gets response from api's staff controller's MoveCar function
            using HttpResponseMessage response = await httpClient.GetAsync("Staff/MoveCar");

            // Reads the json object received from the api
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            // Deserializes the json response back to original car object
            var car = JsonSerializer.Deserialize<Car>(jsonResponse);

            // Updates car's LotID field to new LotID using
            // LotFunctions' moveCar() function
            _lotFunctions.moveCar(car.PlateNumber, car.LotID);
        }
    }
}
