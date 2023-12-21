namespace parking_front.Data
{
    public class ParkingSpaceStatus
    {
        public int ParkingAreaID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int BookingID { get; set; }
    }
}
