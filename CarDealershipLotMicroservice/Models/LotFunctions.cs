using System.Data;
using System.Xml.Serialization;

namespace CarDealershipLot.Models
{
    public class LotFunctions
    {

        // Defining DatabaseContext object
        private readonly DatabaseContext _databaseContext;

        // Constructor for CarFunctions
        // Initializes Database Context object to allow interaction with database
        public LotFunctions(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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
        public Car createCar(int plateNum, int lotID)
        {
            var car = new Car();

            car.PlateNumber = plateNum;
            car.LotID = lotID;

            return car;
        }

        // Creates a new Car object and adds it to the database
        public void addCar(int plateNum, int lotID) 
        {

            // Creates a new Car object using the createCar() function
            var car = createCar(plateNum, lotID);

            // Adds Car object to database Car table and saves changes to database
            _databaseContext.Add(car);
            _databaseContext.SaveChanges();
        }

        // Moves car to new lot
        public void moveCar(int plateNum, int lotID)
        {

            // Gets car from database by PlateNumber field using built in Find() function
            var car = _databaseContext.Car.Find(plateNum);

            // if car exists in database Car table
            if (car != null)
            {
                car.LotID = lotID;

                // Updates car in database using Update() function,
                // then saves changes to database
                _databaseContext.Car.Update(car);
                _databaseContext.SaveChanges();
            }
        }
    }
}
