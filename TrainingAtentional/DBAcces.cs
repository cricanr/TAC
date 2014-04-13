using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace TrainingAtentional
{
    public static class DBAcces
    {
        public static int ExecuteSP(string spName, object[] paramValues)
        {
            SqlConnection sqlConnection = null ;

            try
            {
                using (sqlConnection = new SqlConnection(DBHelp.ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    DiscoverParameters(sqlCommand);
                    AssignParameterValues(sqlCommand, paramValues);

                    if (sqlCommand.Connection.State == ConnectionState.Closed)
                        sqlCommand.Connection.Open();

                    int rows = (int)sqlCommand.ExecuteNonQuery();

                    if (sqlCommand.Connection.State == ConnectionState.Open)
                        sqlCommand.Connection.Close();

                    return rows;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                    
                }
            }
            
        }

        public static object ExecuteScalar(string spName, object[] paramValues)
        {
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(DBHelp.ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    DiscoverParameters(sqlCommand);
                    AssignParameterValues(sqlCommand, paramValues);

                    if (sqlCommand.Connection.State == ConnectionState.Closed)
                        sqlCommand.Connection.Open();

                    object result = sqlCommand.ExecuteScalar();

                    if (sqlCommand.Connection.State == ConnectionState.Open)
                        sqlCommand.Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();

                }
            }

        }

        private static void DiscoverParameters(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection.State == ConnectionState.Closed)
                sqlCommand.Connection.Open();

            SqlCommandBuilder.DeriveParameters(sqlCommand);

            if (sqlCommand.Connection.State == ConnectionState.Open)
                sqlCommand.Connection.Close();

            //if (!includeReturnValueParameter)
            sqlCommand.Parameters.RemoveAt(0);
        }

        private static void AssignParameterValues(SqlCommand sqlCommand, object[] paramValues)
        {
            for (int i = 0; i < sqlCommand.Parameters.Count; i++)
            {
                sqlCommand.Parameters[i].Value = paramValues[i];
            }
        }


        #region Obsolete

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection cn, string spName, bool includeReturnValueParameter)
        {
            using (SqlCommand cmd = new SqlCommand(spName, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //Adaugat --- de revazut
                bool bClose = false;
                if (cn.State == ConnectionState.Closed)
                {
                    bClose = true;
                    cn.Open();
                }
                SqlCommandBuilder.DeriveParameters(cmd);
                if (bClose)
                {
                    cn.Close();
                }

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count]; ;

                cmd.Parameters.CopyTo(discoveredParameters, 0);

                return discoveredParameters;
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null))
            {
                //do nothing if we get no parameters
                return;
            }
            if (parameterValues == null)
            {
                parameterValues = new object[0];
            }

            SqlParameter[] new_comandParameters = new SqlParameter[commandParameters.Length];

            int noParamUsed = -1;
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                new_comandParameters[i] = commandParameters[i];
                object paramValue = null; // GlobalParameters.GetParameterValue(new_comandParameters[i].ParameterName);

                if (paramValue != null)
                {
                    new_comandParameters[i].Value = paramValue;
                }
                else
                {
                    noParamUsed++;
                    //					if (noParamUsed >= parameterValues.Length)
                    //					{
                    //						throw new ArgumentException("Parameter count does not match Parameter Value count.");
                    //					}

                    if (parameterValues.Length <= noParamUsed)
                    {
                        new_comandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        new_comandParameters[i].Value = parameterValues[noParamUsed];
                    }
                }
            }
            commandParameters = new_comandParameters;

            //			if (noParamUsed + 1 != parameterValues.Length)
            //				throw new ArgumentException("Parameter count does not match Parameter Value count.");
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                    command.Parameters.Add(p);
                }

                
            }
        }

        #endregion
    }
}