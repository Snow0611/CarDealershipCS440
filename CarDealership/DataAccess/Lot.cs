using System.ComponentModel.DataAnnotations;

namespace CarDealership.DataAccess
{
    public class Lot
    {

        // Recreating database Lot table as a Model class
        // DatabaseContext matches this model to the database table, then binds to it
        [Key]
        public int LotID { get; set; }
        public string? LotLocation { get; set; }
    }
}
