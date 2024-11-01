namespace Garage2.Models
{
    public class ParkingSpaceModel
    {
        private static int Count = 0;
        public int Id { get; set; }

        public int Number { get; private set; } // Parking slot number to display, auto-incremented and read-only

        public bool IsOccupied { get; set; }

        //Meant for storing liscence plates of vehicles that are parked here - can be more than 1 for motorcycles
        public List<string> Vehicle { get; private set; } = new List<string>();

        public ParkingSpaceModel()
        {
            Number = Count++;
        }
    }
}
