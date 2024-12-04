using Microsoft.EntityFrameworkCore;

namespace CarDealershipStaff.Models
{
    public class DatabaseContext : DbContext
    {

        // This class creates a DatabaseContext object that acts as an object representation of the database
        // Using DbSet objects, it binds Car, Lot and Staff Models to database tables of same names

        // Constructor for DatabaseContext object, uses options specified in Program.cs
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Defining database Staff table
        public DbSet<Staff> Staff { get; set;}
    }
}
