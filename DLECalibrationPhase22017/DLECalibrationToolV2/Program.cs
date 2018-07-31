using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DLECalibrationToolV2
{
    static class Program
    {
        internal static string Username;                            // Variable to store logged in user's name
        internal static string UserType;                            // Variable to store logged in user's type whether Admin or Normal User
        internal static string comments = "";                       // Variable to store comments entered by the user 
        
        //Error Logs file name
        internal static string errorlogs_PathName = "";
        internal static string errorlogs_FileName = "ErrorLogs-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + "hrs-" + DateTime.Now.Minute.ToString() + "mins-" + DateTime.Now.Second.ToString() + "secs";
        internal static WriteErrorLogsToFile Err = new WriteErrorLogsToFile();
        
        internal static int exit_app_flag = 0;                     //Flag to check if session has been auto exited or not. Set to 1 when session has been auto exit. 
        
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //Get Guid attribute
                Assembly assembly = Assembly.GetExecutingAssembly();
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                var appGuid = attribute.Value;

                // To have only one instance of the application running across all sessions on the machine, the named mutex is put into the global namespace with the prefix “Global\
                using (Mutex mutex = new Mutex(false, @"Global\" + appGuid))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        MessageBox.Show("Instance already running");
                        return;
                    }
                    GC.Collect();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new LoginForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class Name : " + "Program.cs"+ ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }
    }
}
