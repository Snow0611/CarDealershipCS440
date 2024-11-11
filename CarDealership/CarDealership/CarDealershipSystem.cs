using Microsoft.AspNetCore.Mvc;

namespace CarDealership.CarDealership
{

    // Due to the way routing works in .NET, this class must inherit
    // from the Controller class, even though the application is monolithic
    public class CarDealershipSystem : Controller
    {

        // Defining DatabaseContext object
        private readonly DatabaseContext _databaseContext;

        // Constructor that initializes DatabaseContext object
        public CarDealershipSystem(DatabaseContext databaseContext)
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

        // Returns all cars as a List object from database by Make and Model fields
        public List<Car> getCarByMakeModel(string make, string model)
        {
            return _databaseContext.Car.Where(c => c.Make.Equals(make) && c.Model.Equals(model)).ToList();
        }

        // Removes car from database Car table and
        // updates assigned salesman's CarsSold and Profit fields
        public void sellCar(int plateNum)
        {
            // gets car from database by plate number using built in Find() function
            var car = _databaseContext.Car.Find(plateNum);

            if (car != null)
            {
                // Gets staff from database by salesman id using built in Find() function
                var staff = _databaseContext.Staff.Find(car.SalesmanID);

                // Updates car's CarStatus field to "Sold" using updateCar() function
                updateCar(car.PlateNumber, car.LotID, car.Make, car.Model,
                    car.ModelYear, car.Price, car.Color, "Sold", car.SalesmanID);

                // Updates staff object's CarsSold and Profit fields
                staff.CarsSold += 1;
                staff.Profit += car.Price;

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

        // Gets and returns staff from database using staff id
        public Staff? getStaff(int staffID)
        {

            // Gets staff from database Staff table by StaffID field using build in Find() function
            var staff = _databaseContext.Staff.Find(staffID);

            return staff;
        }

        // Returns Index view for the web site
        public ActionResult Index()
        {
            return View("~/CarDealership/Pages/Index/index.cshtml");
        }

        // Returns Index view for the customer
        public ActionResult CustomerIndex()
        {
            return View("~/CarDealership/Pages/Customer/CustomerIndex.cshtml");
        }

        // Returns Index view for the staff
        public ActionResult StaffIndex()
        {
            return View("~/CarDealership/Pages/Staff/StaffIndex.cshtml");
        }

        // Gets cars from database using getCars() function,
        // then returns Inventory view with cars' data
        public ActionResult CustomerInventory()
        {
            var cars = getCars();

            return View("~/CarDealership/Pages/Customer/CustomerInventory.cshtml", cars);
        }

        // Gets cars from database using getCars() function,
        // then returns Inventory view with cars' data
        public ActionResult StaffInventory()
        {
            var cars = getCars();

            return View("~/CarDealership/Pages/Staff/StaffInventory.cshtml", cars);
        }

        // Gets car from database by Make and Model using getCarByMakeModel() function,
        // then returns car detail view with car's data
        [HttpGet]
        public ActionResult CustomerCar(string make, string model)
        {
            var car = getCarByMakeModel(make, model);

            return View("~/CarDealership/Pages/Customer/CustomerCar.cshtml", car);
        }

        // Returns Buy view
        public ActionResult Buy()
        {
            return View("~/CarDealership/Pages/Customer/Buy.cshtml");
        }

        // Returns Search view for customer
        public ActionResult CustomerSearch()
        {
            return View("~/CarDealership/Pages/Customer/CustomerSearch.cshtml");
        }

        // Returns Search view for staff
        public ActionResult StaffSearch()
        {
            return View("~/CarDealership/Pages/Staff/StaffSearch.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function,
        // then returns the Car detail view
        [HttpGet]
        public ActionResult StaffCar(int plateNum)
        {
            var car = getCarByPlate(plateNum);

            return View("~/CarDealership/Pages/Staff/StaffCar.cshtml", car);
        }

        // Returns Sell view
        public ActionResult Sell()
        {
            return View("~/CarDealership/Pages/Staff/Sell.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function,
        // passes car into sellCar() function,
        // then returns Sold view with car's data
        [HttpGet]
        public ActionResult Sold(int plateNum)
        {
            var car = getCarByPlate(plateNum);

            sellCar(plateNum);

            return View("~/CarDealership/Pages/Staff/Sold.cshtml", car);
        }

        // Returns CarForm view
        public ActionResult CarForm()
        {
            return View("~/CarDealership/Pages/Staff/CarForm.cshtml");
        }

        // Adds car to database using addCar() function,
        // then returns Add view
        public ActionResult Add(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            addCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/CarDealership/Pages/Staff/Add.cshtml");
        }

        // Returns UpdateSearch view
        public ActionResult UpdateSearch()
        {
            return View("~/CarDealership/Pages/Staff/UpdateSearch.cshtml");
        }

        // Finds car in database by plate number using getCarByPlate() function,
        // then returns UpdateForm view with car's data
        public ActionResult UpdateForm(int plateNum)
        {
            var car = getCarByPlate(plateNum);

            return View("~/CarDealership/Pages/Staff/UpdateForm.cshtml", car);
        }

        // Updates car using updateCar() function,
        // then returns Update view
        public ActionResult Update(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/CarDealership/Pages/Staff/Update.cshtml");
        }

        // Returns MoveCarSearch view
        public ActionResult MoveCarSearch()
        {
            return View("~/CarDealership/Pages/Staff/MoveCarSearch.cshtml");
        }

        // Finds car in database by plate number using getCarByPlate() function
        // then returns MoveCarForm view with car's data
        public ActionResult MoveCarForm(int plateNum)
        {
            var car = getCarByPlate(plateNum);

            return View("~/CarDealership/Pages/Staff/MoveCarForm.cshtml", car);
        }

        // Updates car's LotID and CarStatus fields using updateCar() function,
        // then returns MoveCar view
        public ActionResult MoveCar(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {

            // Updates car's LotID field to new LotID and sets car's CarStatus field to "InTransit",
            // using updateCar() function
            updateCar(plateNum, lotID, make, model, modelYear, price, color, "InTransit", salesmanID);

            // (Lot transfer is now complete) Updates car's CarStatus field to "Available" using updateCar() function
            updateCar(plateNum, lotID, make, model, modelYear, price, color, "Available", salesmanID);

            return View("~/CarDealership/Pages/Staff/MoveCar.cshtml");
        }

        // Returns MaintenanceForm view
        public ActionResult MaintenanceForm()
        {
            return View("~/CarDealership/Pages/Staff/MaintenanceForm.cshtml");
        }

        // Updates car's CarStatus field using the updateCar() function,
        // then returns Maintenance view
        public ActionResult Maintenance(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/CarDealership/Pages/Staff/Maintenance.cshtml");
        }

        // Returns AssignSalesmanSearch view
        public ActionResult AssignSalesmanSearch()
        {
            return View("~/CarDealership/Pages/Staff/AssignSalesmanSearch.cshtml");
        }

        // Gets car from database by plate number using getCarByPlate() function
        // then returns AssignSalesmanForm view with car's data
        public ActionResult AssignSalesmanForm(int plateNum)
        {
            var car = getCarByPlate(plateNum);

            return View("~/CarDealership/Pages/Staff/AssignSalesmanForm.cshtml", car);
        }

        // Updates car's SalesmanID field using updateCar() function,
        // then returns AssignSalesman view
        public ActionResult AssignSalesman(int plateNum, int lotID, string make, string model,
            int modelYear, int price, string color, string carStatus, int salesmanID)
        {
            updateCar(plateNum, lotID, make, model, modelYear, price, color, carStatus, salesmanID);

            return View("~/CarDealership/Pages/Staff/AssignSalesman.cshtml");
        }

        // Returns SalesReportSearch view
        public ActionResult SalesReportSearch()
        {
            return View("~/CarDealership/Pages/Staff/SalesReportSearch.cshtml");
        }

        // Gets staff from database by StaffID field using getStaff() function,
        // then returns SalesReport view with staff's data
        public ActionResult SalesReport(int staffID)
        {
            var staff = getStaff(staffID);

            return View("~/CarDealership/Pages/Staff/SalesReport.cshtml", staff);
        }
    }
}
