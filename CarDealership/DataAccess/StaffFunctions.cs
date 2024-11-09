namespace CarDealership.DataAccess
{
    public class StaffFunctions
    {

        // Defining DatabaseContext object
        private readonly DatabaseContext _databaseContext;

        // Constructor for StaffFunctions
        // Initializes Database Context object to allow interaction with database
        public StaffFunctions(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // Gets and returns staff from database using staff id
        public Staff? getStaff(int staffID) 
        {

            // Gets staff from database Staff table by StaffID field using build in Find() function
            var staff = _databaseContext.Staff.Find(staffID);

            return staff;
        }
    }
}
