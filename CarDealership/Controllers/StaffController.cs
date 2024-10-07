using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Controllers
{
    public class StaffController : Controller
    {
        
        // Defining CarFunctions and StaffFunctions objects
        private readonly CarFunctions _carFunctions;
        private readonly StaffFunctions _staffFunctions;

        // Constructor for Controller
        // Initializes CarFunctions and StaffFunctions objects,
        // to allow use of their functions
        public StaffController(CarFunctions carFunctions, StaffFunctions staffFunctions)
        {
            _carFunctions = carFunctions;
            _staffFunctions = staffFunctions;
        }

        // Returns Index view for the staff
        public ActionResult Index()
        {
            return View();
        }
        
        // Gets all cars from database table using getCars() function,
        // then returns the Inventory view with all cars' data
        public ActionResult Inventory()
        {
            var cars = _carFunctions.getCars();

            return View(cars);
        }

        // Gets car from database by plate number using getCar() function,
        // then returns the Car detail view
        [HttpGet]
        public ActionResult Car(int plateNum)
        {
            var car = _carFunctions.getCar(plateNum);

            return View(car);
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

        // Gets car from database by plate number using getCar() function,
        // passes car into sellCar() function,
        // then returns Sold view with car's data
        [HttpGet]
        public ActionResult Sold(int plateNum)
        {
            var car = _carFunctions.getCar(plateNum);

            _carFunctions.sellCar(plateNum);

            return View(car);
        }

        // Returns CarForm view
        public ActionResult CarForm() 
        {
            return View();
        }

        // Adds car to database using addCar() function,
        // then returns Add view
        public ActionResult Add(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.addCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View();
        }

        // Returns UpdateSearch view
        public ActionResult UpdateSearch()
        {
            return View();
        }

        // Finds car in database by plate number using getCar() function,
        // then returns UpdateForm view with car's data
        public ActionResult UpdateForm(int plateNum)
        {
            var car = _carFunctions.getCar(plateNum);

            return View(car);
        }

        // Updates car using updateCar() function,
        // then returns Update view
        public ActionResult Update(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID) 
        {

            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View();
        }

        // Returns MoveCarSearch view
        public ActionResult MoveCarSearch()
        {
            return View();
        }

        // Finds car in database by plate number using getCar() function
        // then returns MoveCarForm view with car's data
        public ActionResult MoveCarForm(int plateNum)
        {
            var car = _carFunctions.getCar(plateNum);

            return View(car);
        }

        // Updates car's LotID and CarStatus fields using updateCar() function,
        // then returns MoveCar view
        public ActionResult MoveCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            // Updates car's LotID field to new LotID and sets car's CarStatus field to "InTransit",
            // using updateCar() function
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, "InTransit", salesmanID);

            // (Lot transfer is now complete) Updates car's CarStatus field to "Available" using updateCar() function
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, "Available", salesmanID);

            return View();
        }

        // Returns MaintenanceForm view
        public ActionResult MaintenanceForm() 
        { 
            return View();
        }

        // Updates car's CarStatus field using the updateCar() function,
        // then returns Maintenance view
        public ActionResult Maintenance(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View();
        }

        // Returns AssignSalesmanSearch view
        public ActionResult AssignSalesmanSearch() 
        {
            return View();
        }

        // Gets car from database by plate number using getCar() function
        // then returns AssignSalesmanForm view with car's data
        public ActionResult AssignSalesmanForm(int plateNum) 
        {
            var car = _carFunctions.getCar(plateNum);

            return View(car);
        }

        // Updates car's SalesmanID field using updateCar() function,
        // then returns AssignSalesman view
        public ActionResult AssignSalesman(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View();
        }

        // Returns SalesReportSearch view
        public ActionResult SalesReportSearch()
        {
            return View();
        }

        // Gets staff from database by StaffID field using getStaff() function,
        // then returns SalesReport view with staff's data
        public ActionResult SalesReport(int staffID)
        {
            var staff = _staffFunctions.getStaff(staffID);

            return View(staff);
        }
    }
}
