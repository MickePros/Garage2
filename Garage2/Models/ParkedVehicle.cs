using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class ParkedVehicle
    {
        public VehicleType VehicleType { get; set; }

        [Key]
        [Display(Name = "License plate")]
        public string RegNr { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        [Range(0, 18)]
        public int Wheels { get; set; }

        [Required()]
        [ReadOnly(true)]
        public DateTime Arrival { get; private set; }

        public ParkedVehicle()
        {
            Arrival = DateTime.Now;
        }
    }
}
