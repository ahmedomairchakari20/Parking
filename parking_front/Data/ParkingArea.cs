namespace parking_front.Data
{
    public class ParkingArea
    {
        public int ParkingAreaID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int TotalSpaces { get; set; }
        public int AvailableSpaces { get; set; }
    }
}
