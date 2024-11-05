using System;

namespace Garage2.Models
{
    public class ReceiptViewModel
    {
        public string RegNr { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public TimeSpan ParkLength => Departure - Arrival;

        public string ParkLengthFormatted
        {
            get
            {
                var hours = (int)Math.Ceiling(ParkLength.TotalHours);
                return $"{hours} hour{(hours > 1 ? "s" : "")}";
            }
        }

        public decimal TotalFee => (int)Math.Ceiling(ParkLength.TotalHours) * 10; // $10 per started hour
        public string FeeMessage => "The parking fee is 10 USD per started hour.";
    }
}