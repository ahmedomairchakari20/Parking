﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL

{
    public class dbhelper
    {
        public static SqlConnection Getconnection()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-BK40RI3;Initial Catalog=Parking;Integrated Security=True");

            return sqlConnection;
        }
    }
}
