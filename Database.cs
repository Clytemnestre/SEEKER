using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeker
{
    class Database
    {
        const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=seeker_v1;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn;
        // open the connection to the database
        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }
    }
}
