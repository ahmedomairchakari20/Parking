using System.Data.SqlClient;

namespace parking_front.DAL
{
    public class DbHelper
    {
        public static SqlConnection Getconnection()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=HUZAIFA\\SQLEXPRESS;Initial Catalog=Parking;Integrated Security=True;");

            return sqlConnection;
        }
    }
}
