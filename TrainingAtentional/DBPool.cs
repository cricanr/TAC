using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TrainingAtentional
{
    public static class DBPool
    {
        public static bool InsertUser(string userName, string password, string lastName, string firstName, int genderID, int age, int dominandHandID, int perifericInterfaceID,string screenResolution, out int userID )
        {
            object[] paramValues = new object[] {userName,password,lastName,firstName,genderID,age,dominandHandID,perifericInterfaceID,screenResolution};
            object result = DBAcces.ExecuteScalar("sp_Users_Insert", paramValues);

            if (result != null)
            {
                userID = Convert.ToInt32(result);
                return true;
            }
            else
            {
                userID = 0;
                return false;  
            }
                
        }

        public static bool UpdateUserStage(int userID, int stage)
        {
            object[] paramValues = new object[] { userID, stage};
            int result = DBAcces.ExecuteSP("ssp_User_UpdateStage", paramValues);

            if (result == 0)
            {

            }
            return (result > 0);
        }

        public static bool InsertDotTrial(int userID, string SessionID, DateTime DataStart, DateTime DataStop, string PhotoName,int photoCode,int genderID, int emotionID,   int PhotoOrientationID, bool isCongruent ,int targetTypeID, bool IsTest, string KeyPressed,  DateTime DataStopWrongKeyPressed,string WrongKeyPressed,int stage)
        {
            object dataWrong = (DataStopWrongKeyPressed == new DateTime())?  DBNull.Value: (object)DataStopWrongKeyPressed;
            object[] paramValues = new object[] { userID, SessionID, DataStart, DataStop, PhotoName, photoCode, genderID, emotionID, PhotoOrientationID, isCongruent, targetTypeID, IsTest, KeyPressed, dataWrong, WrongKeyPressed ,stage};
            int result = DBAcces.ExecuteSP("sp_DotTrials_Insert", paramValues);

            if (result == 0)
            {

            }
            return (result > 0);
        }

        public static bool IsUniqueUser(string userName)
        {
            object result = DBAcces.ExecuteScalar("sp_Users_IsUnique", new object[] { userName });
            if (result != null)
            {
                return Convert.ToBoolean(result) ;
            }
            else
            {
                return false;
            }
        }

        public static bool CorrectLogin(string userName, string password, out int userID, out int stage, out string doTraining)
        {
            string whereCondition = string.Format(" userName = '{0}' and password = '{1}' ", userName, password);
            DataTable dt = DBHelp.GetTable("Users",whereCondition);

            if (dt != null && dt.Rows.Count == 1)
            {
                userID = (int)dt.Rows[0]["ID"];
                //userFullName = dt.Rows[0]["fullName"].ToString();
                object temp = dt.Rows[0]["Stage"];
                object temp1 = dt.Rows[0]["ScreenResolution"];
                
                stage = 0;
                if (temp != null && temp.ToString() != "" ) { stage = (int)temp; }
                if (temp1 != null && temp1.ToString() != "")
                { doTraining = temp1.ToString(); }
                else doTraining = "1";
                    

                return true;
            }
            else
            {
                userID = 0;
                stage = 0;
                //userFullName = "";
                doTraining = "1";
                return false;
            }
        }

       

        public static int InsertTrainingTrialNew(int userID, string SessionID, DateTime DataStart, DateTime DataStop, string PhotoName, int PhotoCode, int GenderID, int EmotionID, int photoPosition, int Block, bool IsTest, int WrongPhotoPosition, string wrongPhotoName, DateTime DataStopWrongPhoto)
        {
            int result = 0;

            try
            {
                object dataWrong = (DataStopWrongPhoto == new DateTime()) ? DBNull.Value : (object)DataStopWrongPhoto;
                object[] paramValues = new object[] { userID, SessionID, DataStart, DataStop, PhotoName, PhotoCode, GenderID, EmotionID, photoPosition, Block, IsTest, WrongPhotoPosition, wrongPhotoName, dataWrong };
                result = DBAcces.ExecuteSP("sp_TrainingTrails_Insert", paramValues);

                if (result == 0)
                {
                    throw new ApplicationException("Eroare la incarcare Training Trail");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (result);
        }

        public static int InsertTrainingBreakNew(int UserID, string SessionID, DateTime DataStart, DateTime DataStop)
        {
            int result = 0;

            try
            {
                object[] paramValues = new object[] { UserID, SessionID, DataStart, DataStop};
                result = DBAcces.ExecuteSP("sp_TrainingBreak_Insert", paramValues);

                if (result == 0)
                {
                    throw new ApplicationException("Eroare la incarcare Break Trail");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (result);
        }






        [Obsolete]
        public static int InsertTrainingTrail(int userID, string SessionID, DateTime DataStart, DateTime DataStop, string PhotoName, int PhotoCode, int GenderID, int EmotionID, int photoPosition, int Block,bool IsTest, int WrongPhotoPosition, string wrongPhotoName, DateTime DataStopWrongPhoto)
        {
            try
            {

                SqlConnection sqlConnection = new SqlConnection(DBHelp.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("sp_TrainingTrails_Insert", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                #region

                SqlParameter p = new SqlParameter("userID", userID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("SessionID", SessionID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("DataStart", DataStart);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("DataStop", DataStop);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("PhotoName", PhotoName);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("PhotoCode", PhotoCode);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("GenderID", GenderID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("EmotionID", EmotionID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("photoPosition", photoPosition);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("Block", Block);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("isTest",IsTest );
                sqlCommand.Parameters.Add(p);

                p = new SqlParameter("WrongPhotoPosition", WrongPhotoPosition);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("wrongPhotoName", wrongPhotoName);
                sqlCommand.Parameters.Add(p);

                if (DataStopWrongPhoto == new DateTime())
                {
                    p = new SqlParameter("DataStopWrongPhoto", DBNull.Value);
                }
                else
                {
                    p = new SqlParameter("DataStopWrongPhoto", DataStopWrongPhoto);
                }
                sqlCommand.Parameters.Add(p);


                #endregion
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                int rows = sqlCommand.ExecuteNonQuery();

                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }

                return rows;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [Obsolete]
        public static int InsertTrainingBreak(int UserID, string SessionID, DateTime DataStart, DateTime DataStop )
        {
            try
            {

                SqlConnection sqlConnection = new SqlConnection(DBHelp.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("sp_TrainingBreak_Insert", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                #region
                SqlParameter p = new SqlParameter("UserID", UserID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("SessionID", SessionID);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("DataStart", DataStart);
                sqlCommand.Parameters.Add(p);
                p = new SqlParameter("DataStop", DataStop);
                sqlCommand.Parameters.Add(p);
               

                #endregion
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                int rows = sqlCommand.ExecuteNonQuery();

                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }

                return rows;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [Obsolete]
        public static int InsertDistortion(int userID, string SessionID, DateTime DataStart, DateTime DataStop, string PhotoName, int PhotoOrientationID, int TargetOrientationID, bool IsTest, string KeyPressed, string WrongKeyPressed, DateTime DataStopWrongKeyPressed)
        {

            SqlConnection sqlConnection = new SqlConnection(DBHelp.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_Distortions_Insert", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            #region

            SqlParameter p = new SqlParameter("userID", userID);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("SessionID", SessionID);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("DataStart", DataStart);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("DataStop", DataStop);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("PhotoName", PhotoName);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("PhotoOrientationID", PhotoOrientationID);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("TargetOrientationID", TargetOrientationID);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("IsTest", IsTest);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("KeyPressed", KeyPressed);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("WrongKeyPressed", WrongKeyPressed);
            sqlCommand.Parameters.Add(p);
            p = new SqlParameter("DataStopWrongKeyPressed", DataStopWrongKeyPressed);
            sqlCommand.Parameters.Add(p);



            #endregion
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            int rows = sqlCommand.ExecuteNonQuery();

            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }

            return rows;

        }
    }
}

