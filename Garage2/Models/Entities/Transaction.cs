using System.ComponentModel.DataAnnotations;

namespace Garage2.Models.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId {get; set; }

        public int RegNr {get; set;}

        public int ParkingSpaceId { get; set;}   

    }
}
