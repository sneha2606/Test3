using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Class to write error logs to notepad file. 
    /// </summary>
    class WriteErrorLogsToFile
    {
        string errorsLogFormat;

        public WriteErrorLogsToFile()
        {
            try
            {
                //Get the error log file location from app config file
                Program.errorlogs_PathName = ConfigurationManager.AppSettings["errorlog-filelocation"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                Environment.Exit(0);
                return;
            }
        }

        /// <summary>
        /// Function to write error log to error log file
        /// </summary>
        /// <param name="sErrMsg"></param>
        public void ErrorLog(string sErrMsg)
        {
            //Date time Format in which error logs will be written in text file - eg: 24/4/2017 10:38:49 AM 
            errorsLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            try
            {
                //File path name along with file name
                StreamWriter sw = new StreamWriter(Program.errorlogs_PathName + Program.errorlogs_FileName + ".txt", true);
                //Write to error logs file in format - eg: error log format ==> Error Message
                sw.WriteLine(errorsLogFormat + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            catch (IOException ioex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.errorlogslocation_incorrect + ioex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return;
            }
        }

    }
}
