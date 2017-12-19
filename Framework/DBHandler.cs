using Framework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class DBHandler
    {
        #region GetAllContent
        public static List<Content> getcontents()
        {
            List<Content> contents = new List<Content>();
            try
            {
                contents.Add(new Content
                {
                    id = 1,
                    text = "Hello World!"
                });
                //To use database we can use the following code

                //Method for returning a list of all contents in the content table in the database and put it in the list

                /*   using (var sqlConnection =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["DataDB"].ConnectionString))
                   {
                       using (var command = new SqlCommand(null, sqlConnection))
                       {
                           sqlConnection.Open();
                           command.CommandType = CommandType.StoredProcedure;
                           //running the GetClientInformation stored procedure in DB
                           command.CommandText = "GetAllContents";

                           SqlDataReader reader = command.ExecuteReader();

                           while (reader.Read())
                           {
                               contents.Add(new Content()
                               {
                                   id = Convert.ToInt32(reader["id"].ToString()),
                                   text = reader["Text"].ToString(),

                               });
                           }
                       }
                   }
                   return contents;*/



                return contents;
            }
            catch (Exception ex)
            {
                //If an exception happens it will insert the error to errorlog table and to the log file
                Logger.WriteError(new ErrorDto()
                {
                    Source = "GetContents",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
                Console.WriteLine(string.Format("Error - {0} - {1} ", "GetClientInformation", ex.Message));

                return contents;
            }
        }
        #endregion
        #region WriteErrorLog
        //Writing an error to a errorlog table
        public static void WriteErrorLog(ErrorDto error)
        {
            //Writing the error to the errorlog file under log folder
            Logger.WriteToLog("WriteError", string.Format("Source: {0} Error: {1} StackTrace: {2}", error.Source, error.Message, error.StackTrace));
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataDB"].ConnectionString))
                {
                    using (var command = new SqlCommand(null, sqlConnection))
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Source", error.Source);
                        command.Parameters.AddWithValue("@Message", error.Message);
                        command.Parameters.AddWithValue("@StackTrace", error.StackTrace);
                        command.CommandText = "CreateErrorLog";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                //If an exception happens it will insert the error to errorlog table and to the log file
                Logger.WriteError(new ErrorDto()
                {
                    Source = "WriteErrorLog",
                    Message = e.Message,
                    StackTrace = e.StackTrace
                });
                Console.WriteLine(string.Format("Error - {0} - {1}", "WriteErrorLog", e.Message));
            }
        }
        #endregion
    }
}
