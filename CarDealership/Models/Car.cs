using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Car
    {

        // Recreating database Car table as a Model class
        // DatabaseContext matches this model to the database table, then binds to it
        [Key]
        public int PlateNumber { get; set; }
	    public int LotID { get; set; } 
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int ModelYear { get; set; }
        public int Price { get; set; }
	    public string? Color { get; set; }
        public string? CarStatus { get; set; }
        public int SalesmanID { get; set; }
    }
}
