using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Models
{
    public class OddballStoreDB
    {
        public static SqlConnection GetConnection()
        {

            string connectionString = ;

            SqlConnection connection = new SqlConnection(@WebConfigurationManager.C);

            return connection;
        }
    }
}
