using CarDealership.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.BusinessLogicLayer
{
    public class BusinessLogicLayer : Controller
    {

        // Define CarFunctions object
        private readonly CarFunctions _carFunctions;
        private readonly StaffFunctions _staffFunctions;

        // Constructor for Controller
        // Initializes CarFunctions and StaffFunctions objects,
        // to allow use of their functions
        public BusinessLogicLayer(CarFunctions carFunctions, StaffFunctions staffFunctions)
        {
            _carFunctions = carFunctions;
            _staffFunctions = staffFunctions;
        }

        // Returns Index view for the web site
        public ActionResult Index()
        {
            return View("~/Presentation/Pages/Index/index.cshtml");
        }

        // Returns Index view for the customer
        public ActionResult CustomerIndex()
        {
            return View("~/Presentation/Pages/Customer/CustomerIndex.cshtml");
        }

        // Returns Index view for the staff
        public ActionResult StaffIndex()
        {
            return View("~/Presentation/Pages/Staff/StaffIndex.cshtml");
        }

        // Gets cars from database using getCars() function,
        // then returns Inventory view with cars' data
        public ActionResult Inventory()
        {
            var cars = _carFunctions.getCars();

            return View("~/Presentation/Pages/Customer/Inventory.cshtml", cars);
        }

        // Gets car from database by Make and Model using getCarByMakeModel() function,
        // then returns car detail view with car's data
        [HttpGet]
        public ActionResult CustomerCar(string make, string model)
        {
            var car = _carFunctions.getCarByMakeModel(make, model);

            return View("~/Presentation/Pages/Customer/Car.cshtml", car);
        }

        // Returns Buy view
        public ActionResult Buy()
        {
            return View("~/Presentation/Pages/Customer/Buy.cshtml");
        }

        // Returns Search view for customer
        public ActionResult CustomerSearch()
        {
            return View("~/Presentation/Pages/Customer/CustomerSearch.cshtml");
        }

        // Returns Search view for staff
        public ActionResult StaffSearch()
        {
            return View("~/Presentation/Pages/Staff/StaffSearch.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function,
        // then returns the Car detail view
        [HttpGet]
        public ActionResult StaffCar(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            return View("~/Presentation/Pages/staff/Car.cshtml", car);
        }

        // Returns Sell view
        public ActionResult Sell()
        {
            return View("~/Presentation/Pages/Staff/Sell.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function,
        // passes car into sellCar() function,
        // then returns Sold view with car's data
        [HttpGet]
        public ActionResult Sold(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            _carFunctions.sellCar(plateNum);

            return View("~/Presentation/Pages/Staff/Sold.cshtml", car);
        }

        // Returns CarForm view
        public ActionResult CarForm()
        {
            return View("~/Presentation/Pages/Staff/CarForm.cshtml");
        }

        // Adds car to database using addCar() function,
        // then returns Add view
        public ActionResult Add(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.addCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/Presentation/Pages/Staff/Add.cshtml");
        }

        // Returns UpdateSearch view
        public ActionResult UpdateSearch()
        {
            return View("~/Presentation/Pages/Staff/UpdateSearch.cshtml");
        }

        // Finds car in database by plate number using getCarByPlate() function,
        // then returns UpdateForm view with car's data
        public ActionResult UpdateForm(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            return View("~/Presentation/Pages/Staff/UpdateForm.cshtml", car);
        }

        // Updates car using updateCar() function,
        // then returns Update view
        public ActionResult Update(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/Presentation/Pages/Staff/Update.cshtml");
        }

        // Returns MoveCarSearch view
        public ActionResult MoveCarSearch()
        {
            return View("~/Presentation/Pages/Staff/MoveCarSearch.cshtml");
        }

        // Finds car in database by plate number using getCarByPlate() function
        // then returns MoveCarForm view with car's data
        public ActionResult MoveCarForm(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            return View("~/Presentation/Pages/Staff/MoveCarForm.cshtml", car);
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

            return View("~/Presentation/Pages/Staff/MoveCar.cshtml");
        }

        // Returns MaintenanceForm view
        public ActionResult MaintenanceForm()
        {
            return View("~/Presentation/Pages/Staff/MaintenanceForm.cshtml");
        }

        // Updates car's CarStatus field using the updateCar() function,
        // then returns Maintenance view
        public ActionResult Maintenance(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/Presentation/Pages/Staff/Maintenance.cshtml");
        }

        // Returns AssignSalesmanSearch view
        public ActionResult AssignSalesmanSearch()
        {
            return View("~/Presentation/Pages/Staff/AssignSalesmanSearch.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function
        // then returns AssignSalesmanForm view with car's data
        public ActionResult AssignSalesmanForm(int plateNum)
        {
            var car = _carFunctions.getCarByPlate(plateNum);

            return View("~/Presentation/Pages/Staff/AssignSalesmanForm.cshtml", car);
        }

        // Updates car's SalesmanID field using updateCar() function,
        // then returns AssignSalesman view
        public ActionResult AssignSalesman(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            _carFunctions.updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/Presentation/Pages/Staff/AssignSalesman.cshtml");
        }

        // Returns SalesReportSearch view
        public ActionResult SalesReportSearch()
        {
            return View("~/Presentation/Pages/Staff/SalesReportSearch.cshtml");
        }

        // Gets staff from database by StaffID field using getStaff() function,
        // then returns SalesReport view with staff's data
        public ActionResult SalesReport(int staffID)
        {
            var staff = _staffFunctions.getStaff(staffID);

            return View("~/Presentation/Pages/Staff/SalesReport.cshtml", staff);
        }
    }
}
