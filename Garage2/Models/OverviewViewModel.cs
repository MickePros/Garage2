using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class OverviewViewModel
    {
        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }

        [Key]
        [Display(Name = "License plate")]
        public string RegNr { get; set; }

        public DateTime Arrival { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Time parked")]
        public TimeSpan ParkLenght {  get; private set; }

        public OverviewViewModel()
        {
            ParkLenght = DateTime.Now - Arrival;
        }
    }
}
