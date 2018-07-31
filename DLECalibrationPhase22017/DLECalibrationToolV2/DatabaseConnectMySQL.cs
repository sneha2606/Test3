using DLECalibrationToolV2.PI_AFSDK;
using MySql.Data.MySqlClient;
using OSIsoft.AF.PI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Class contains methods to connect to database and perform CRUD operations
    /// </summary>
    public class DatabaseConnectMySQL
    { 
        public static string Conn;                                                                                                                 //Variable to store connection string for Mysql Database
        public string LogsFilePath_FromDB = String.Empty;                                                                                          //Variable to store logs file path
        public string PrevSession_ID = "";                                                                                                         //Variable to get the last session id from database  
        public string CurrentSession_ID = "";                                                                                                      //Variable to hold the new session id for current session
        public int ErrCount = 0;                                                                                                                   //Temporary variable to keep track of out of tol values for report creation                                                         
        public List<string> EmailID = new List<string>();                                                                                          //List to store email ids from DB to whom report has to be sent                                                                                      
        public string SmtpServer = "";                                                                                                             //Variable to store SMTP Server name 
        public string SmtpEmail = "";                                                                                                              //Variable to store SMTP Port number
        public string SmtpEmailPwd = "";                                                                                                              //Variable to store SMTP Port number
        public string SmtpPort = "";                                                                                                               //Variable to store SMTP email address
        public string EnableSSL = "";                                                                                                              //Variable to store if SSL is enabled for SMTP
        public int Panel_ID = 0;                                                                                                                   //Variable to store panel id
        public List<int> DLEScneario_ID = new List<int>();                                                                                         //List to store DLE Scenario ID
        public string PanelRun;                                                                                                                    //List to store number of panels which have been run - (includes panels for which data has been saved and not saved)
        public string PanelSavedData;                                                                                                              //List to store number of panels for which data has been saved
        public string Start_Time = "";                                                                                                             //Variable to store session start time
        public string End_Time = "";                                                                                                               //Variable to store session end time
        public string Rep_Title = "";                                                                                                              //Variable to store Tolerance report title
        public List<string> Tag = new List<string>();                                                                                              //List to store Out of Tolerance Tags
        public List<string> ToleranceStatus = new List<string>();                                                                                  //List to store Tolerance status
        public List<string> ActualValue = new List<string>();                                                                                      //List to store Actual values
        public List<string> UpperLimit = new List<string>();                                                                                       //List to store Upper Limit values
        public List<string> LowerLimit = new List<string>();                                                                                       //List to store Lower limit values
        public List<string> Lux = new List<string>();                                                                                              //List to store Lux values                                                                                                                                                       
        public List<string> Cct = new List<string>();                                                                                              //List to store CCT values
        public string LuxTag = "";                                                                                                                 //Temporary variable to store Lux tag
        public string CctTag = "";                                                                                                                 //Temporary variable to store CCT tag
        public List<string> Increasing = new List<string>();                                                                                       //List to add string messages for progressively increasing values
        public List<string> Decreasing = new List<string>();                                                                                       //List to add string messages for progressively decreasing values
        public int Rows = 0;                                                                                                                       //Temporary variable to check if there are more than 6 progressively increasing or decreasing values
        public int UserType_ID = 0;                                                                                                                //Variable to store user type id
        public List<int> Panelids = new List<int>();                                                                                               //Array to store panelids for Report creation

        public string Username = "";                                                                                                               //Variable to store logged in users name 
        public PIServer PiServer;                                                                                                                  //Variable to store PIServer name
        public string Com_Port = "";                                                                                                               //Variable to store Serial Port - COM Port number
        public int Baud_Rate;                                                                                                                      //Variable to store Konica Minolta device Baud Rate
        public string Parity;                                                                                                                      //Variable to store Konica Minolta device Parity
        public int Stop_Bits;                                                                                                                      //Variable to store Konica Minolta device Stop Bits
        public int Data_Bits;                                                                                                                      //Variable to store Konica Minolta device Data Bits
        public string ip_address = "";                                                                                                             //Variable to store Blizzard device IP Address
        public int udp_port;                                                                                                                       //Variable to store Blizzard device UDP Port
        public string datetime;                                                                                                                    //Variable to store current datetime
        public string pi_Server;                                                                                                                   //Variable to store PI Server name
        public string af_Server;                                                                                                                   //Variable to store AF Server name
        public string af_DB;                                                                                                                       //Variable to store AF Database name
        public int progtrend_count = 0;                                                                                                            //Variable to store number of values to be used in progressive trend calculation
        
        /// <summary>
        /// Method to get connection string for mysql from app config file
        /// </summary>
        public static void DbConnection()
        {
            try
            //Connection string 
            {
                Conn = ConfigurationManager.AppSettings["mysql-connectionstring"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }  


        }


        /// <summary>
        /// Method to validate user login details - Used in Login Form and User profile form
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string ValidateLogin(string user, string pass)
        {
            string login_status = "";
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select * from login_users where username=@user and password=sha2(@pass,256) and is_active='Yes'";
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);
                        using (MySqlDataReader login = cmd.ExecuteReader())
                        {
                            if (!login.HasRows)
                                login_status = "Username or Password entered is incorrect.";
                            else
                            {
                                while (login.Read())
                                    UserType_ID = Convert.ToInt32(login[2]);
                                login_status = "";
                            }
                        }
                    }
                    return login_status;
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }   
            }
        }

        /// <summary>
        /// Method to get logged in user role name based on user id - Used in Login form
        /// </summary>
        public void GetLoggedInUserRole()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select role_name from user_role where role_id=@id";
                        cmd.Parameters.AddWithValue("@id", UserType_ID);
                        using (MySqlDataReader userrole = cmd.ExecuteReader())
                        {
                            while (userrole.Read())
                                Program.UserType = userrole[0].ToString();
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///  Method to get the PI Server Name- Used in Calibration form, DLEDataMovingAvgFromPI class and PIToleranceTags class
        /// </summary>
        public void GetPIServerName()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT pi_server FROM conf_pi_settings where id=1";
                        cmd.Connection = connection;
                        using (MySqlDataReader pisettings = cmd.ExecuteReader())
                        {
                            while (pisettings.Read())
                                PiServer = new PIConnector(pisettings[0].ToString()).Connect();
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }        
            }
        }


        /// <summary>
        /// Method to get logs file location -Used in Calibration form,History form and LogsFileLocation form
        /// </summary>
        public void GetLogFileLocation()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT logfile_location FROM dle_calibration.conf_logfile_settings where id=1";
                        cmd.Connection = connection;
                        using (MySqlDataReader logfileloc = cmd.ExecuteReader())
                        {
                            while (logfileloc.Read())
                                LogsFilePath_FromDB = logfileloc[0].ToString();
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        /// <summary>
        /// Method to get the serial port settings - Used in Serial port settings form and Calibraton form
        /// </summary>
        public void GetSerialPortSettings()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT * FROM dle_calibration.conf_serialport_settings where id=1";
                        cmd.Connection = connection;
                        using (MySqlDataReader serialportsettings = cmd.ExecuteReader())
                        {
                            while (serialportsettings.Read())
                            {
                                Com_Port = serialportsettings[1].ToString();
                                Baud_Rate = Convert.ToInt32(serialportsettings[2].ToString());
                                Parity = serialportsettings[3].ToString();
                                Stop_Bits = Convert.ToInt32(serialportsettings[4].ToString());
                                Data_Bits = Convert.ToInt32(serialportsettings[5].ToString());
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        

        /// <summary>
        /// Method to get the artnet settings - Used by ARTNet Settings form, DLE Control form
        /// </summary>
        public void GetARTNetSettings()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT artnet_ip,artnet_port FROM dle_calibration.conf_artnet_settings where id=1";
                        cmd.Connection = connection;
                        using (MySqlDataReader artnetsettings = cmd.ExecuteReader())
                        {
                            while (artnetsettings.Read())
                            {
                                ip_address = artnetsettings[0].ToString();
                                udp_port = Convert.ToInt32(artnetsettings[1].ToString());
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        
        /// <summary>
        /// Method to check if email has been sent for previous session - Used by Calibration form
        /// </summary>
        /// <returns></returns>
        public string CheckPrevSessionEmailSent()
        {
            string PanelDataSaved_PrevSession = "";
            string EmailSent_PrevSession = "";
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select panels_data_saved,email_sent from session_details WHERE session_id=@session_id";
                        cmd.Parameters.AddWithValue("@session_id", PrevSession_ID);
                        using (MySqlDataReader session = cmd.ExecuteReader())
                        {
                            while (session.Read())
                            {
                                PanelDataSaved_PrevSession = session[0].ToString();
                                EmailSent_PrevSession = session[1].ToString();
                            }
                            if ((PanelDataSaved_PrevSession != "") && (PanelDataSaved_PrevSession != "0") && ((EmailSent_PrevSession == "No") || (EmailSent_PrevSession == "")))
                                return "No";
                            else
                                return "Yes";
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }        
            }
        }


        
        /// <summary>
        /// Method to create new session id - Used in Calibration form
        /// </summary>
        public void CreateNewSessionID()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select session_id from session_details ORDER BY session_id DESC LIMIT 1";
                        using (MySqlDataReader session = cmd.ExecuteReader())
                        {
                            while (session.Read())
                                PrevSession_ID = session[0].ToString();
                            if(!session.HasRows)
                            {
                                CurrentSession_ID = "00001"; // need to chk this
                                return;
                            }
                            int newsession_id = 0;
                            newsession_id = Convert.ToInt32(PrevSession_ID.Substring(PrevSession_ID.Length - 5)) + 1;
                            string newid = "";
                            if (newsession_id.ToString().Length == 1)
                                newid = "0000" + newsession_id;
                            else if (newsession_id.ToString().Length == 2)
                                newid = "000" + newsession_id;
                            else if (newsession_id.ToString().Length == 3)
                                newid = "00" + newsession_id;
                            else if (newsession_id.ToString().Length == 4)
                                newid = "0" + newsession_id;
                            else
                                newid = newsession_id.ToString();
                           // CurrentSession_ID = PrevSession_ID.Substring(0, 8) + newid;
                            CurrentSession_ID =  newid;
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        /// <summary>
        /// Method to update session details - start time , report title, system name and ip address - Used in Calibration form
        /// </summary>
        /// <param name="start_time"></param>
        /// <param name="report_title"></param>
        /// <param name="sys_name"></param>
        /// <param name="sys_ip"></param>
        public void UpdateSessionDetailsTable(string start_time, string report_title,string sys_name,string sys_ip)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO session_details(session_id,started_by,start_time,report_title,system_name,system_ipaddress) VALUES (@session_id,@started_by,STR_TO_DATE(@start_time,'%e/%c/%Y %r'),@report_title,@sysname,@sysip) ";
                        cmd.Parameters.AddWithValue("@session_id", CurrentSession_ID);
                        cmd.Parameters.AddWithValue("@started_by", Program.Username);
                        cmd.Parameters.AddWithValue("@start_time", start_time);
                        cmd.Parameters.AddWithValue("@report_title", report_title);
                        cmd.Parameters.AddWithValue("@sysname", sys_name);
                        cmd.Parameters.AddWithValue("@sysip",sys_ip);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }


        /// <summary>
        /// Method to find the count of number of values which are out of tolerance  for specific session to be used in Report creation. Used in Calibration Form.
        /// </summary>
        /// <param name="sessionid"></param>
        public void OutOfTolValCount(string sessionid)
        {
            ErrCount = 0;
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT COUNT(*) FROM dle_calibration.report_details where session_id=@session_id";
                        cmd.Parameters.AddWithValue("@session_id", sessionid);//finalsession_id
                        using (MySqlDataReader tol_errcount = cmd.ExecuteReader())
                        {
                            while (tol_errcount.Read())
                                ErrCount = Convert.ToInt32(tol_errcount[0].ToString());
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        

        /// <summary>
        /// Method to update number of panels which have been run during a session - Used in Calibration form.
        /// </summary>
        /// <param name="panel_run"></param>
        /// <param name="panel_List"></param>
        public void UpdatePanelRunCountDetails(int panel_run, int[] panel_List)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE session_details SET panels_run=@panels_run,panel1left_run_count=@panel1leftcount,panel1right_run_count=@panel1rightcount,panel2left_run_count=@panel2leftcount,panel2right_run_count=@panel2rightcount,panel3left_run_count=@panel3leftcount,panel3right_run_count=@panel3rightcount,panel4left_run_count=@panel4leftcount,panel4right_run_count=@panel4rightcount,panel5left_run_count=@panel5leftcount,panel5right_run_count=@panel5rightcount,panel6left_run_count=@panel6leftcount,panel6right_run_count=@panel6rightcount,panel7left_run_count=@panel7leftcount,panel7right_run_count=@panel7rightcount,panel8left_run_count=@panel8leftcount,panel8right_run_count=@panel8rightcount where session_id =@session_id";
                        cmd.Parameters.AddWithValue("@panels_run", panel_run);
                        cmd.Parameters.AddWithValue("@session_id", CurrentSession_ID);
                        cmd.Parameters.AddWithValue("@panel1leftcount", panel_List[0]);
                        cmd.Parameters.AddWithValue("@panel1rightcount", panel_List[1]);
                        cmd.Parameters.AddWithValue("@panel2leftcount", panel_List[2]);
                        cmd.Parameters.AddWithValue("@panel2rightcount", panel_List[3]);
                        cmd.Parameters.AddWithValue("@panel3leftcount", panel_List[4]);
                        cmd.Parameters.AddWithValue("@panel3rightcount", panel_List[5]);
                        cmd.Parameters.AddWithValue("@panel4leftcount", panel_List[6]);
                        cmd.Parameters.AddWithValue("@panel4rightcount", panel_List[7]);
                        cmd.Parameters.AddWithValue("@panel5leftcount", panel_List[8]);
                        cmd.Parameters.AddWithValue("@panel5rightcount", panel_List[9]);
                        cmd.Parameters.AddWithValue("@panel6leftcount", panel_List[10]);
                        cmd.Parameters.AddWithValue("@panel6rightcount", panel_List[11]);
                        cmd.Parameters.AddWithValue("@panel7leftcount", panel_List[12]);
                        cmd.Parameters.AddWithValue("@panel7rightcount", panel_List[13]);
                        cmd.Parameters.AddWithValue("@panel8leftcount", panel_List[14]);
                        cmd.Parameters.AddWithValue("@panel8rightcount", panel_List[15]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        /// <summary>
        /// Method to update session details when user exits session - Session end time , individual panel count for which data has been saved. Used in Calibration form.
        /// </summary>
        /// <param name="end_time"></param>
        /// <param name="panel_List"></param>
        public void UpdateSessionDetailsOnExit(string end_time,  int[] panel_List)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE session_details SET end_time=STR_TO_DATE(@end_time,'%e/%c/%Y %r'),panel1left_run_count=@panel1leftcount,panel1right_run_count=@panel1rightcount,panel2left_run_count=@panel2leftcount,panel2right_run_count=@panel2rightcount,panel3left_run_count=@panel3leftcount,panel3right_run_count=@panel3rightcount,panel4left_run_count=@panel4leftcount,panel4right_run_count=@panel4rightcount,panel5left_run_count=@panel5leftcount,panel5right_run_count=@panel5rightcount,panel6left_run_count=@panel6leftcount,panel6right_run_count=@panel6rightcount,panel7left_run_count=@panel7leftcount,panel7right_run_count=@panel7rightcount,panel8left_run_count=@panel8leftcount,panel8right_run_count=@panel8rightcount where session_id =@session_id";
                        cmd.Parameters.AddWithValue("@end_time", end_time);
                        cmd.Parameters.AddWithValue("@session_id", CurrentSession_ID);
                        cmd.Parameters.AddWithValue("@panel1leftcount", panel_List[0]);
                        cmd.Parameters.AddWithValue("@panel1rightcount", panel_List[1]);
                        cmd.Parameters.AddWithValue("@panel2leftcount", panel_List[2]);
                        cmd.Parameters.AddWithValue("@panel2rightcount", panel_List[3]);
                        cmd.Parameters.AddWithValue("@panel3leftcount", panel_List[4]);
                        cmd.Parameters.AddWithValue("@panel3rightcount", panel_List[5]);
                        cmd.Parameters.AddWithValue("@panel4leftcount", panel_List[6]);
                        cmd.Parameters.AddWithValue("@panel4rightcount", panel_List[7]);
                        cmd.Parameters.AddWithValue("@panel5leftcount", panel_List[8]);
                        cmd.Parameters.AddWithValue("@panel5rightcount", panel_List[9]);
                        cmd.Parameters.AddWithValue("@panel6leftcount", panel_List[10]);
                        cmd.Parameters.AddWithValue("@panel6rightcount", panel_List[11]);
                        cmd.Parameters.AddWithValue("@panel7leftcount", panel_List[12]);
                        cmd.Parameters.AddWithValue("@panel7rightcount", panel_List[13]);
                        cmd.Parameters.AddWithValue("@panel8leftcount", panel_List[14]);
                        cmd.Parameters.AddWithValue("@panel8rightcount", panel_List[15]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        
        /// <summary>
        /// Method to get the email address of users to whom the DLE Tolerance report needs to be sent. Used in OutOfToleranceCheck class.
        /// </summary>
        /// <returns></returns>
        public List<string> GetEmailID()
        {
            EmailID.Clear();
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select email_id from  dle_calibration.conf_email_details";//change to dle_calibration.conf_email_details
                        using (MySqlDataReader emails = cmd.ExecuteReader())
                        {
                            while (emails.Read())
                            {
                                EmailID.Add(emails[0].ToString());
                            }
                        }
                    }
                    return EmailID;
                }

                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        /// <summary>
        /// Method to get the SMTP Settings in Calibration form and OutOfToleranceCheck class.
        /// </summary>
        public void GetSMTPSettings()
        {
            string smtppwd = "";
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "Select smtp_server,smtp_port,smtp_send_email,smtp_sendemail_password,enable_ssl from dle_calibration.conf_smtp_settings ORDER BY id DESC LIMIT 1;";
                        cmd.Connection = connection;
                        using (MySqlDataReader smtpsettings = cmd.ExecuteReader())
                        {
                            while (smtpsettings.Read())
                            {
                                SmtpServer = smtpsettings[0].ToString();
                                SmtpEmail = smtpsettings[2].ToString();
                                SmtpPort = smtpsettings[1].ToString();
                                smtppwd = smtpsettings[3].ToString();
                                EnableSSL = smtpsettings[4].ToString();
                                EnableSSL = EnableSSL.Replace(" ", "");
                            }
                        }
                    }

                    string decrypt_smtppwd = string.Empty;
                    UTF8Encoding encodestring = new UTF8Encoding();
                    Decoder Decode = encodestring.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(smtppwd);
                    int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    decrypt_smtppwd = new String(decoded_char);
                    SmtpEmailPwd = decrypt_smtppwd;
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }


        /// <summary>
        /// Method to update email sent status in OutOfToleranceCheck Class
        /// </summary>
        /// <param name="email_Sent"></param>
        /// <param name="session_id"></param>
        public void UpdateEmailSent(string email_Sent, string session_id)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE session_details SET email_sent=@email_sent where session_id =@session_id";
                        cmd.Parameters.AddWithValue("@session_id", session_id);
                        cmd.Parameters.AddWithValue("@email_sent", email_Sent);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        
        /// <summary>
        /// Method to get panel id based on panel name in Calibration form.
        /// </summary>
        /// <param name="panel"></param>
        public void GetPanelID(string panel)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT id from panel where panel_name=@panelname";
                        cmd.Parameters.AddWithValue("@panelname", panel);
                        using (MySqlDataReader panelno = cmd.ExecuteReader())
                        {
                            while (panelno.Read())
                            {
                                Panel_ID = Convert.ToInt32(panelno[0]);
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }  
            }
        }


        /// <summary>
        /// Method to get dle scenario ids in Calibration form.
        /// </summary>
        public void GetDLEScenariosID()
        {
            DLEScneario_ID.Clear();
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT id from dle_scenarios";
                        using (MySqlDataReader dlescenarios = cmd.ExecuteReader())
                        {
                            while (dlescenarios.Read())
                            {
                                DLEScneario_ID.Add(Convert.ToInt32(dlescenarios[0]));
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        

        /// <summary>
        /// Method to Insert DLE Values - Lux and cct values in Calibration form.
        /// </summary>
        /// <param name="sensor_LUX"></param>
        /// <param name="sensor_CCT"></param>
        /// <param name="pitags_sensor"></param>
        /// <param name="time"></param>
        /// <param name="scenario_id"></param>
        public void InsertDLEValues(List<string> sensor_LUX, List<string> sensor_CCT, List<string> pitags_sensor, List<string> time, int scenario_id)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        for (int i = 0, j = scenario_id, k = 0; i < 7; i++, j++, k = k + 2)
                        {
                            cmd.CommandText = "INSERT INTO dle_values(session_id,panel_id,dle_scenario_id,lux_value,cct_value,lux_tag,cct_tag,datetime)VALUES(@session_id,@panel_id,@dle_scenario_id,ROUND(@lux_value,2),ROUND(@cct_value,2),@lux_tag,@cct_tag,STR_TO_DATE(@datetime,'%e/%c/%Y %r'))";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@session_id", CurrentSession_ID);//finalsession_id"SBB-DLE-001"
                            cmd.Parameters.AddWithValue("@panel_id", Panel_ID);
                            cmd.Parameters.AddWithValue("@dle_scenario_id", DLEScneario_ID[j]);
                            cmd.Parameters.AddWithValue("@lux_value", sensor_LUX[i]);
                            cmd.Parameters.AddWithValue("@cct_value", sensor_CCT[i]);
                            cmd.Parameters.AddWithValue("@lux_tag", pitags_sensor[k]);
                            cmd.Parameters.AddWithValue("@cct_tag", pitags_sensor[k + 1]);
                            cmd.Parameters.AddWithValue("@datetime", time[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }


       /// <summary>
       /// Method to insert out of tol values in database
       /// </summary>
       /// <param name="tag"></param>
       /// <param name="curr_val"></param>
       /// <param name="tol_status"></param>
       /// <param name="upp_lim"></param>
       /// <param name="low_lim"></param>
        public void InsertOutOfTolVal(string tag, string curr_val, string tol_status, string upp_lim, string low_lim)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO dle_calibration.report_details(session_id,tag,current_value,tolerance_status,upper_limit,lower_limit,datetime) VALUES (@sessionid,@tag,ROUND(@currentval,2),@tolstatus,@upperlim,@lowerlim,@datetime)";
                        cmd.Parameters.AddWithValue("@sessionid", CurrentSession_ID);
                        cmd.Parameters.AddWithValue("@tag", tag); //NEED TO CHK IF THIS IS CORRECT
                        cmd.Parameters.AddWithValue("@currentval", curr_val);
                        cmd.Parameters.AddWithValue("@tolstatus", tol_status);
                        cmd.Parameters.AddWithValue("@upperlim", upp_lim);
                        cmd.Parameters.AddWithValue("@lowerlim", low_lim);
                        cmd.Parameters.AddWithValue("@datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        /// <summary>
        /// Method to get session details to be included in Tolerance Report. Used in OutOfToleranceCheck class.
        /// </summary>
        /// <param name="sessionid"></param>
        public void GetSessionDetailsForReport(string sessionid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT start_time,end_time,started_by,panels_run,report_title,panels_data_saved FROM dle_calibration.session_details where session_id=@session_id";
                        cmd.Parameters.AddWithValue("@session_id", sessionid);//finalsession_id
                        using (MySqlDataReader session_details = cmd.ExecuteReader())
                        {
                            while (session_details.Read())
                            {
                                Start_Time = session_details[0].ToString();
                                End_Time = session_details[1].ToString();
                                Username = session_details[2].ToString();
                                PanelRun = session_details[3].ToString();
                                Rep_Title = session_details[4].ToString();
                                PanelSavedData = session_details[5].ToString();
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }         
            }
        }


        /// <summary>
        /// Method to get all out of tolerance values based on session id n OutOfToleranceCheck class.
        /// </summary>
        /// <param name="sessionid"></param>
        public void GetOutOfTolValues(string sessionid)
        {
            Tag.Clear(); ToleranceStatus.Clear(); ActualValue.Clear(); UpperLimit.Clear(); LowerLimit.Clear();
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM dle_calibration.report_details where session_id=@session_id";
                        cmd.Parameters.AddWithValue("@session_id", sessionid);//finalsession_id
                        using (MySqlDataReader outoftol_values = cmd.ExecuteReader())
                        {
                            while (outoftol_values.Read())
                            {
                                Tag.Add(outoftol_values[2].ToString());
                                ToleranceStatus.Add(outoftol_values[3].ToString());
                                ActualValue.Add(outoftol_values[4].ToString());
                                UpperLimit.Add(outoftol_values[5].ToString());
                                LowerLimit.Add(outoftol_values[6].ToString());
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        
        /// <summary>
        /// Method to check if there are any tags whose values are progressively increasing or decreasing in OutOfToleranceCheck form.
        /// </summary>
        /// <param name="panelid"></param>
        /// <param name="scenarioid"></param>
        public void CheckForProgressiveTrends(string panelid, string scenarioid)
        {
            progtrend_count = 0; 
            
            //Get progressive trends count from App config file
            try
            {
                progtrend_count = Convert.ToInt32(ConfigurationManager.AppSettings["progtrend-count"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
            

            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "Select * from dle_calibration.dle_values WHERE panel_id=@panelid AND dle_scenario_id=@scenario_id ORDER BY id DESC LIMIT 0,6";
                        cmd.Parameters.AddWithValue("@panelid", panelid);
                        cmd.Parameters.AddWithValue("@scenario_id", scenarioid);
                        cmd.Connection = connection;
                        using (MySqlDataReader progressivetrends = cmd.ExecuteReader())
                        {
                            if (progressivetrends.HasRows)
                            {
                                while (progressivetrends.Read())
                                {
                                    Rows++;
                                    Lux.Add(progressivetrends[4].ToString());
                                    Cct.Add(progressivetrends[5].ToString());
                                    if (!progressivetrends.IsDBNull(6))
                                        LuxTag = progressivetrends.GetString(6);
                                    if (!progressivetrends.IsDBNull(7))
                                        CctTag = progressivetrends.GetString(7);
                                }
                            }
                            if (Rows >= progtrend_count)
                            {
                                if ((Convert.ToDecimal(Lux[0]) > Convert.ToDecimal(Lux[1])) && (Convert.ToDecimal(Lux[1]) > Convert.ToDecimal(Lux[2])) && (Convert.ToDecimal(Lux[2]) > Convert.ToDecimal(Lux[3])) && (Convert.ToDecimal(Lux[3]) > Convert.ToDecimal(Lux[4])) && (Convert.ToDecimal(Lux[4]) > Convert.ToDecimal(Lux[5])))
                                {
                                    Increasing.Add("Value for " + LuxTag + " is increasing");
                                }
                                else if ((Convert.ToDecimal(Lux[0]) < Convert.ToDecimal(Lux[1])) && (Convert.ToDecimal(Lux[1]) < Convert.ToDecimal(Lux[2])) && (Convert.ToDecimal(Lux[2]) < Convert.ToDecimal(Lux[3])) && (Convert.ToDecimal(Lux[3]) < Convert.ToDecimal(Lux[4])) && (Convert.ToDecimal(Lux[4]) < Convert.ToDecimal(Lux[5])))
                                {
                                    Decreasing.Add("Value for " + LuxTag + " is decreasing");
                                }

                                if ((Convert.ToDecimal(Cct[0]) > Convert.ToDecimal(Cct[1])) && (Convert.ToDecimal(Cct[1]) > Convert.ToDecimal(Cct[2])) && (Convert.ToDecimal(Cct[2]) > Convert.ToDecimal(Cct[3])) && (Convert.ToDecimal(Cct[3]) > Convert.ToDecimal(Cct[4])) && (Convert.ToDecimal(Cct[4]) > Convert.ToDecimal(Cct[5])))
                                {
                                    Increasing.Add("Value for " + CctTag + " is increasing");
                                }
                                else if ((Convert.ToDecimal(Cct[0]) < Convert.ToDecimal(Cct[1])) && (Convert.ToDecimal(Cct[1]) < Convert.ToDecimal(Cct[2])) && (Convert.ToDecimal(Cct[2]) < Convert.ToDecimal(Cct[3])) && (Convert.ToDecimal(Cct[3]) < Convert.ToDecimal(Cct[4])) && (Convert.ToDecimal(Cct[4]) < Convert.ToDecimal(Cct[5])))
                                {
                                    Decreasing.Add("Value for " + CctTag + " is decreasing");
                                }
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


       /// <summary>
       /// Method to update artnetsettings in artnet settings form.
       /// </summary>
       /// <param name="ip"></param>
       /// <param name="port"></param>
        public void UpdateArtNetSettings(string ip, string port)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "UPDATE conf_artnet_settings SET artnet_ip=@artnet_ip, artnet_port=@artnet_port, updated_by=@updated_by, updated_on=@updated_on where id=1";
                        cmd.Parameters.AddWithValue("@artnet_ip", ip);
                        cmd.Parameters.AddWithValue("@artnet_port", port);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Method to update config logs based on status message
        /// </summary>
        /// <param name="message"></param>
        public void UpdateConfigLogs(string message)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO conf_logs(updated_by,datetime,action) VALUES(@updated_by,@datetime,@action)";
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@action", message);
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }         
            }
        }


        /// <summary>
        /// Method to get PI Settings details in PI Settings form.
        /// </summary>
        public void GetPISettings()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT pi_server,af_server,afdb FROM conf_pi_settings where id=1";
                        cmd.Connection = connection;
                        using (MySqlDataReader pisettings = cmd.ExecuteReader())
                        {
                            while (pisettings.Read())
                            {
                               
                                pi_Server = pisettings[0].ToString();
                                af_Server = pisettings[1].ToString();
                                af_DB = pisettings[2].ToString();
                            }
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }


      /// <summary>
      /// Method to update PI Settings in PI Settings form.
      /// </summary>
      /// <param name="piserver"></param>
      /// <param name="afserver"></param>
      /// <param name="afdb"></param>
        public void UpdatePISettings(string piserver, string afserver, string afdb)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE conf_pi_settings SET pi_server=@pi_server,af_server=@af_server,afdb=@afdb,updated_by=@updated_by,updated_on=@updated_on where id=1 ";
                        cmd.Parameters.AddWithValue("@pi_server", piserver);
                        cmd.Parameters.AddWithValue("@af_server", afserver);
                        cmd.Parameters.AddWithValue("@afdb", afdb);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }


        /// <summary>
        /// MEthod to update logs file location in logs file location form
        /// </summary>
        /// <param name="logs"></param>
        public void UpdateLogsFileLoc(string logs)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE conf_logfile_settings SET logfile_location=@logfile_loc,updated_by=@updated_by,updated_on=@updated_on where id=1 ";
                        cmd.Parameters.AddWithValue("@logfile_loc", logs);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        
        /// <summary>
        /// Method to get all email details to be displayed in Email Recipients form.
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable ViewEmailDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        MySqlDataAdapter MyDA = new MySqlDataAdapter();
                        string sqlSelectAll = "Select username AS \"Username\",email_id AS \"Email Address\",added_on AS \"Added On\" from conf_email_details";
                        MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);
                        DataTable table = new DataTable();
                        MyDA.Fill(table);
                        return table;
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        
        /// <summary>
        /// Method to delete email details of user in email recipients form.
        /// </summary>
        /// <param name="username"></param>
        public void DeleteEmailDetail(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "DELETE FROM conf_email_details where username=@username";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        /// <summary>
        /// Method to check if username exists in Email details in Add New User form.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string CheckUsernameExists(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM conf_email_details WHERE username=@username";
                        cmd.Parameters.AddWithValue("@username", username);
                        using (MySqlDataReader existingemaildetails = cmd.ExecuteReader())
                        {
                            if (existingemaildetails.HasRows)
                                return "Yes";
                            else
                                return "No";
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        

        /// <summary>
        /// Method to create new email details in Email-AddNewUser form.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void CreateEmailDetails(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO conf_email_details(username,email_id,created_by,added_on)VALUES(@username,@email,@created_by,@added_on)";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", password);
                        cmd.Parameters.AddWithValue("@created_by", Program.Username);
                        cmd.Parameters.AddWithValue("@added_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }



        /// <summary>
        /// Method to update SMTPSettings in SMTP Settings form.
        /// </summary>
        /// <param name="SMTPServer"></param>
        /// <param name="SMTPPort"></param>
        /// <param name="SMTPSendEmail"></param>
        /// <param name="EnableSSL"></param>
        public void UpdateSMTPSettings(string SMTPServer, string SMTPPort, string SMTPSendEmail, string SMTPSendEmailPwd,string EnableSSL)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE conf_smtp_settings SET smtp_server=@smtp_server,smtp_port=@smtp_port,smtp_send_email=@smtp_email,smtp_sendemail_password=@smtp_emailpassword,enable_ssl=@enable_ssl,updated_by=@updated_by,updated_on=@updated_on where id=1 ";
                        cmd.Parameters.AddWithValue("@smtp_server", SMTPServer);
                        cmd.Parameters.AddWithValue("@smtp_port", SMTPPort);
                        cmd.Parameters.AddWithValue("@smtp_email", SMTPSendEmail);
                        cmd.Parameters.AddWithValue("@smtp_emailpassword", SMTPSendEmailPwd);
                        cmd.Parameters.AddWithValue("@enable_ssl", EnableSSL);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        
        /// <summary>
        /// Method to get user email id based on username used in user profile form.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetLoggedInUserEmailID(string username)
        {
            string email_id = "";
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT email_id FROM login_users where username=@username ";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Connection = connection;
                        using (MySqlDataReader user = cmd.ExecuteReader())
                        {
                            while (user.Read())
                            {
                                email_id = user[0].ToString();
                            }
                            return email_id;
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                } 
            }
        }



        //Used by 1 form- User Profile form
        /// <summary>
        /// Method to update password and email id in user profile form
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="emailid"></param>
        public void UpdateLoggedInUserPasswordAndEmailDetails(string username, string password, string emailid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "UPDATE login_users SET password=sha2(@password,256),email_id=@email_id,updated_by=@updatedby,updated_on=@updatedon where username=@username;";
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email_id", emailid);
                        cmd.Parameters.AddWithValue("@updatedby", Program.Username);
                        cmd.Parameters.AddWithValue("@updatedon", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                } 
            }
        }


        /// <summary>
        /// Method to update only email address in User Profile Form.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="emailid"></param>
        public void UpdateOnlyEmail(string username, string emailid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "UPDATE login_users SET email_id=@email_id,updated_by=@updatedby,updated_on=@updatedon where username=@username;";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email_id", emailid);
                        cmd.Parameters.AddWithValue("@updatedby", Program.Username);
                        cmd.Parameters.AddWithValue("@updatedon", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }  
            }
        }


        
        /// <summary>
        /// Method to get logged in users details to be displayed in Users form.
        /// </summary>
        /// <returns></returns>
        public DataTable GetLoggedInUsersList()
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        MySqlDataAdapter MyDA = new MySqlDataAdapter();
                        string sqlSelectAll = "Select l.username AS Username,l.email_id AS \"Email Address\",l.is_active AS \"Is Active\",u.role_name AS \"User Role\",l.created_on AS \"Created On\",l.updated_on AS \"Updated On\" from login_users l,user_role u where l.user_type=u.role_id order by l.is_active desc,l.username asc ";
                        MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);
                        DataTable table = new DataTable();
                        MyDA.Fill(table);
                        return table;
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
             
            }
        }

        
        /// <summary>
        /// Method to get all user roles to be displayed in User-CreateNewuser form.
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserRoles()
        {
            List<string> user_role = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select role_name from user_role";
                        using (MySqlDataReader login = cmd.ExecuteReader())
                        {
                            while (login.Read())
                                user_role.Add(login[0].ToString());
                            return user_role;
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        
        /// <summary>
        /// Method to check if username or email id exists in login users form.Used in Users-CreateNewUserForm.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="emailid"></param>
        /// <returns></returns>
        public string CheckUsernameExistsInLoginUsers(string username,string emailid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM login_users WHERE username=@username or email_id=@emailid";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@emailid", emailid);
                        using (MySqlDataReader existingloginusers = cmd.ExecuteReader())
                        {
                            if (existingloginusers.HasRows)
                            {
                                return "Yes";
                            }
                            else
                                return "No";
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
              
            }
        }

        
        
        /// <summary>
        /// Method to get user role id based on role name to be used in User-CreateNewUserForm and User-UpdateUserForm.
        /// </summary>
        /// <param name="role_name"></param>
        /// <returns></returns>
        public int GetRoleIDBasedOnRoleName(string role_name)
        {
            int role_id = 0;
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select role_id from user_role where role_name=@role_name";
                        cmd.Parameters.AddWithValue("@role_name", role_name);
                        using (MySqlDataReader role = cmd.ExecuteReader())
                        {
                            while (role.Read())
                                role_id = Convert.ToInt32(role[0]);
                            return role_id;
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
              
            }
        }

        

        /// <summary>
        /// Method to create new login user in login_users form.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="roleid"></param>
        /// <param name="isactive"></param>
        public void CreateLoginUser(string username, string password, string email, int roleid, string isactive)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO login_users(username,password,email_id,user_type,created_by,created_on,is_active)VALUES(@username,sha2(@password,256),@email,@user_type,@created_by,@created_on,@is_active)";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@user_type", roleid);
                        cmd.Parameters.AddWithValue("@created_by", Program.Username);
                        cmd.Parameters.AddWithValue("@created_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@is_active", isactive);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Method to update user details in Update User form.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email_id"></param>
        /// <param name="roleid"></param>
        /// <param name="is_active"></param>
        public void UpdateUserDetails(string username, string email_id, int roleid, string is_active)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "UPDATE login_users SET user_type=@usertype,is_active=@is_active,email_id=@email_id,updated_by=@updated_by,updated_on=@updated_on where username='" + username + "';";
                        cmd.Parameters.AddWithValue("@email_id", email_id);
                        cmd.Parameters.AddWithValue("@usertype", roleid);
                        cmd.Parameters.AddWithValue("@is_active", is_active);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        /// <summary>
        /// Method to update serial port settings in SerialPortSettingsForm.
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="stopBits"></param>
        /// <param name="dataBits"></param>
        public void UpdateSerialPortSettings(string comPort, string baudRate, string parity, string stopBits, string dataBits)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE conf_serialport_settings SET com_port=@com_port,baud_rate=@baud_rate,parity=@parity,stop_bits=@stop_bits,data_bits=@data_bits,updated_by=@updated_by,updated_on=@updated_on where id=1 ";
                        cmd.Parameters.AddWithValue("@com_port", comPort);
                        cmd.Parameters.AddWithValue("@baud_rate", baudRate);
                        cmd.Parameters.AddWithValue("@parity", parity);
                        cmd.Parameters.AddWithValue("@stop_bits", stopBits);
                        cmd.Parameters.AddWithValue("@data_bits", dataBits);
                        cmd.Parameters.AddWithValue("@updated_by", Program.Username);
                        cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }

                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }


        
        /// <summary>
        /// Method to get the last updated tolerance date for all panels to be used in DLEDataMovingAvgFromPI class.
        /// </summary>
        /// <returns></returns>
        public List<string> GetLastUpdatedTolDateForPanels()
        {
            List<string> tol_lastUpdatedDate = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "Select updated_on from panel_movingavg_updatedetails where panel_id=@Panel_Id";
                        cmd.Parameters.AddWithValue("@Panel_Id", SqlDbType.Int);
                        for (int i = 1; i < 17; i++)
                        {
                            cmd.Parameters["@Panel_Id"].Value = i;
                            using (MySqlDataReader tol_date = cmd.ExecuteReader())
                            {
                                if (tol_date.HasRows)
                                {
                                    while (tol_date.Read())
                                        tol_lastUpdatedDate.Add(tol_date[0].ToString());
                                }
                            }
                        }
                        return tol_lastUpdatedDate;
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// //Method to update last updated date at which moving avg was calculated for specific panel to be used in DLEDataMovingAvgFromPI class.
        /// </summary>
        /// <param name="movingavg"></param>
        public void UpdateLastUpdatedDateForMovingAvg(List<int> movingavg)
        {
            List<string> movingavg_pk = new List<string>();
            for (int i = 0; i < movingavg.Count; i++)
            {
                if (movingavg[i] != 0)
                {
                    if (movingavg[i] < 10)
                        movingavg_pk.Add("M0" + (movingavg[i]));
                    else
                        movingavg_pk.Add("M" + (movingavg[i] ));
                }
            }
                using (MySqlConnection connection = new MySqlConnection(Conn))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                              cmd.Connection = connection;
                              for (int i = 0; i < movingavg_pk.Count; i++)
                              {
                              cmd.CommandText = "000UPDATE panel_movingavg_updatedetails SET updated_on=STR_TO_DATE(@updated_on,'%e/%c/%Y %r') where movingavg_id=@movingavg_id ";
                              cmd.Parameters.Clear();
                              cmd.Parameters.AddWithValue("@updated_on", DateTime.Now.ToString());    
                              cmd.Parameters.AddWithValue("@movingavg_id", movingavg_pk[i]);
                              cmd.ExecuteNonQuery();
                              }
                        }
                    }
                    catch (MySqlException mysqlex)
                    {
                        throw mysqlex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
        }


        /// <summary>
        /// Method to update panels for which data has been saved. Used in Calibration form.
        /// </summary>
        /// <param name="panel_savedcount"></param>
         public void UpdatePanelSaveCountDetails(int panel_savedcount)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE session_details SET panels_data_saved=@panels_data_saved where session_id=@session_id ";
                        cmd.Parameters.AddWithValue("@panels_data_saved", panel_savedcount.ToString());
                        cmd.Parameters.AddWithValue("@session_id", CurrentSession_ID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Method to get all Panel ids.Used in DLEDataMovingAvgFromPI class.
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllPanelIDs()
        {
            List<int> panelids = new List<int>();

            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT id from panel";
                        using (MySqlDataReader panelno = cmd.ExecuteReader())
                        {
                            while (panelno.Read())
                            {
                                panelids.Add(Convert.ToInt32(panelno[0]));
                            }
                        }
                    }
                    return panelids;
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        /// <summary>
        /// Method to get Panel name based on panel id. Used in DLEDataMovingAvgFromPI class.
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetPanelNameFromID(int panel)
        {
            string panel_name = "";
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT panel_name from panel where id=@panelid";
                        cmd.Parameters.AddWithValue("@panelid", panel);
                        using (MySqlDataReader panelno = cmd.ExecuteReader())
                        {
                            while (panelno.Read())
                            {
                                panel_name = panelno[0].ToString();
                            }
                        }
                    }
                    return panel_name;
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Method to check if username or email id already exists. Used in Email-AddNewUserForm.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="emailid"></param>
        /// <returns></returns>
        public string CheckUsernameEmailExistsInEmailDetails(string username, string emailid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM conf_email_details WHERE username=@username or email_id=@emailid";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@emailid", emailid);
                        using (MySqlDataReader existingloginusers = cmd.ExecuteReader())
                        {
                            if (existingloginusers.HasRows)
                            {
                                return "Yes";
                            }
                            else
                                return "No";
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        /// <summary>
        /// Method to check if email id entered by user already exists. Used in Calibration form. 
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns></returns>
        public string CheckEmailExistsInLogin(string username, string emailid)
        {
            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM login_users WHERE email_id=@emailid AND NOT username=@username";
                        cmd.Parameters.AddWithValue("@emailid", emailid);
                        cmd.Parameters.AddWithValue("@username", username);
                        using (MySqlDataReader existingloginusers = cmd.ExecuteReader())
                        {
                            if (existingloginusers.HasRows)
                            {
                                return "Yes";
                            }
                            else
                                return "No";
                        }
                    }
                }
                catch (MySqlException mysqlex)
                {
                    throw mysqlex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
