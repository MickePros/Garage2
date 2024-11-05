using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class EditVehicleViewModel
    {
        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }

        public string RegNr { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        [Range(0, 18)]
        public int Wheels { get; set; }
    }
}
