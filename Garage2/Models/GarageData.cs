using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Garage2.Models.ViewModels;

namespace Garage2.Models
{
    public class GarageData
    {
        public List<OverviewViewModel> Vehicles { get; set; }

        public GarageModel Garage { get; set; }
    }

    public class GarageModel
    {
        [Range(0, 30)]
        public int SpacesOccupied { get; set; }

        [Required()]
        [ReadOnly(true)]
        public int TotalSpaces { get; private set; }

        [Display(Name = "Total number of cars:")]
        public int TotalNrCars { get; set; } //ToDo change to list
        [Display(Name = "Total number of trucks:")]
        public int TotalNrTrucks { get; set; }
        [Display(Name = "Total number of motorcycles:")]
        public int TotalNrMotorcycles { get; set; }
        [Display(Name = "Total number of boats:")]
        public int TotalNrBoats { get; set; }
        [Display(Name = "Total number of airplanes:")]
        public int TotalNrAirplanes { get; set; }

        [Display(Name = "Total number of wheels:")]
        public int TotalNrOfWheels { get; set; }

        [Display(Name = "Total profit generated:")]
        public int TotalProfit { get; set; }

        public GarageModel()
        {
            TotalSpaces = 30;
        }
    }
}
