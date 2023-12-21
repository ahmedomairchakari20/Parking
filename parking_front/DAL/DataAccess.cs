using parking_front.Data;
using System;
using System.Data;
using System.Data.SqlClient;
namespace parking_front.DAL
{
   public class DataAccess
    {
        private string connectionString; // Your database connection string
        public DataAccess()
        {
            connectionString = "Data Source=HUZAIFA\\SQLEXPRESS;Initial Catalog=Parking;Integrated Security=True;";
        }

        // Execute a non-query stored procedure
        private void ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add Parking Area using stored procedure
        public void AddParkingArea(ParkingArea parkingArea)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@Name", parkingArea.Name),
            new SqlParameter("@Location", parkingArea.Location),
            new SqlParameter("@TotalSpaces", parkingArea.TotalSpaces),
            new SqlParameter("@AvailableSpaces", parkingArea.AvailableSpaces)
        };

            ExecuteStoredProcedure("AddParkingArea", parameters);
        }

        // Add User using stored procedure
        public void AddUser(User user)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@FirstName", user.FirstName),
            new SqlParameter("@LastName", user.LastName),
            new SqlParameter("@Email", user.Email),
            new SqlParameter("@Phone", user.Phone)
        };

            ExecuteStoredProcedure("AddUser", parameters);
        }

        // Make Booking using stored procedure
        public void MakeBooking(Booking booking)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@UserID", booking.UserID),
            new SqlParameter("@ParkingAreaID", booking.ParkingAreaID),
            new SqlParameter("@StartTime", booking.StartTime),
            new SqlParameter("@EndTime", booking.EndTime),
            new SqlParameter("@PaymentStatus", booking.PaymentStatus)
        };

            ExecuteStoredProcedure("MakeBooking", parameters);
        }

        // Update Booking using stored procedure
        public void UpdateBooking(int bookingID, bool paymentStatus)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@BookingID", bookingID),
            new SqlParameter("@PaymentStatus", paymentStatus)
        };

            ExecuteStoredProcedure("UpdateBooking", parameters);
        }

        // Cancel Booking using stored procedure
        public void CancelBooking(int bookingID)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@BookingID", bookingID)
        };

            ExecuteStoredProcedure("CancelBooking", parameters);
        }

        // Add Parking Space Status using stored procedure
        public void AddParkingSpaceStatus(ParkingSpaceStatus parkingSpaceStatus)
        {
            SqlParameter[] parameters =
            {
            new SqlParameter("@ParkingAreaID", parkingSpaceStatus.ParkingAreaID),
            new SqlParameter("@StartTime", parkingSpaceStatus.StartTime),
            new SqlParameter("@EndTime", parkingSpaceStatus.EndTime),
            new SqlParameter("@BookingID", parkingSpaceStatus.BookingID)
        };

            ExecuteStoredProcedure("AddParkingSpaceStatus", parameters);
    
        
        }


        public List<ParkingArea> GetParkingAreas()
        {
            List<ParkingArea> parkingAreas = new List<ParkingArea>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetParkingArea", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ParkingArea area = new ParkingArea
                            {
                                ParkingAreaID = Convert.ToInt32(reader["ParkingAreaID"]),
                                Name = reader["Name"].ToString(),
                                Location = reader["Location"].ToString(),
                                TotalSpaces = Convert.ToInt32(reader["TotalSpaces"]),
                                AvailableSpaces = Convert.ToInt32(reader["AvailableSpaces"])
                            };
                            parkingAreas.Add(area);
                        }
                    }
                }
            }

            return parkingAreas;
        }
        public List<Booking> GetBookedParkingSlots(int parkingAreaID)
        {
            List<Booking> bookedSlots = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBookedParkingSlots", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ParkingAreaID", parkingAreaID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                StartTime = reader["StartTime"].ToString(),
                                EndTime = reader["EndTime"].ToString()
                            };
                            bookedSlots.Add(booking);
                        }
                    }
                }
            }

            return bookedSlots;
        }

        public bool RegisterUser(string email, string password, string firstName, string lastName, string phone)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("RegisterUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Phone", phone);

            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public (bool IsValid, int UserID) ValidateUser(string email, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("ValidateUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                bool isValid = Convert.ToBoolean(reader["IsValid"]);
                int userId = isValid ? Convert.ToInt32(reader["UserID"]) : 0;
                return (isValid, userId);
            }

            return (false, 0);
        }

    }
    }
