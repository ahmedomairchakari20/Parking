    namespace parking_front.Data
    {
        public class Booking
        {
            public int BookingID { get; set; }
            public int UserID { get; set; }
            public int ParkingAreaID { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public bool PaymentStatus { get; set; }
        }
    }
