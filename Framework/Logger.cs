using Framework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    class Logger
    {
        #region WriteError
        public static void WriteError(ErrorDto error)
        {
            //Errors will be written to the errorlog table in Database
            DBHandler.WriteErrorLog(error);
        }
        #endregion
        #region WriteToLog
        public static void WriteToLog(string source, string message)
        {
            /*Creating a log text file
           file name will have the source and date of the logs, 
           source of log was added to the name to stop causing an issue when 2 tasks are running at the same time*/
            string logFile = Path.Combine(ConfigurationManager.AppSettings["LogFile"] + source + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt");
            //By using an StreamWriter, a new line will be added to the log file, if the file doesn't exist it will be created
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(string.Format("{0} Source: {1} - {2}", DateTime.Now, source, message));
            }
        }
        #endregion
    }
}
