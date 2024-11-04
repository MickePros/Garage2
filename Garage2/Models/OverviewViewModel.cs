using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class OverviewViewModel
    {
        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "License plate")]
        public string RegNr { get; set; }

        public DateTime Arrival { get; set; }

        [ReadOnly(true)]
        public TimeSpan ParkLenght => DateTime.Now - Arrival;

        [Display(Name = "Time parked")]
        public string ParkLengthFormatted
        {
            get
            {
                var days = ParkLenght.Days;
                var hours = ParkLenght.Hours;
                var minutes = ParkLenght.Minutes;

               //Returns a string with only values greater than 0 
                return days > 0
                    ? $"{days}days {hours}h {minutes}min" 
                    : hours > 0
                        ? $"{hours}h {minutes}min"      
                        : $"{minutes} minutes";             
            }
        }
    }
}
