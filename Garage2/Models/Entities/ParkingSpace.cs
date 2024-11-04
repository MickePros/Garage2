using System.ComponentModel.DataAnnotations;

namespace Garage2.Models.Entities
{
    public class ParkingSpace
    {
        [Key]
        public int ParkingId { get; set; }

        public int Points { get; set; }
    }
}
