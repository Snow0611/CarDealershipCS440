using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Staff
    {

        // Recreating database Staff table as a Model class
        // DatabaseContext matches this model to the database table, then binds to it
        [Key]
        public int StaffID { get; set; }
        public int LotID { get; set; }
        public string? StaffName { get; set; }
        public string? StaffRole {get; set;}
        public int CarsSold { get; set; }
        public int Profit { get; set; }
    }
}
