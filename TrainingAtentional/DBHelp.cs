using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TrainingAtentional
{
    public static class DBHelp
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["TrainingAtentionalConnectionString"].ConnectionString;

        public static DataTable GetTable(string tableName, string whereCondition)
        {
            try
            {
                DataTable result = new DataTable();

                whereCondition = string.IsNullOrEmpty(whereCondition) ? "1 = 1" : whereCondition;
                string sql = string.Format(" SELECT * from {0} WHERE {1}", tableName, whereCondition);

                SqlDataAdapter sda = new SqlDataAdapter(sql, ConnectionString);
                sda.Fill(result);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}