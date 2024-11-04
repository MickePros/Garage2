using System.ComponentModel.DataAnnotations;

namespace Garage2.Models.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId {get; set; }
        public int VehicleId {get; set;}

        public int SlotId {get; set;}   

    }
}
