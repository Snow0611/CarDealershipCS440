using System.ComponentModel.DataAnnotations;

namespace CarDealershipLot.Models
{
    public class Car
    {

        // Recreating database Car table as a Model class
        // DatabaseContext matches this model to the database table, then binds to it
        [Key]
        public int PlateNumber { get; set; }
	    public int LotID { get; set; }
    }
}
