using System.Data;
using System.Text.Json;

namespace CarDealership.Models
{
    public class CarFunctions
    {

        // Defining DatabaseContext object
        private readonly DatabaseContext _databaseContext;
        private readonly HttpClient _httpClient;

        // Constructor for CarFunctions
        // Initializes Database Context object to allow interaction with database
        public CarFunctions(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _httpClient = new HttpClient();
        }

        // Returns a list of all rows of Car table in database
        public List<Car> getCars()
        {
            return _databaseContext.Car.ToList();
        }

        // Returns a car as a List object from database by PlateNumber field
        public List<Car> getCarByPlate(int plateNum)
        {
            return _databaseContext.Car.Where(c => c.PlateNumber == plateNum).ToList();
        }

        // Returns all cars as a List object from database by Make and Model fields
        public List<Car> getCarByMakeModel(string make, string model)
        {
            return _databaseContext.Car.Where(c => c.Make.Equals(make) && c.Model.Equals(model)).ToList();
        }

        // Removes car from database Car table
        public void sellCar(int plateNum)
        {
            // gets car from database by plate number using built in Find() function
            var car = _databaseContext.Car.Find(plateNum);

            if (car != null)
            {

                // Removes car from database using built in Remove() function,
                // then saves changes to database using built in SaveChanges() function
                _databaseContext.Car.Remove(car);
                _databaseContext.SaveChanges();
            }
        }

        // Creates and returns a new Car object
        public Car createCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            var car = new Car();

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

        // Creates a new Car object and adds it to the database
        public void addCar(int plateNum, int lotID, string make, string model, 
            int modelYear, int price, string color, string carStatus, int salesmanID) 
        {

            // Creates a new Car object using the createCar() function
            var car = createCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            // Adds Car object to database Car table and saves changes to database
            _databaseContext.Add(car);
            _databaseContext.SaveChanges();
        }

        // Updates car in database if user enters new column values
        public void updateCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            // Gets car from database by PlateNumber field using built in Find() function
            var car = _databaseContext.Car.Find(plateNum);

            // if car exists in database Car table
            if (car != null)
            {

                // All if statements check if user entered anything other than
                // default value returned by html form (i.e. if user changed any table column values)
                if (lotID != -1) 
                {
                    car.LotID = lotID;
                }

                if (make != "null")
                {
                    car.Make = make;
                }

                if (model != "null")
                {
                    car.Model = model;
                }

                if (modelYear != 0)
                {
                    car.ModelYear = modelYear;
                }

                if (price != 0)
                {
                    car.Price = price;
                }

                if (color != "null")
                {
                    car.Color = color;
                }

                if (carStatus != "null")
                {
                    car.CarStatus = carStatus;
                }

                if (salesmanID != -1)
                {
                    car.SalesmanID = salesmanID;
                }

                // Updates car in database using Update() function,
                // then saves changes to database
                _databaseContext.Car.Update(car);
                _databaseContext.SaveChanges();
            }
        }
    }
}
