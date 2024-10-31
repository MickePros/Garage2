using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class OverviewViewModel
    {
        public VehicleType VehicleType { get; set; }

        [Key]
        public string RegNr { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime ParkLenght { get; set; }
    }
}
