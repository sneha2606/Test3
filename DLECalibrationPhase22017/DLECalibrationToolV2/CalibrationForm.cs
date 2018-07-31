using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using DLECalibrationToolV2.Properties;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using System.Threading;
using OSIsoft.AF.PI;
using MultipleSensorClassLibrary;
using System.Runtime.InteropServices;
using System.Timers;
using System.IO.Ports;
using System.Net.Sockets;
using DLECalibrationToolV2.PI_AFSDK;
using DLECalibrationToolV2.DLE;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.Data;
using Microsoft.Win32;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// This is the Main Calibration Form 
    /// </summary>
    public partial class CalibrationForm : Form
    {
        
        #region Variables to store Konica values
        MultipleSensor multipleSensors = new MultipleSensor();                                                                              //Object to access Multiplesensor class library
        TimeSpan totalTime = new TimeSpan(0, 0, 0);                                                                                         //Used to calculate Time
        string[] konikaValues = new string[] { };                                                                                           //Array to store Konica values collected from the device
        int sensorData_Location = 0;                                                                                                        //Variable used to keep track of lux and cct values stored in sensor 0 , sensor 1 and sensor 2 arrays
        int err_Flag = 0;                                                                                                                   //Temporary variale to store err flag value in case dle operation is stopped by user
        public static List<string> tags = new List<String>();                                                                                             //temporary list to store tags for the panel and measurement location running
        #endregion

        #region String Array to store sensor readings for each panel
        string[] sensor0_LUX = new string[21];                                                                                              //Array to store sensor 0 Lux readings
        string[] sensor0_CCT = new string[21];                                                                                              //Array to store sensor 0 CCT readings
        string[] sensor1_LUX = new string[21];                                                                                              //Array to store sensor 1 Lux readings
        string[] sensor1_CCT = new string[21];                                                                                              //Array to store sensor 1 CCT readings
        string[] sensor2_LUX = new string[21];                                                                                              //Array to store sensor 2 Lux readings
        string[] sensor2_CCT = new string[21];                                                                                              //Array to store sensor 2 CCT readings
        List<string> avg_Sensor0_LUX = new List<string>();                                                                                  //List to store average of sensor 0 Lux readings
        List<string> avg_Sensor1_LUX = new List<string>();                                                                                  //List to store average of sensor 1 Lux readings
        List<string> avg_Sensor2_LUX = new List<string>();                                                                                  //List to store average of sensor 2 Lux readings
        List<string> avg_Sensor0_CCT = new List<string>();                                                                                  //List to store average of sensor 0 CCT readings
        List<string> avg_Sensor1_CCT = new List<string>();                                                                                  //List to store average of sensor 1 CCT readings
        List<string> avg_Sensor2_CCT = new List<string>();                                                                                  //List to store average of sensor 2 CCT readings

        string[] sensor0_lux_copy = new string[21];                                                                                         //Array to store copy of sensor 0 lux readings
        string[] sensor0_cct_copy = new string[21];                                                                                         //Array to store copy of sensor 0 cct readings
        string[] sensor1_lux_copy = new string[21];                                                                                         //Array to store copy of sensor 1 lux readings
        string[] sensor1_cct_copy = new string[21];                                                                                         //Array to store copy of sensor 1 cct readings
        string[] sensor2_lux_copy = new string[21];                                                                                         //Array to store copy of sensor 2 lux readings
        string[] sensor2_cct_copy = new string[21];                                                                                         //Array to store copy of sensor 2 cct readings

        double[] panelMPTSensor0LUX_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 0 lux lower limit values
        double[] panelMPTSensor0LUX_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 0 lux upper limit values
        double[] panelMPTSensor0CCT_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 0 cct lower limit values
        double[] panelMPTSensor0CCT_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 0 cct upper limit values
        double[] panelMPTSensor1LUX_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 1 lux lower limit values
        double[] panelMPTSensor1LUX_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 1 lux upper limit values
        double[] panelMPTSensor1CCT_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 1 cct lower limit values
        double[] panelMPTSensor1CCT_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 1 cct upper limit values
        double[] panelMPTSensor2LUX_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 2 lux lower limit values
        double[] panelMPTSensor2LUX_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 2 lux upper limit values
        double[] panelMPTSensor2CCT_LowerLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 2 cct lower limit values
        double[] panelMPTSensor2CCT_UpperLimit_values_copy = new double[7];                                                                 //Array to store copy of sensor 2 cct upper limit values
        #endregion

        #region Logs File related variables
        string logsTextFile_Name = String.Empty;                                                                                            //Variable to store logs file text name
        string logs_FilePath_WithFileName = String.Empty;                                                                                   //Variable to store logs file path and txt name
        #endregion

        #region Variables to store PI Tags
        string[] pitags_Array = new string[] { };                                                                                           //Array to store all PI tags
        List<string> pitags_Sensor0 = new List<string>();                                                                                   //List to store sensor 0 tags
        List<string> pitags_Sensor1 = new List<string>();                                                                                   //List to store sensor 1 tags
        List<string> pitags_Sensor2 = new List<string>();                                                                                   //List to store sensor 2 tags
        string usercomments_Tag = "";                                                                                                       //Variable to store user comments tag
        string stopComments_Tag = "";                                                                                                       //Variable to store stop comments tag
        #endregion

        #region DLE control Data variables
        List<byte[]> controlPanel_data = new List<byte[]>();                                                                                //List of byte arrays to control specific panel in DLE
        byte[] controlByte;                                                                                                                 //control byte array
        #endregion

        #region UI related Dictionary variables
        Dictionary<string, Color> button_Colour = new Dictionary<string, Color>();                                                          //Dictionary to store button colour for different DLE settings
        Dictionary<string, System.Windows.Forms.Button> listBTN = new Dictionary<string, System.Windows.Forms.Button>();                    //Dictionary to store button for all Panels
        Dictionary<string, System.Windows.Forms.Label> locationLabel = new Dictionary<string, System.Windows.Forms.Label>();                //Dictionary to store panel location label
        Dictionary<string, System.Windows.Forms.Label> timeTakenLabel = new Dictionary<string, System.Windows.Forms.Label>();               //Dictionary to store ime taken label for each panel
        #endregion

        #region Variables related to tolerance calculation
        //int valuesZero = 0;                                                                                                               //Variable to keep track of values which are zero
        int outOfTol = 0;                                                                                                                   //Variable to keep track of values out of tolerance
        public static string tol_FileLoc = " ";                                                                                             //Variable to store Tolerance file location
        OutOfToleranceCheck tolchk = new OutOfToleranceCheck();
        PIToleranceTags pitoltag = new PIToleranceTags();
        #endregion

        #region Report Details variables
        public string tolRepLoc = "";                                                                                                       //Variable to store Tolerance file location
        string report_Title = "";                                                                                                           //To store the Report Title
        #endregion

        #region DLE related vairables
        Dictionary<string, int> panelDetails = new Dictionary<string, int>();
        string panel = "";                                                                                                                  //To store panel and mpt location selected by user eg Panel1Left
        string state = "";                                                                                                                  //Variable to store the state for firing the DLE (Warm16,Warm64,Warm128,Cool16,Cool64,Cool128,Combined64)
        int[] panelList_RunCount;                                                                                                           //Array to store number of times panel has been run during each session 
        int[] panelList_SaveCount;                                                                                                          //Array to store number of times data has been saved for each panel during each session 
        List<string> panelsSaveData = new List<string>();                                                                                   //List to store the panel names which have been run during each session to be used while displaying message to user
        string message = String.Empty;                                                                                                      //Variable to store message on click of exit session button
        string error_Message = "";                                                                                                          //Variable to store error message
        bool panel_datasaved;
        #endregion

        #region Thread variables
        Thread thread_ChkPrevSessionRepSent = null;                                                                                          //Thread variable to check if report is sent for previous session
        string temp = "";                                                                                                                    //temporary variable    
        int workdone = 0;                                                                                                                    //Variable to indicate if work has been completed in thread
        #endregion

     
        #region Timer - Auto timeout session
        System.Windows.Forms.Timer MyTimer;                                                                                                  //Timer to time each Calibration Session
        int session_timeout=Convert.ToInt32(ConfigurationManager.AppSettings["session-autotimeout"].ToString());                             //Stores duration for session timeout from App Config file
        //TimeSpan elapsed_time;
        //int timer_interval = 0;
        //int temp_var;
        //DateTime timerstartTime;
        //DateTime lockoutTime;
        //bool session_status=false;
        //DateTime unlockTime;
        #endregion

        #region Annotation related variables
        Dictionary<string, string> lux = new Dictionary<string, string>();                                                                    //Dictionary to store Lux tags based on cell value selected from gridview table 
        Dictionary<string, string> cct = new Dictionary<string, string>();                                                                    //Dictionary to store CCT tags based on cell value selected from gridview table 
        Dictionary<int, string> row = new Dictionary<int, string>();                                                                          //Dictionary containing mapping between cell number and lux/cct tag number 
        #endregion

        DatabaseConnectMySQL db_connect = new DatabaseConnectMySQL();
        TextWriter txt;
      

        /// <summary>
        /// Constructor - initialise UI related variables
        /// </summary>
        public CalibrationForm()
        {
            InitializeComponent();

            //Calibration Form - UI Settings
            this.Visible = false;
            this.WindowState = FormWindowState.Maximized;
            //this.BackgroundImage = Resource1.LOGO_1932x1015;
            this.BackgroundImage = Resource1.LOGO_1932X1050_center;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            //List containing button colours for different DLE settings
            button_Colour.Add("Warm-16", Color.Yellow);
            button_Colour.Add("Warm-64", Color.Orange);
            button_Colour.Add("Warm-128", Color.DarkOrange);
            button_Colour.Add("Cool-16", Color.LightCyan);
            button_Colour.Add("Cool-64", Color.SkyBlue);
            button_Colour.Add("Cool-128", Color.DeepSkyBlue);
            button_Colour.Add("Combined-64", Color.MediumSeaGreen);

            //List containing buttons which are used to display the different panels in UI
            listBTN.Add("Panel-1", btnPanel1);
            listBTN.Add("Panel-2", btnPanel2);
            listBTN.Add("Panel-3", btnPanel3);
            listBTN.Add("Panel-4", btnPanel4);
            listBTN.Add("Panel-5", btnPanel5);
            listBTN.Add("Panel-6", btnPanel6);
            listBTN.Add("Panel-7", btnPanel7);
            listBTN.Add("Panel-8", btnPanel8);

            //List containing labels to display location of different panels and measurement location
            locationLabel.Add("Panel-1Left", labelPanel1Left);
            locationLabel.Add("Panel-1Right", labelPanel1Right);
            locationLabel.Add("Panel-2Left", labelPanel2Left);
            locationLabel.Add("Panel-2Right", labelPanel2Right);
            locationLabel.Add("Panel-3Left", labelPanel3Left);
            locationLabel.Add("Panel-3Right", labelPanel3Right);
            locationLabel.Add("Panel-4Left", labelPanel4Left);
            locationLabel.Add("Panel-4Right", labelPanel4Right);
            locationLabel.Add("Panel-5Left", labelPanel5Left);
            locationLabel.Add("Panel-5Right", labelPanel5Right);
            locationLabel.Add("Panel-6Left", labelPanel6Left);
            locationLabel.Add("Panel-6Right", labelPanel6Right);
            locationLabel.Add("Panel-7Left", labelPanel7Left);
            locationLabel.Add("Panel-7Right", labelPanel7Right);
            locationLabel.Add("Panel-8Left", labelPanel8Left);
            locationLabel.Add("Panel-8Right", labelPanel8Right);

            //List containing labels to display time taken for Panel run for Panel and Mpt location
            timeTakenLabel.Add("Panel-1Left", labelPanel1LeftData);
            timeTakenLabel.Add("Panel-1Right", labelPanel1RightData);
            timeTakenLabel.Add("Panel-2Left", labelPanel2LeftData);
            timeTakenLabel.Add("Panel-2Right", labelPanel2RightData);
            timeTakenLabel.Add("Panel-3Left", labelPanel3LeftData);
            timeTakenLabel.Add("Panel-3Right", labelPanel3RightData);
            timeTakenLabel.Add("Panel-4Left", labelPanel4LeftData);
            timeTakenLabel.Add("Panel-4Right", labelPanel4RightData);
            timeTakenLabel.Add("Panel-5Left", labelPanel5LeftData);
            timeTakenLabel.Add("Panel-5Right", labelPanel5RightData);
            timeTakenLabel.Add("Panel-6Left", labelPanel6LeftData);
            timeTakenLabel.Add("Panel-6Right", labelPanel6RightData);
            timeTakenLabel.Add("Panel-7Left", labelPanel7LeftData);
            timeTakenLabel.Add("Panel-7Right", labelPanel7RightData);
            timeTakenLabel.Add("Panel-8Left", labelPanel8LeftData);
            timeTakenLabel.Add("Panel-8Right", labelPanel8RightData);

            //List to store panel and measurement location and corresponding panel number
            panelDetails.Add("Panel-1Left", 1);
            panelDetails.Add("Panel-1Right", 2);
            panelDetails.Add("Panel-2Left", 3);
            panelDetails.Add("Panel-2Right", 4);
            panelDetails.Add("Panel-3Left", 5);
            panelDetails.Add("Panel-3Right", 6);
            panelDetails.Add("Panel-4Left", 7);
            panelDetails.Add("Panel-4Right", 8);
            panelDetails.Add("Panel-5Left", 9);
            panelDetails.Add("Panel-5Right", 10);
            panelDetails.Add("Panel-6Left", 11);
            panelDetails.Add("Panel-6Right", 12);
            panelDetails.Add("Panel-7Left", 13);
            panelDetails.Add("Panel-7Right", 14);
            panelDetails.Add("Panel-8Left", 15);
            panelDetails.Add("Panel-8Right", 16);

        }



        /// <summary>
        /// Form load - Set UI for Calibration form based on User type and calculate moving avg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibrationForm_Load(object sender, EventArgs e)
        {
            //Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            //UI Settings
            //If logged in user is Admin, all menus will be displayed
            if (Program.UserType == "Admin")
            { 
                adminToolStripMenuItem.Visible = true;
            }

            //Adding column headers for datagridview Illuminance
            this.dataGridView_Illuminance.Columns.Add("Warm-16", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Warm-16", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Warm-16", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Warm-64", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Warm-64", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Warm-64", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Warm-128", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Warm-128", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Warm-128", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Cool-16", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Cool-16", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Cool-16", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Cool-64", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Cool-64", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Cool-64", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Cool-128", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Cool-128", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Cool-128", "Value-3");
            this.dataGridView_Illuminance.Columns.Add("Combined-128", "Value-1");
            this.dataGridView_Illuminance.Columns.Add("Combined-128", "Value-2");
            this.dataGridView_Illuminance.Columns.Add("Combined-128", "Value-3");

            for (int j = 0; j < this.dataGridView_Illuminance.ColumnCount; j++)
            {
                this.dataGridView_Illuminance.Columns[j].Width = 80;
            }

            this.dataGridView_Illuminance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView_Illuminance.ColumnHeadersHeight = this.dataGridView_Illuminance.ColumnHeadersHeight * 2;
            this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dataGridView_Illuminance.ColumnWidthChanged += new DataGridViewColumnEventHandler(dataGridView_Illuminance_ColumnWidthChanged);

            //Adding column headers for datagridview CCT
            this.dataGridView_CCT.Columns.Add("Warm-16", "Value-1");
            this.dataGridView_CCT.Columns.Add("Warm-16", "Value-2");
            this.dataGridView_CCT.Columns.Add("Warm-16", "Value-3");
            this.dataGridView_CCT.Columns.Add("Warm-64", "Value-1");
            this.dataGridView_CCT.Columns.Add("Warm-64", "Value-2");
            this.dataGridView_CCT.Columns.Add("Warm-64", "Value-3");
            this.dataGridView_CCT.Columns.Add("Warm-128", "Value-1");
            this.dataGridView_CCT.Columns.Add("Warm-128", "Value-2");
            this.dataGridView_CCT.Columns.Add("Warm-128", "Value-3");
            this.dataGridView_CCT.Columns.Add("Cool-16", "Value-1");
            this.dataGridView_CCT.Columns.Add("Cool-16", "Value-2");
            this.dataGridView_CCT.Columns.Add("Cool-16", "Value-3");
            this.dataGridView_CCT.Columns.Add("Cool-64", "Value-1");
            this.dataGridView_CCT.Columns.Add("Cool-64", "Value-2");
            this.dataGridView_CCT.Columns.Add("Cool-64", "Value-3");
            this.dataGridView_CCT.Columns.Add("Cool-128", "Value-1");
            this.dataGridView_CCT.Columns.Add("Cool-128", "Value-2");
            this.dataGridView_CCT.Columns.Add("Cool-128", "Value-3");
            this.dataGridView_CCT.Columns.Add("Combined-128", "Value-1");
            this.dataGridView_CCT.Columns.Add("Combined-128", "Value-2");
            this.dataGridView_CCT.Columns.Add("Combined-128", "Value-3");

            for (int j = 0; j < this.dataGridView_CCT.ColumnCount; j++)
                this.dataGridView_CCT.Columns[j].Width = 80;

            this.dataGridView_CCT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView_CCT.ColumnHeadersHeight = this.dataGridView_CCT.ColumnHeadersHeight * 2;
            this.dataGridView_CCT.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dataGridView_CCT.ColumnWidthChanged += new DataGridViewColumnEventHandler(dataGridView_CCT_ColumnWidthChanged);

            foreach (DataGridViewColumn c in dataGridView_Illuminance.Columns)
            {
                c.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, GraphicsUnit.Pixel);
            }

            foreach (DataGridViewColumn c in dataGridView_CCT.Columns)
            {
                c.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, GraphicsUnit.Pixel);
            }

            dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            dataGridView_CCT.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);

            DateTime.Now.Date.ToString("MM/dd/yyyy");

            dataGridView_Illuminance.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView_CCT.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //Get PI Server name from db
            try
            {
                db_connect.GetPIServerName();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.movingavg_PIerror);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + DisplayMessages.pisettings_chk, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.movingavg_PIerror);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get Tolerance file location from app config file
            try
            {
                tol_FileLoc = ConfigurationManager.AppSettings["tolerance-filelocation"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
            
            MessageBox.Show(Form.ActiveForm, DisplayMessages.movavg_recalccheck); 

            try
            {
                //Calculate Moving Avg
                DLEDataMovingAvgFromPI PI = new DLEDataMovingAvgFromPI();
                PI.CheckMovingAvg();
            }
            catch (ThreadStateException th_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + th_ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + th_ex.GetType().ToString() + ", Target Site : " + th_ex.TargetSite + ",\tError Message : " + th_ex.Message + ", Inner Exception : " + th_ex.InnerException);
                return;
            }
            catch (OutOfMemoryException ome_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ome_ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ome_ex.GetType().ToString() + ", Target Site : " + ome_ex.TargetSite + ",\tError Message : " + ome_ex.Message + ", Inner Exception : " + ome_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
     
       }



        /// <summary>
        /// To avoid flickering in UI
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        /// <summary>
        /// Method to adjust column width in dataGridView_Illuminance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_Illuminance_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            System.Drawing.Rectangle rtHeader = this.dataGridView_Illuminance.DisplayRectangle;
            rtHeader.Height = this.dataGridView_Illuminance.ColumnHeadersHeight / 2;
            this.dataGridView_Illuminance.Invalidate(rtHeader);
        }


        /// <summary>
        /// Method to adjust column width in dataGridView_CCT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CCT_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            System.Drawing.Rectangle rtHeader = this.dataGridView_Illuminance.DisplayRectangle;
            rtHeader.Height = this.dataGridView_Illuminance.ColumnHeadersHeight / 2;
            this.dataGridView_Illuminance.Invalidate(rtHeader);
        }


        /// <summary>
        /// Method to paint dataGridView_Illuminance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_Illuminance_Paint(object sender, PaintEventArgs e)
        {
            int j;
            string[] states = { "Warm-16", "Warm-64", "Warm-128", "Cool-16", "Cool-64", "Cool-128", "Combined-64" };
            for (j = 0; j < 21; )
            {
                System.Drawing.Rectangle r1 = this.dataGridView_Illuminance.GetCellDisplayRectangle(j, -1, true);
                int w2 = this.dataGridView_Illuminance.GetCellDisplayRectangle(j + 1, -1, true).Width + this.dataGridView_Illuminance.GetCellDisplayRectangle(j + 2, -1, true).Width;
                r1.X += 1;
                r1.Y += 1;
                r1.Width = r1.Width + w2;
                r1.Height = r1.Height / 2;
                e.Graphics.FillRectangle(new SolidBrush(this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.BackColor), r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(states[j / 3], this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);
                j = j + 3;
            }
        }



        /// <summary>
        /// Method to paint dataGridView_CCT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CCT_Paint(object sender, PaintEventArgs e)
        {
            int j;
            string[] states = { "Warm-16", "Warm-64", "Warm-128", "Cool-16", "Cool-64", "Cool-128", "Combined-64" };
            for (j = 0; j < 21; )
            {
                System.Drawing.Rectangle r1 = this.dataGridView_Illuminance.GetCellDisplayRectangle(j, -1, true);
                int w2 = this.dataGridView_Illuminance.GetCellDisplayRectangle(j + 1, -1, true).Width + this.dataGridView_Illuminance.GetCellDisplayRectangle(j + 2, -1, true).Width;
                r1.X += 1;
                r1.Y += 1;
                r1.Width = r1.Width + w2 - 2;
                r1.Height = r1.Height / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.BackColor), r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(states[j / 3],
                this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);
                j += 3;
            }
        }


        /// <summary>
        /// Method to clear user selection when cell is clicked in dataGridView_Illuminance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_Illuminance_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView_Illuminance.ClearSelection();
        }


        /// <summary>
        /// Method to clear user selection when cell is clicked in dataGridView_CCT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CCT_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView_CCT.ClearSelection();
        }


        /// <summary>
        /// Start New session click - setup UI 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            panelList_RunCount = new int[16];
            panelList_SaveCount = new int[16];
            temp = "start";
            labelDataPanelNumer.Text = "";
            labelMptLocDetails.Text = "";
            //temp_var = 0; 
            //session_status = true;

            string errorsLogPathName = ConfigurationManager.AppSettings["errorlog-filelocation"].ToString();
            try
            {
                //Check if folder exists in path 
                bool exists = System.IO.Directory.Exists(errorsLogPathName);
                if (!exists)
                {
                    MessageBox.Show(DisplayMessages.logsFolder_notFound + errorsLogPathName);
                    return;
                }      
            }
            catch (DirectoryNotFoundException dex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.errorlogslocation_incorrect + dex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //get PI Server name from db
            try
            {
                db_connect.GetPIServerName();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.pisettings_chk);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get Serial Port settings from database
            try
            {
                db_connect.GetSerialPortSettings();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            Parity parity = Parity.None;
            //Set serial port settings in Konica device based on serial port settings in database
            try
            {
                switch (db_connect.Parity)
                {
                    case "Parity.Even": parity = System.IO.Ports.Parity.Even;
                        break;
                    case "Parity.Odd": parity = System.IO.Ports.Parity.Odd;
                        break;
                    case "Parity.None": parity = System.IO.Ports.Parity.None;
                        break;
                }
                //Set serial port settings
                multipleSensors.serialPortConfigurations(db_connect.Com_Port, parity, db_connect.Baud_Rate, db_connect.Stop_Bits, db_connect.Data_Bits);
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Form.ActiveForm,DisplayMessages.comport_check);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.comport_check);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Creating new session ID
            try
            {
                db_connect.CreateNewSessionID();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get Logs File Location from database
            try
            {
                db_connect.GetLogFileLocation();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);          
                return;
            }

            //Creating logs file name for the session
            logsTextFile_Name = "Logs-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + "hrs-" + DateTime.Now.Minute.ToString() + "mins-" + DateTime.Now.Second.ToString() + "secs"+".txt";            // Variable to store Name format for Status logs in Notepad             
            try
            {
                logs_FilePath_WithFileName = db_connect.LogsFilePath_FromDB + "\\" + logsTextFile_Name;
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            int num = 0;
            row.Clear();
            //Create dictionary to identify cell clicked to add annotation eg : Cells 0 1 and 2 correspond to tag 1  
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    row.Add(num, i.ToString());
                    num++;
                }
            }

            //Clear all panel run time
            foreach (var i in timeTakenLabel)
                i.Value.Text = "";

            //UI Settings
            radioButton_Left.Checked = false;
            radioButton_Right.Checked = false;
            comboBox_PanelNumbers.SelectedItem = null;
            totalTime = TimeSpan.Zero;
            report_Title = "";

            dataGridView_Illuminance.Rows.Clear();
            dataGridView_CCT.Rows.Clear();
            dataGridView_CCT.Enabled = false;
            dataGridView_Illuminance.Enabled = false;

            labelSessionStartTime.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Bold);
            labelSessionStartTimeData.Text = DateTime.Now.ToString();
            labelSessionEndTimeData.Text = String.Empty;
            labelSessionEndTimeData.Visible = false;
            labelDateData.Text = DateTime.Now.Date.ToString("d");

            //Writing status to status box
            try
            {
                textBox_Status.Text = "Session User Name: " + Program.Username + Environment.NewLine;
                textBox_Status.Text = textBox_Status.Text + "Session Start Time: " + labelSessionStartTimeData.Text + Environment.NewLine + Environment.NewLine; ;
                textBox_Status.Text = textBox_Status.Text + "Date-Time\t\t\t\tStatus Message" + Environment.NewLine;
            }
            catch (DirectoryNotFoundException dir_ex)
            {   
                MessageBox.Show(Form.ActiveForm, DisplayMessages.errorlogslocation_incorrect + dir_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + dir_ex.GetType().ToString() + ", Target Site : " + dir_ex.TargetSite + ",\tError Message : " + dir_ex.Message + ", Inner Exception : " + dir_ex.InnerException);
                return;
            }
            catch (IOException io_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + io_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + io_ex.GetType().ToString() + ", Target Site : " + io_ex.TargetSite + ",\tError Message : " + io_ex.Message + ", Inner Exception : " + io_ex.InnerException);
                return;
            }
            catch (UnauthorizedAccessException unac_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + unac_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + unac_ex.GetType().ToString() + ", Target Site : " + unac_ex.TargetSite + ",\tError Message : " + unac_ex.Message + ", Inner Exception : " + unac_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Start timer to auto timeout after 120 mins
            MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Enabled = true;
            MyTimer.Interval = (session_timeout *1000); // 120 mins
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
            //timer_interval = 0;
            
            
            //UI changes
            labelSessionEndTime.Visible = false;
            labelSessionEndTime.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
            btnAboveTol.Visible = false;
            btnBelowTol.Visible = false;
            labelAboveTol.Visible = false;
            labelBelowTol.Visible = false;
            btnWithinTol.Visible = false;
            labelWithinTol.Visible = false;
            labelTotalTimeLeft.Visible = true;
            labelTotalTimeRight.Visible = true;
            groupBoxLegend.Visible = false; ;
            this.BackgroundImage = base.BackgroundImage;
            this.BackgroundImage = null;
            this.BackColor = Color.White;
            tableLayoutPanel1.Visible = true;
            btnStop.Enabled = false;
            btnExit.Enabled = true;
            newToolStripMenuItem.Enabled = false;
            groupBox_DLE.Visible = true;
            tableLayoutPanel1.Visible = true;
            btnExit.Visible = true;
            labelStartTimeValue.Text = "";
            labelStopTimeValue.Text = "";
            labelTotalTimeValue.Text = "";
            labelPanel1Left.ForeColor = Color.Black;
            labelPanel2Left.ForeColor = Color.Black;
            labelPanel3Left.ForeColor = Color.Black;
            labelPanel4Left.ForeColor = Color.Black;
            labelPanel5Left.ForeColor = Color.Black;
            labelPanel6Left.ForeColor = Color.Black;
            labelPanel7Left.ForeColor = Color.Black;
            labelPanel8Left.ForeColor = Color.Black;
            labelPanel1Right.ForeColor = Color.Black;
            labelPanel2Right.ForeColor = Color.Black;
            labelPanel3Right.ForeColor = Color.Black;
            labelPanel4Right.ForeColor = Color.Black;
            labelPanel5Right.ForeColor = Color.Black;
            labelPanel6Right.ForeColor = Color.Black;
            labelPanel7Right.ForeColor = Color.Black;
            labelPanel8Right.ForeColor = Color.Black;

            labelPanel1Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel2Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel3Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel4Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel5Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel6Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel7Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel8Left.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel1Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel2Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel3Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel4Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel5Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel6Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel7Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);
            labelPanel8Right.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Regular);

            //Check if report has been sent through email for previous session
            try
            {
                thread_ChkPrevSessionRepSent = new Thread(new ThreadStart(DoingWork));
                thread_ChkPrevSessionRepSent.Start();
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (ThreadStateException th_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + th_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + th_ex.GetType().ToString() + ", Target Site : " + th_ex.TargetSite + ",\tError Message : " + th_ex.Message + ", Inner Exception : " + th_ex.InnerException);
                return;
            }
            catch (OutOfMemoryException ome_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ome_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ome_ex.GetType().ToString() + ", Target Site : " + ome_ex.TargetSite + ",\tError Message : " + ome_ex.Message + ", Inner Exception : " + ome_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {  
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Session Details
            report_Title = "Report for DLE Calibration run on " + labelSessionStartTimeData.Text;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            //Update session details table
            try
            {
                db_connect.UpdateSessionDetailsTable(labelSessionStartTimeData.Text, report_Title, Dns.GetHostName().ToString(), ipAddress.ToString());
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Timer tick function called when session has auto timed out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                auto_exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }



        private void auto_exit()
        {
            panelsSaveData.Clear();

            //Store the panels for which data has been saved
            for (int i = 1; i <= panelList_SaveCount.Length; i++)
            {
                if (panelList_SaveCount[i - 1] > 0)
                {
                    if (!panelsSaveData.Contains(panelDetails.FirstOrDefault(x => x.Value == i).Key)) //get key from value
                        panelsSaveData.Add(panelDetails.FirstOrDefault(x => x.Value == i).Key);
                }
            }
            //Set exit_app flag to 1 as session was auto exited
            Program.exit_app_flag = 1;

            //call function to perform updates on exit . 
            btnExit_updates(true);

            //Stop and Disable the timer
            MyTimer.Stop();
            MyTimer.Enabled = false;

            //Exit Application
            System.Windows.Forms.Application.Exit();
        }


        /// <summary>
        /// Method to check if report has been sent in previous session and to send the report if it has not been sent
        /// </summary>
        public void DoingWork()
        {
            string email_sent = db_connect.CheckPrevSessionEmailSent();
            if (email_sent == "No")
            {
                tolchk.reportDetails(db_connect.PrevSession_ID, temp);
            }
        }


        /// <summary>
        /// Start button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Confirm from user if jig has been placed correctly
            if ((comboBox_PanelNumbers.Text == "") || ((radioButton_Left.Checked == false) && (radioButton_Right.Checked == false)))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.dlecontrol_notselected);
            }
            else
            {
                {
                    try
                    {
                        DialogResult dialogResult = MessageBox.Show(Form.ActiveForm, DisplayMessages.jigposition_confirm, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //UI Settings
                            avg_Sensor0_LUX.Clear();
                            avg_Sensor0_CCT.Clear();
                            avg_Sensor1_LUX.Clear();
                            avg_Sensor1_CCT.Clear();
                            avg_Sensor2_LUX.Clear();
                            avg_Sensor2_CCT.Clear();
                            controlPanel_data.Clear();
                            panel = ""; err_Flag = 0;
                            btnAboveTol.Visible = false;
                            btnBelowTol.Visible = false;
                            labelAboveTol.Visible = false;
                            labelBelowTol.Visible = false;
                            groupBoxLegend.Visible = false;
                            labelWithinTol.Visible = false;
                            btnWithinTol.Visible = false;
                            comboBox_PanelNumbers.Enabled = false;
                            radioButton_Left.Enabled = false;
                            radioButton_Right.Enabled = false;
                            dataGridView_Illuminance.Rows.Clear();
                            dataGridView_CCT.Rows.Clear();
                            dataGridView_Illuminance.Rows.Clear();
                            dataGridView_CCT.Rows.Clear();
                            labelStartTimeValue.Text = "";
                            labelStopTimeValue.Text = "";
                            labelStartTimeValue.Text = DateTime.Now.ToString("hh:mm:ss tt");
                            btnDLESetting.Visible = true;
                            labelDLESetting.Visible = true;
                            btnStart.Enabled = false;
                            btnStop.Enabled = true;
                            btnExit.Enabled = false;
                            btnDLESetting.Visible = true;
                            this.dataGridView_CCT.Enabled = false;
                            this.dataGridView_Illuminance.Enabled = false;
                            tags.Clear();
                            Array.Clear(sensor0_CCT, 0, sensor0_CCT.Length);
                            Array.Clear(sensor0_LUX, 0, sensor0_LUX.Length);
                            Array.Clear(sensor1_CCT, 0, sensor1_CCT.Length);
                            Array.Clear(sensor1_LUX, 0, sensor1_LUX.Length);
                            Array.Clear(sensor2_CCT, 0, sensor2_CCT.Length);
                            Array.Clear(sensor2_LUX, 0, sensor2_LUX.Length);
                            Array.Clear(pitags_Array, 0, pitags_Array.Length);
                            checkComboBoxStatus();

                            //If location label panel label colour is Forest green , it indicates that data has been saved for the panel mpt loc
                            if (locationLabel[panel].ForeColor == Color.ForestGreen)
                                panel_datasaved = true;
                            else
                                panel_datasaved = false;

                            //Location label colour is changed to Limegreen to indicate that data is currently being taken for that panel mpt loc
                            locationLabel[panel].ForeColor = Color.LimeGreen;
                            locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Bold);
                            timeTakenLabel[panel].Visible = true;
                            
                            //Updated textbox Status
                            textBox_Status.Text = textBox_Status.Text + Environment.NewLine + Environment.NewLine + "Data for " + panel.Substring(0, 7) + " Measurement Location " + panel.Substring(7) + " " + Environment.NewLine + Environment.NewLine;

                            //Update panel run for which panel has run
                            updatePanelRuncount();

                            //Control DLE at different settings
                            dle_Running("Warm-16", panel, controlPanel_data[0]);
                        }
                    }

                   
                    catch (MySqlException mysqlex)
                    {
                        MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + mysqlex.Message;
                        return;
                    }
                    catch (ArgumentNullException ax)
                    {
                        MessageBox.Show(DisplayMessages.client_argumentnullexception + ax.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ax.GetType().ToString() + ", Target Site : " + ax.TargetSite + ",\tError Message : " + ax.Message + ", Inner Exception : " + ax.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ax.Message;
                        return;
                    }
                    catch (ArgumentOutOfRangeException ax)
                    {
                        MessageBox.Show(DisplayMessages.client_argumentoutofrangeexception + ax.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ax.GetType().ToString() + ", Target Site : " + ax.TargetSite + ",\tError Message : " + ax.Message + ", Inner Exception : " + ax.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ax.Message;
                        return;
                    }
                    catch (ObjectDisposedException ox)
                    {
                        MessageBox.Show(DisplayMessages.client_objectdisposedexception + ox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ox.GetType().ToString() + ", Target Site : " + ox.TargetSite + ",\tError Message : " + ox.Message + ", Inner Exception : " + ox.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ox.Message;
                        return;
                    }
                    catch (InvalidOperationException ix)
                    {
                        MessageBox.Show(DisplayMessages.client_invalidoperatorexception + ix.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ix.GetType().ToString() + ", Target Site : " + ix.TargetSite + ",\tError Message : " + ix.Message + ", Inner Exception : " + ix.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ix.Message;
                        return;
                    }
                    catch (SocketException sx)
                    {
                        MessageBox.Show(DisplayMessages.client_socketexception + sx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + sx.GetType().ToString() + ", Target Site : " + sx.TargetSite + ",\tError Message : " + sx.Message + ", Inner Exception : " + sx.InnerException);
                        dle_SwitchOff();
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + sx.Message;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dle_SwitchOff();
                        Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                        textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ex.Message;
                        return;
                    }

                }
            }
        }


        /// <summary>
        /// Method to get and set panel and measurement location related variables
        /// </summary>
        private void checkComboBoxStatus()
        {
            outOfTol = 0;
            usercomments_Tag = "";
            pitags_Array = null;

            //Store Measurement location selected
            panel = comboBox_PanelNumbers.Text + (radioButton_Left.Checked ? "Left" : "Right");

            //Store panel and measurement location related tags
            PanelDetails panelDetails = new PanelDetails();
            panelDetails.panelTags(panel, pitoltag);
            controlPanel_data = panelDetails.controlpaneldata;


            pitags_Array = panelDetails.paneltags;
            pitoltag.GetToleranceValuesFromPI();
            usercomments_Tag = PITags.user_commentsTag;
            stopComments_Tag = PITags.stop_session_commentsTag;

            //UI updates
            labelDataPanelNumer.Text = "\t\t\t\tData for " + panel.Substring(0, 7);
            labelMptLocDetails.Text = "Measurement Location " + panel.Substring(7);
        }


        /// <summary>
        /// Method to control DLE based on DLE setting panel and measurement location selected
        /// </summary>
        /// <param name="states"></param>
        /// <param name="panel"></param>
        /// <param name="controlpaneldata"></param>
        private void dle_Running(string states, string panel, byte[] controlpaneldata)
        {
            //UI related setttings
            state = states;
            textBox_Status.Text = textBox_Status.Text + DateTime.Now + "\t\t\t" + panel.Substring(0, 6) + " " + panel.Substring(6, 1) + " " + state + " is fired" + " " + Environment.NewLine;
            listBTN[panel.Substring(0, 7)].BackColor = button_Colour[state];
            btnDLESetting.BackColor = button_Colour[state];
            labelDLESetting.Text = state + " is fired";

            //Control DLE
            controlByte = new DLEControl().createbytearray(new DLEControlByteConstants().header, controlpaneldata);
            new DLEControl().senddpdmxmessage(controlByte);
            backgroundWorker1.RunWorkerAsync();
            
            //Update textbox status
            textBox_Status.Text = textBox_Status.Text + "\t\t\t\t\tAcquiring data.." + Environment.NewLine;

            //Switch Off DLE if error occured
            if (err_Flag == 1)
                dle_SwitchOff();
        }



        /// <summary>
        /// Stop button Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy == true)
                {
                    backgroundWorker1.CancelAsync();
                    err_Flag = 1;
                }

                //Switch off dle
                dle_SwitchOff();

                Program.comments = "";

                //Comment box to enter comments when stop button is clicked
                StopDLECommentBox stopDLEComments = new StopDLECommentBox(db_connect.PiServer, stopComments_Tag);
                stopDLEComments.ShowDialog();

                //Update textbox status with comments entered by user
                if (stopDLEComments.stopbox_comments!= "")
                    textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Reason for aborting DLE: " + stopDLEComments.stopbox_comments + Environment.NewLine;
            }

            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dle_SwitchOff();
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                textBox_Status.Text = textBox_Status.Text + "Error Occurred " + ex.Message;
                return;
            }
        }


        /// <summary>
        /// Method to compare data with Tolerance values and display in UI
        /// </summary>
        private void compareData()
        {
            //Get Tolerance Values from PI
            pitoltag.GetToleranceValuesFromPI();
            outOfTol = 0;
            
            int rowCount = dataGridView_Illuminance.RowCount - 1;
            int colCount = dataGridView_Illuminance.ColumnCount;
            string val1;
            string val2;
            double val1_double;
            double val2_double;
            int c = 0;
            string status = "";
            pitags_Sensor0.Clear();
            pitags_Sensor1.Clear();
            pitags_Sensor2.Clear();

            for (int k = 0; k < rowCount; k++)
            {
                c = 0;
                for (int i = 0; i < colCount; i++)
                {
                    if ((i != 0) && (i % 3 == 0))
                        c = c + 1;
                    switch (k)
                    {
                        //Sensor 0 data Tolerance Comparision
                        case 0://Sensor 0 Lux Tolerance Comparision
                            val1 = sensor0_LUX[i];
                            if (!string.IsNullOrEmpty(val1))
                            {
                                val1_double = Convert.ToDouble(val1);
                                status = "Lux";
                                comparision(status, val1_double, pitoltag.panelMPTSensor0LUX_LowerLimit_values[c], pitoltag.panelMPTSensor0LUX_UpperLimit_values[c], k, i);
                            }

                            //Sensor 0 CCT Tolerance Comparision
                            val2 = sensor0_CCT[i];
                            if (!string.IsNullOrEmpty(val2))
                            {
                                val2_double = Convert.ToDouble(val2);
                                status = "CCT";
                                comparision(status, val2_double, pitoltag.panelMPTSensor0CCT_LowerLimit_values[c], pitoltag.panelMPTSensor0CCT_UpperLimit_values[c], k, i);
                            }
                            break;

                        // Sensor 1 data Tolerance Comparision
                        case 1: //Sensor 1 Lux Tolerance Comparision
                            val1 = sensor1_LUX[i];
                            if (!string.IsNullOrEmpty(val1))
                            {
                                val1_double = Convert.ToDouble(val1);
                                status = "Lux";
                                comparision(status, val1_double, pitoltag.panelMPTSensor1LUX_LowerLimit_values[c], pitoltag.panelMPTSensor1LUX_UpperLimit_values[c], k, i);
                            }
                            //Sensor 1 CCT Tolerance Comparision
                            val2 = sensor1_CCT[i];
                            if (!string.IsNullOrEmpty(val2))
                            {
                                val2_double = Convert.ToDouble(sensor1_CCT[i]);
                                status = "CCT";
                                comparision(status, val2_double, pitoltag.panelMPTSensor1CCT_LowerLimit_values[c], pitoltag.panelMPTSensor1CCT_UpperLimit_values[c], k, i);
                            }
                            break;

                        // Sensor 2 data Tolerance Comparision
                        case 2: //Sensor 2 Lux Tolerance Comparision
                            val1 = sensor2_LUX[i];
                            if (!string.IsNullOrEmpty(val1))
                            {
                                val1_double = Convert.ToDouble(sensor2_LUX[i]);
                                status = "Lux";
                                comparision(status, val1_double, pitoltag.panelMPTSensor2LUX_LowerLimit_values[c], pitoltag.panelMPTSensor2LUX_UpperLimit_values[c], k, i);
                            }
                            //Sensor 2 CCT Tolerance Comparision
                            val2 = sensor2_CCT[i];
                            if (!string.IsNullOrEmpty(val2))
                            {
                                val2_double = Convert.ToDouble(sensor2_CCT[i]);
                                status = "CCT";
                                comparision(status, val2_double, pitoltag.panelMPTSensor2CCT_LowerLimit_values[c], pitoltag.panelMPTSensor2CCT_UpperLimit_values[c], k, i);
                            }
                            break;
                    }
                }
            }

            dataGridView_CCT.TabStop = false;
            dataGridView_CCT.ClearSelection();
            dataGridView_Illuminance.TabStop = false;
            dataGridView_Illuminance.ClearSelection();


            List<string[]> sensorList = new List<string[]>();
            sensorList.Add(sensor0_LUX);
            sensorList.Add(sensor0_CCT);
            sensorList.Add(sensor1_LUX);
            sensorList.Add(sensor1_CCT);
            sensorList.Add(sensor2_LUX);
            sensorList.Add(sensor2_CCT);

            // Check if any values have reading as 0 or 0.0
            List<string> valuesZero = new List<string>();
            foreach (var i in sensorList)
            {
                foreach (var j in i)
                {
                    if (j == "0" || j == "0.0")
                    {
                        valuesZero.Add(j);
                    }//valuesZero++;
                }
            }

            //UI related settings
            labelAboveTol.Visible = true;
            labelBelowTol.Visible = true;
            btnAboveTol.Visible = true;
            btnBelowTol.Visible = true;
            labelWithinTol.Visible = true;
            btnWithinTol.Visible = true;
            groupBoxLegend.Visible = true;
            btnAboveTol.BackColor = Color.OrangeRed;
            btnBelowTol.BackColor = Color.DarkOrange;
            btnWithinTol.BackColor = Color.White;

            //Switching Off DLE
            byte[] controlturnoffbyte = new DLEControl().createbytearray(new DLEControlByteConstants().header, new DLEControlByteConstants().controlturnoff);
            new DLEControl().senddpdmxmessage(controlturnoffbyte);

            //UI Related changes
            listBTN[panel.Substring(0, 7)].BackColor = Color.Gainsboro;
            btnStop.Enabled = false;

            //Display message to User about number of values out of tolerance
            if ((outOfTol > 0) || (valuesZero.Count > 0))
            {
               if (valuesZero.Count > 0)
                    MessageBox.Show(Form.ActiveForm, "There are totally " + valuesZero.Count + " values which are zero.");
                MessageBox.Show(Form.ActiveForm, "There are totally " + outOfTol + " values which are out of tolerance limit.");
            }

            //Confirm from User if data needs to be saved in PI
            DialogResult dialogResult = MessageBox.Show(Form.ActiveForm, DisplayMessages.savedata_msg, "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            List<string> sensor0_time = new List<string>();
            List<string> sensor1_time = new List<string>();
            List<string> sensor2_time = new List<string>();

            if (dialogResult == DialogResult.Yes)
            {
                this.dataGridView_CCT.Enabled = true;
                this.dataGridView_Illuminance.Enabled = true;

                for (int i = 0, j = 0; i < 21; i = i + 3, j = j + 6)
                {
                    pitags_Sensor0.Add(pitags_Array[j]);
                    pitags_Sensor0.Add(pitags_Array[j + 1]);
                    pitags_Sensor1.Add(pitags_Array[j + 2]);
                    pitags_Sensor1.Add(pitags_Array[j + 3]);
                    pitags_Sensor2.Add(pitags_Array[j + 4]);
                    pitags_Sensor2.Add(pitags_Array[j + 5]);
                }

                //Thread to save data in PI and in database
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (obj, e) => WorkerDoWork(pitags_Sensor0, pitags_Sensor1, pitags_Sensor2);
                worker.RunWorkerAsync();
                worker.RunWorkerCompleted += (obj, e) => WorkerCompleted();

                Array.Clear(sensor0_lux_copy, 0, sensor0_lux_copy.Length);
                Array.Clear(sensor0_cct_copy, 0, sensor0_cct_copy.Length);
                Array.Clear(sensor1_lux_copy, 0, sensor1_lux_copy.Length);
                Array.Clear(sensor1_cct_copy, 0, sensor1_cct_copy.Length);
                Array.Clear(sensor2_lux_copy, 0, sensor2_lux_copy.Length);
                Array.Clear(sensor2_cct_copy, 0, sensor2_cct_copy.Length);

                Array.Clear(panelMPTSensor0LUX_LowerLimit_values_copy, 0, panelMPTSensor0LUX_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor0LUX_UpperLimit_values_copy, 0, panelMPTSensor0LUX_UpperLimit_values_copy.Length);
                Array.Clear(panelMPTSensor0CCT_LowerLimit_values_copy, 0, panelMPTSensor0CCT_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor0CCT_UpperLimit_values_copy, 0, panelMPTSensor0CCT_UpperLimit_values_copy.Length);
                Array.Clear(panelMPTSensor1LUX_LowerLimit_values_copy, 0, panelMPTSensor1LUX_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor1LUX_UpperLimit_values_copy, 0, panelMPTSensor1LUX_UpperLimit_values_copy.Length);
                Array.Clear(panelMPTSensor1CCT_LowerLimit_values_copy, 0, panelMPTSensor1CCT_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor1CCT_UpperLimit_values_copy, 0, panelMPTSensor1CCT_UpperLimit_values_copy.Length);
                Array.Clear(panelMPTSensor2LUX_LowerLimit_values_copy, 0, panelMPTSensor2LUX_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor2LUX_UpperLimit_values_copy, 0, panelMPTSensor2LUX_UpperLimit_values_copy.Length);
                Array.Clear(panelMPTSensor2CCT_LowerLimit_values_copy, 0, panelMPTSensor2CCT_LowerLimit_values_copy.Length);
                Array.Clear(panelMPTSensor2CCT_UpperLimit_values_copy, 0, panelMPTSensor2CCT_UpperLimit_values_copy.Length);

                //Copy values from sensor lux and cct arrays to temporary array
                Array.Copy(sensor0_LUX, sensor0_lux_copy, sensor0_LUX.Length);
                Array.Copy(sensor0_CCT, sensor0_cct_copy, sensor0_CCT.Length);
                Array.Copy(sensor1_LUX, sensor1_lux_copy, sensor1_LUX.Length);
                Array.Copy(sensor1_CCT, sensor1_cct_copy, sensor1_CCT.Length);
                Array.Copy(sensor2_LUX, sensor2_lux_copy, sensor2_LUX.Length);
                Array.Copy(sensor2_CCT, sensor2_cct_copy, sensor2_CCT.Length);

                //Copy panel upper and lower limit values to temporary array
                Array.Copy(pitoltag.panelMPTSensor0LUX_LowerLimit_values, panelMPTSensor0LUX_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor0LUX_UpperLimit_values, panelMPTSensor0LUX_UpperLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor0CCT_LowerLimit_values, panelMPTSensor0CCT_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor0CCT_UpperLimit_values, panelMPTSensor0CCT_UpperLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor1LUX_LowerLimit_values, panelMPTSensor1LUX_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor1LUX_UpperLimit_values, panelMPTSensor1LUX_UpperLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor1CCT_LowerLimit_values, panelMPTSensor1CCT_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor1CCT_UpperLimit_values, panelMPTSensor1CCT_UpperLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor2LUX_LowerLimit_values, panelMPTSensor2LUX_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor2LUX_UpperLimit_values, panelMPTSensor2LUX_UpperLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor2CCT_LowerLimit_values, panelMPTSensor2CCT_LowerLimit_values_copy, 7);
                Array.Copy(pitoltag.panelMPTSensor2CCT_UpperLimit_values, panelMPTSensor2CCT_UpperLimit_values_copy, 7);


                /*
                //Thread to insert out of tolerance values
                BackgroundWorker worker1 = new BackgroundWorker();
                worker1.DoWork += (obj, e) => OutOfTolDataCheck(pitags_Sensor0, pitags_Sensor1, pitags_Sensor2);
                worker1.RunWorkerAsync();
                */

                OutOfTolDataCheck(pitags_Sensor0, pitags_Sensor1, pitags_Sensor2);

            }
            else
            {
                textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Data has not been saved to PI.";
                //If panels data has been previously saved , retain the Forest Green colour to indicate that data has been saved once
                if (panel_datasaved == true)
                {
                    locationLabel[panel].ForeColor = Color.ForestGreen;//Change colour if data previously taken for the panel
                    locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Bold);
                }
                else //If panels data has not been previously saved , change colour back to black
                {
                    locationLabel[panel].ForeColor = Color.Black;//Change colour if not complete
                    locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Regular);
                }
            }

            //Switch off DLE
            dle_SwitchOff();

            //Check if user wants to enter any comments
            DialogResult result2 = MessageBox.Show(DisplayMessages.entercomments_afterdlerun, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result2 == DialogResult.Yes)
            {
                Program.comments = "";
                Comments ab = new Comments(usercomments_Tag, "Enter Comments", db_connect.PiServer, 0);
                ab.ShowDialog();
                if (Program.comments != "")
                {
                    textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Comments Entered By User: ";
                    textBox_Status.Text = textBox_Status.Text + Program.comments + Environment.NewLine;
                }
            }

        }

        /// <summary>
        /// Method to save data in PI and mysql database
        /// </summary>
        /// <param name="pitags_Sensor0"></param>
        /// <param name="pitags_Sensor1"></param>
        /// <param name="pitags_Sensor2"></param>
        public void WorkerDoWork(List<string> pitags_Sensor0, List<string> pitags_Sensor1, List<string> pitags_Sensor2)
        {
            int rowCount = dataGridView_Illuminance.RowCount - 1;
            int colCount = dataGridView_Illuminance.ColumnCount;
            string time = "";
            string avg_sensor0_lux;
            string avg_sensor0_cct;
            string avg_sensor1_lux;
            string avg_sensor1_cct;
            string avg_sensor2_lux;
            string avg_sensor2_cct;
            List<string> sensor0_time = new List<string>();
            List<string> sensor1_time = new List<string>();
            List<string> sensor2_time = new List<string>();

            //Save data in PI
            for (int i = 0, j = 0; i < 21; i = i + 3, j = j + 6)
            {
                //Get pitags for the Lux and CCT values
                string[] s1 = new string[] { pitags_Array[j], pitags_Array[j + 1] };
                //Average of 3 sets of values for Lux and CCT
                avg_sensor0_lux = ((Convert.ToDouble(sensor0_LUX[i]) + Convert.ToDouble(sensor0_LUX[i + 1]) + Convert.ToDouble(sensor0_LUX[i + 2])) / 3).ToString();
                avg_sensor0_cct = ((Convert.ToDouble(sensor0_CCT[i]) + Convert.ToDouble(sensor0_CCT[i + 1]) + Convert.ToDouble(sensor0_CCT[i + 2])) / 3).ToString();
                //Store the average Lux and CCT value 
                konikaValues = new string[] { avg_sensor0_lux, avg_sensor0_cct };
                time = DateTime.Now.ToString();
                //Update konica values to PI
                PIUpdate.UpdatePI(s1, konikaValues, db_connect.PiServer, time);
                //Add average values of lux and cct to List
                avg_Sensor0_LUX.Add(avg_sensor0_lux);
                avg_Sensor0_CCT.Add(avg_sensor0_cct);
                //Add sensor time to List
                sensor0_time.Add(time);

                string[] s2 = new string[] { pitags_Array[j + 2], pitags_Array[j + 3] };
                avg_sensor1_lux = ((Convert.ToDouble(sensor1_LUX[i]) + Convert.ToDouble(sensor1_LUX[i + 1]) + Convert.ToDouble(sensor1_LUX[i + 2])) / 3).ToString();
                avg_sensor1_cct = ((Convert.ToDouble(sensor1_CCT[i]) + Convert.ToDouble(sensor1_CCT[i + 1]) + Convert.ToDouble(sensor1_CCT[i + 2])) / 3).ToString();
                konikaValues = new string[] { avg_sensor1_lux, avg_sensor1_cct };
                time = DateTime.Now.ToString();
                PIUpdate.UpdatePI(s2, konikaValues, db_connect.PiServer, time);
                avg_Sensor1_LUX.Add(avg_sensor1_lux);
                avg_Sensor1_CCT.Add(avg_sensor1_cct);
                sensor1_time.Add(time);

                string[] s3 = new string[] { pitags_Array[j + 4], pitags_Array[j + 5] };
                avg_sensor2_lux = ((Convert.ToDouble(sensor2_LUX[i]) + Convert.ToDouble(sensor2_LUX[i + 1]) + Convert.ToDouble(sensor2_LUX[i + 2])) / 3).ToString();
                avg_sensor2_cct = ((Convert.ToDouble(sensor2_CCT[i]) + Convert.ToDouble(sensor2_CCT[i + 1]) + Convert.ToDouble(sensor2_CCT[i + 2])) / 3).ToString();
                konikaValues = new string[] { avg_sensor2_lux, avg_sensor2_cct };
                time = DateTime.Now.ToString();
                PIUpdate.UpdatePI(s3, konikaValues, db_connect.PiServer, time);//,sensor2_time[i + 2]);
                avg_Sensor2_LUX.Add(avg_sensor2_lux);
                avg_Sensor2_CCT.Add(avg_sensor2_cct);
                sensor2_time.Add(time);
            }

            //Get panel id
            try
            {
                db_connect.GetPanelID(panel);
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }


            //Get DLE Scenarios ID
            try
            {
                db_connect.GetDLEScenariosID();
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Insert values in database for Sensor 0 - Lux and CCT 
            try
            {
                db_connect.InsertDLEValues(avg_Sensor0_LUX, avg_Sensor0_CCT, pitags_Sensor0, sensor0_time, 0);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Insert values in database for Sensor 1 - Lux and CCT
            try
            {
                db_connect.InsertDLEValues(avg_Sensor1_LUX, avg_Sensor1_CCT, pitags_Sensor1, sensor1_time, 7);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Insert values in database for Sensor 2 - Lux and CCT
            try
            {
                db_connect.InsertDLEValues(avg_Sensor2_LUX, avg_Sensor2_CCT, pitags_Sensor2, sensor2_time, 14);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
            workdone = 1;
        }



        /// <summary>
        /// Method called when background worker has completed saving data to PI and database
        /// </summary>
        public void WorkerCompleted()
        {
            int panel_Num = panelDetails[panel] - 1;

            //If data has been successfully saved to PI and database , workdone = 1
            if (workdone == 1)
            {
                //Change colour of location label to Forest Green if data has  been saved to PI
                locationLabel[panel].ForeColor = Color.ForestGreen;
                locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Bold);
                textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Data has been saved to PI.";
                panelList_SaveCount[panel_Num] = panelList_SaveCount[panel_Num] + 1;
                int panelsSave_Count = 0;

                //Checking to see the number of panels for which data has been saved
                foreach (var i in panelList_SaveCount)
                {
                    if (i != 0)
                        panelsSave_Count = panelsSave_Count + 1;
                }

                //Update panel for which data has been saved in database
                try
                {
                    db_connect.UpdatePanelSaveCountDetails(panelsSave_Count);
                }
                catch (MySqlException mysqlex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                    return;
                }
            }
            else
            {
                //Change colour of location label back to black if data has not been saved to PI
                locationLabel[panel].ForeColor = Color.Black;
                locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Regular);
            }
        }


       

        /// <summary>
        /// Method to check if values are out of tolerance
        /// </summary>
        /// <param name="pitags_Sensor0"></param>
        /// <param name="pitags_Sensor1"></param>
        /// <param name="pitags_Sensor2"></param>
        public void OutOfTolDataCheck(List<string> pitags_Sensor0, List<string> pitags_Sensor1, List<string> pitags_Sensor2)
        {
            lux.Clear(); cct.Clear();

            //Adding lux position along with tag in Dictionary
            for (int i =0,j=0;i<7;i++,j=j+2)
                lux.Add("0"+i.ToString(),pitags_Sensor0[j]);

            for (int i = 0, j = 0; i < 7; i++, j = j + 2)
                lux.Add("1" + i.ToString(), pitags_Sensor1[j]);

            for (int i = 0, j = 0; i < 7; i++, j = j + 2)
                lux.Add("2" + i.ToString(), pitags_Sensor2[j]);


            //Adding cct position along with tag in Dictionary
            for (int i = 0, j = 1; i < 7; i++, j = j + 2)
                cct.Add("0" + i.ToString(), pitags_Sensor0[j]);

            for (int i = 0, j = 1; i < 7; i++, j = j + 2)
                cct.Add("1" + i.ToString(), pitags_Sensor1[j]);

            for (int i = 0, j = 1; i < 7; i++, j = j + 2)
                cct.Add("2" + i.ToString(), pitags_Sensor2[j]);

            int rowCount = dataGridView_Illuminance.RowCount - 1;
            int colCount = dataGridView_Illuminance.ColumnCount;
            int l = 0, c; // c = 0;
            string val1;
            string val2;
            double val1_double;
            double val2_double;

            for (int k = 0; k < rowCount; k++)
            {
                c = 0; l = 0;
                for (int i = 0; i < colCount; i++)
                {
                    if ((i != 0) && (i % 3 == 0))
                    {
                        c = c + 1; l = l + 2;
                    }
                    switch (k)
                    {
                               //Sensor 0 data Tolerance Comparision
                        case 0://Sensor 0 Lux Tolerance Comparision
                                val1 = sensor0_lux_copy[i];        
                                if (!string.IsNullOrEmpty(val1))
                                {
                                val1_double = Convert.ToDouble(val1);
                                insertOutOfTolDataInDatabase(pitags_Sensor0[l], val1_double, panelMPTSensor0LUX_LowerLimit_values_copy[c], panelMPTSensor0LUX_UpperLimit_values_copy[c]);// DLEToleranceData.panelMPTSensor0LUX_UpperLimit_values[c]);
                                }

                                //Sensor 0 CCT Tolerance Comparision
                                val2 = sensor0_cct_copy[i];
                                if (!string.IsNullOrEmpty(val2))
                                {
                                val2_double = Convert.ToDouble(val2);
                                insertOutOfTolDataInDatabase(pitags_Sensor0[l + 1], val2_double, panelMPTSensor0CCT_LowerLimit_values_copy[c], panelMPTSensor0CCT_UpperLimit_values_copy[c]);
                                }
                                break;

                                // Sensor 1 data Tolerance Comparision
                        case 1: //Sensor 1 Lux Tolerance Comparision
                                val1 = sensor1_lux_copy[i];
                                if (!string.IsNullOrEmpty(val1))
                                {
                                val1_double = Convert.ToDouble(val1);
                                insertOutOfTolDataInDatabase(pitags_Sensor1[l], val1_double, panelMPTSensor1LUX_LowerLimit_values_copy[c], panelMPTSensor1LUX_UpperLimit_values_copy[c]);
                                }
                                //Sensor 1 CCT Tolerance Comparision
                                val2 = sensor1_cct_copy[i];
                                if (!string.IsNullOrEmpty(val2))
                                {
                                val2_double = Convert.ToDouble(val2);
                                insertOutOfTolDataInDatabase(pitags_Sensor1[l + 1], val2_double, panelMPTSensor1CCT_LowerLimit_values_copy[c], panelMPTSensor1CCT_UpperLimit_values_copy[c]);
                                }
                                break;

                                // Sensor 2 data Tolerance Comparision
                        case 2: //Sensor 2 Lux Tolerance Comparision
                                val1 = sensor2_lux_copy[i];
                                if (!string.IsNullOrEmpty(val1))
                                {
                                val1_double = Convert.ToDouble(val1);
                                insertOutOfTolDataInDatabase(pitags_Sensor2[l], val1_double, panelMPTSensor2LUX_LowerLimit_values_copy[c], panelMPTSensor2LUX_UpperLimit_values_copy[c]);
                                }
                                //Sensor 2 CCT Tolerance Comparision
                                val2 = sensor2_cct_copy[i];
                                if (!string.IsNullOrEmpty(val2))
                                {
                                val2_double = Convert.ToDouble(val2);
                                insertOutOfTolDataInDatabase(pitags_Sensor2[l + 1], val2_double, panelMPTSensor2CCT_LowerLimit_values_copy[c], panelMPTSensor2CCT_UpperLimit_values_copy[c]);
                                }
                                break;
                    }
                }
            }
        }



        /// <summary>
        /// Method to compare data with Upper and Lower Tolerance limit
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        /// <param name="lowerlimit"></param>
        /// <param name="upperlimit"></param>
        /// <param name="k"></param>
        /// <param name="i"></param>
        private void comparision(string state, double val, double lowerlimit, double upperlimit, int k, int i)
        {
            //Checking if value is greater than upper limit
            if (val > (upperlimit))
            {
                if (state == "CCT")
                    dataGridView_CCT.Rows[k].Cells[i].Style.BackColor = Color.OrangeRed;
                else
                    dataGridView_Illuminance.Rows[k].Cells[i].Style.BackColor = Color.OrangeRed;
                outOfTol++;
            }

            //Checking if value is lesser than lower limit
            else if (val < lowerlimit)
            {
                if (state == "CCT")
                    dataGridView_CCT.Rows[k].Cells[i].Style.BackColor = Color.DarkOrange;
                else
                    dataGridView_Illuminance.Rows[k].Cells[i].Style.BackColor = Color.DarkOrange;
                outOfTol++;  
            }

            else
                return;
    
        }



        /// <summary>
        /// Method to insert out of tolerance data in database
        /// </summary>
        /// <param name="pitag"></param>
        /// <param name="val"></param>
        /// <param name="lowerlimit"></param>
        /// <param name="upperlimit"></param>
        private void insertOutOfTolDataInDatabase(string pitag, double val, double lowerlimit, double upperlimit)
        {
            if (val > (upperlimit))
            {
                //Updating database with Out of Tolerance values when values are more than tolerance limit
                try
                {
                    db_connect.InsertOutOfTolVal(pitag, val.ToString(), "Value Above Tolerance", upperlimit.ToString(), lowerlimit.ToString());
                }
                catch (MySqlException mysqlex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                    return;
                }
            }
            else if (val < lowerlimit)
            {
                //Updating database with Out of Tolerance values when values are less than tolerance limit
                try
                {
                    db_connect.InsertOutOfTolVal(pitag, val.ToString(), "Value Below Tolerance", upperlimit.ToString(), lowerlimit.ToString());
                }
                catch (MySqlException mysqlex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                    return;
                }
            }
            else
                return;
        }




        /// <summary>
        /// This method is used to Load the sensor data in Tables in UI.
        /// </summary>
        /// <param name="count"></param>
        private void loadData(int count)
        {
            dataGridView_CCT.SuspendLayout();
            dataGridView_Illuminance.SuspendLayout();
            dataGridView_Illuminance.DataSource = null;
            dataGridView_Illuminance.Update();
            dataGridView_Illuminance.Refresh();
            dataGridView_CCT.DataSource = null;
            dataGridView_CCT.Update();
            dataGridView_CCT.Refresh();

            //For filling data at all other times other than the first time in tables(gridview)
            if (count == 1)
            {
                dataGridView_Illuminance.Rows.RemoveAt(0);
                dataGridView_Illuminance.Rows.Insert(0, sensor0_LUX);
                dataGridView_Illuminance.Rows.RemoveAt(1);
                dataGridView_Illuminance.Rows.Insert(1, sensor1_LUX);
                dataGridView_Illuminance.Rows.RemoveAt(2);
                dataGridView_Illuminance.Rows.Insert(2, sensor2_LUX);
                dataGridView_CCT.Rows.RemoveAt(0);
                dataGridView_CCT.Rows.Insert(0, sensor0_CCT);
                dataGridView_CCT.Rows.RemoveAt(1);
                dataGridView_CCT.Rows.Insert(1, sensor1_CCT);
                dataGridView_CCT.Rows.RemoveAt(2);
                dataGridView_CCT.Rows.Insert(2, sensor2_CCT);
            }
            //For filling the data for the first time in tables (gridview)
            else
            {
                dataGridView_Illuminance.Rows.Add(sensor0_LUX);
                dataGridView_Illuminance.Rows.Add(sensor1_LUX);
                dataGridView_Illuminance.Rows.Add(sensor2_LUX);
                dataGridView_CCT.Rows.Add(sensor0_CCT);
                dataGridView_CCT.Rows.Add(sensor1_CCT);
                dataGridView_CCT.Rows.Add(sensor2_CCT);
            }

            dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dataGridView_Illuminance.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            dataGridView_Illuminance.DefaultCellStyle.Font = new System.Drawing.Font(dataGridView_Illuminance.DefaultCellStyle.Font, FontStyle.Bold);
            dataGridView_CCT.DefaultCellStyle.Font = new System.Drawing.Font(dataGridView_CCT.DefaultCellStyle.Font, FontStyle.Bold);

            dataGridView_Illuminance.ResumeLayout();
            dataGridView_CCT.ResumeLayout();
        }



        /// <summary>
        /// Background Thread to fetch values from Konica device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            err_Flag = 0;
            if (backgroundWorker1.CancellationPending == false)
            {
                switch (state)
                {
                    case "Warm-16": Thread.Sleep(6000); sensorData_Location = 0; //piTag_Location = 0;//c=0
                        break;
                    case "Warm-64": Thread.Sleep(2000); sensorData_Location = 3;// piTag_Location = 6;//c=1
                        break;
                    case "Warm-128": Thread.Sleep(2000); sensorData_Location = 6;// piTag_Location = 12;//c=2
                        break;
                    case "Cool-16": Thread.Sleep(6000); sensorData_Location = 9; //piTag_Location = 18;//c=3
                        break;
                    case "Cool-64": Thread.Sleep(2000); sensorData_Location = 12; //piTag_Location = 24;//c=4
                        break;
                    case "Cool-128": Thread.Sleep(2000); sensorData_Location = 15;// piTag_Location = 30;//c=5
                        break;
                    case "Combined-64": Thread.Sleep(2000); sensorData_Location = 18;//piTag_Location = 36;//c=6 
                        break;
                }

                if (backgroundWorker1.CancellationPending == false)
                {
                    try
                    {
                        for (int k = 0; k < 3; k++, sensorData_Location++)
                        {
                            if ((k == 0) && (sensorData_Location == 0))
                                multipleSensors.StartReadingData(0);                        //Value 0 is sent when reading data for the first time from the Konica device. When reading for the first time , all the commands have to be sent to the Konica device
                            else
                                multipleSensors.StartReadingData(1);                        //Value 1 is sent at all other times other than the first time that data is read from the device. Only commands relating to fetching data from the device are sent here

                            sensor0_LUX[sensorData_Location] = multipleSensors.luxcct_Values[0, 0];
                            sensor0_CCT[sensorData_Location] = multipleSensors.luxcct_Values[0, 1];

                            sensor1_LUX[sensorData_Location] = multipleSensors.luxcct_Values[1, 0];
                            sensor1_CCT[sensorData_Location] = multipleSensors.luxcct_Values[1, 1];

                            sensor2_LUX[sensorData_Location] = multipleSensors.luxcct_Values[2, 0];
                            sensor2_CCT[sensorData_Location] = multipleSensors.luxcct_Values[2, 1];
                        }

                        if (backgroundWorker1.CancellationPending == true)
                        {
                            err_Flag = 1;
                            return;
                        }
                    }

                    catch (KonicaErrorException ke1)
                    {
                        MessageBox.Show(DisplayMessages.konica_error + ke1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(DisplayMessages.comport_check);
                        error_Message = "Error Occurred : " + ke1.Message + Environment.NewLine;
                        Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ke1.GetType().ToString() + ", Target Site : " + ke1.TargetSite + ",\tError Message : " + ke1.Message + ", Inner Exception : " + ke1.InnerException);
                        err_Flag = 1;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(DisplayMessages.comport_check);
                        error_Message = "Error Occured: " + ex.Message + Environment.NewLine;
                        Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                        err_Flag = 1;
                        return;
                    }
                }
                else
                {
                    e.Cancel = true;
                    err_Flag = 1;
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                err_Flag = 1;
                return;
            }
        }


        /// <summary>
        /// Method called when background thread has completed work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Writing error message captured in Do Work method to Status textbox
            if (error_Message.Length > 0)
            {
                textBox_Status.Text = textBox_Status.Text + error_Message + Environment.NewLine;
                error_Message = "";
            }
            //Checking if background worker has been cancelled
            if (backgroundWorker1.CancellationPending == true)
            {
                err_Flag = 1;
                return;
            }
            //Switch off DLE incase of any error
            if (err_Flag == 1)
            {
                dle_SwitchOff();
                return;
            }

            try
            {
                //Change to next DLE setting
                switch (state)
                {
                    case "Warm-16": loadData(0);
                        dle_Running("Warm-64", panel, controlPanel_data[1]);
                        break;
                    case "Warm-64": loadData(1);
                        dle_Running("Warm-128", panel, controlPanel_data[2]);
                        break;
                    case "Warm-128": loadData(1);
                        dle_Running("Cool-16", panel, controlPanel_data[3]);
                        break;
                    case "Cool-16": loadData(1);
                        dle_Running("Cool-64", panel, controlPanel_data[4]);
                        break;
                    case "Cool-64": loadData(1);
                        dle_Running("Cool-128", panel, controlPanel_data[5]);
                        break;
                    case "Cool-128": loadData(1);
                        dle_Running("Combined-64", panel, controlPanel_data[6]);
                        break;
                    case "Combined-64": loadData(1);
                        compareData();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_Status.Text = textBox_Status.Text + error_Message + Environment.NewLine;
                dle_SwitchOff();
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Check if User has right clicked on any cell in datagridview_Illuminanace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_Illuminance_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView_Illuminance.AllowUserToResizeRows = false;
                dataGridView_Illuminance.AllowUserToResizeColumns = false;
                //Clear selection of cell which has been clicked in datagridview_Illuminanace
                if (this.dataGridView_Illuminance.SelectedCells.Count >= 0)
                {
                    foreach (DataGridViewCell c in this.dataGridView_Illuminance.SelectedCells)
                    {
                        c.Selected = false;
                    }
                }
                //Clear selection of cell which has been clicked in datagridview_CCT
                if (this.dataGridView_CCT.SelectedCells.Count >= 0)
                {
                    foreach (DataGridViewCell c in this.dataGridView_CCT.SelectedCells)
                    {
                        c.Selected = false;
                    }
                }
                //Right click mouse button check
                if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0))
                {
                    dataGridView_Illuminance.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    if (e.RowIndex != -1)
                    {
                       
                        if (tags.Contains(lux[(e.RowIndex.ToString() + row[e.ColumnIndex])]))
                        {
                            MessageBox.Show("Annotation has been entered for this tag");
                            return;
                        }
                        else
                        {
                            //tags.Add(lux[(e.RowIndex.ToString() + row[e.ColumnIndex])]);
                            AnnotatePITag annotate = new AnnotatePITag(lux[(e.RowIndex.ToString() + row[e.ColumnIndex])], db_connect.PiServer);
                            annotate.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : Program.cs " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }   
             
        }



        /// <summary>
        /// Check if User has right clicked on any cell in datagridview_CCT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CCT_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView_CCT.AllowUserToResizeRows = false;
                dataGridView_CCT.AllowUserToResizeColumns = false;
                //Clear selection of cell which has been clicked in datagridview_CCT
                if (this.dataGridView_CCT.SelectedCells.Count >= 0)
                {
                    foreach (DataGridViewCell c in this.dataGridView_CCT.SelectedCells)
                    {
                        c.Selected = false;
                    }
                }
                //Clear selection of cell which has been clicked in datagridview_Illuminanace
                if (this.dataGridView_Illuminance.SelectedCells.Count >= 0)
                {
                    foreach (DataGridViewCell c in this.dataGridView_CCT.SelectedCells)
                    {
                        c.Selected = false;
                    }
                }
                //Right click mouse button check
                if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0))
                {
                    dataGridView_CCT.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    if (e.RowIndex != -1)
                    {
                        if (tags.Contains(cct[(e.RowIndex.ToString() + row[e.ColumnIndex])]))
                        {
                            MessageBox.Show("Annotation has been entered for this tag");
                            return;
                        }
                        else
                        {
                            //tags.Add(cct[(e.RowIndex.ToString() + row[e.ColumnIndex])]);
                            AnnotatePITag annotate = new AnnotatePITag(cct[(e.RowIndex.ToString() + row[e.ColumnIndex])], db_connect.PiServer);
                            annotate.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : Program.cs " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }   
        }
        


        /// <summary>
        /// Method called when textbox status is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Status_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Write to logs file
                txt = new StreamWriter(logs_FilePath_WithFileName);
                txt.Write(textBox_Status.Text);
                txt.Close();
                ((System.Windows.Forms.TextBox)sender).SelectionLength = 0;
                this.textBox_Status.SelectionStart = this.textBox_Status.Text.Length;
                this.textBox_Status.DeselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Method to switch off DLE
        /// </summary>
        private void dle_SwitchOff()
        {
            try
            {
                //Control byte to switch off DLE
                byte[] controlturnoffbyte = new DLEControl().createbytearray(new DLEControlByteConstants().header, new DLEControlByteConstants().controlturnoff);
                new DLEControl().senddpdmxmessage(controlturnoffbyte);
            }
            catch (MySqlException mysqlex)
            {
                 MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                 err_Flag = 1;
                 //return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                err_Flag = 1;
                //return;
            }
            try   
            {
                //UI realted settings
                listBTN[panel.Substring(0, 7)].BackColor = Color.Gainsboro;
                btnDLESetting.Visible = false;
                labelDLESetting.Text = "";

                if (err_Flag == 1)
                {
                    if (panel_datasaved == true)
                    {
                        locationLabel[panel].ForeColor = Color.ForestGreen;//Change colour if data previously taken for the panel
                        locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Bold);
                    }
                    else
                    {
                        locationLabel[panel].ForeColor = Color.Black;//Change colour if data has not been taen for the panel
                        locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Regular);
                    }
                }
                comboBox_PanelNumbers.Enabled = true;
                radioButton_Left.Enabled = true;
                radioButton_Right.Enabled = true;
                labelStopTimeValue.Text = DateTime.Now.ToString("hh:mm:ss tt");
                DateTime startTime = Convert.ToDateTime(labelStartTimeValue.Text);
                DateTime endTime = Convert.ToDateTime(labelStopTimeValue.Text);
                TimeSpan duration = DateTime.Parse(endTime.ToString()).Subtract(DateTime.Parse(startTime.ToString()));
                totalTime = totalTime.Add(duration);
                labelTotalTimeValue.Text = totalTime.ToString();
                if (timeTakenLabel[panel].Text != "")
                {
                    TimeSpan t1 = TimeSpan.Parse(timeTakenLabel[panel].Text);
                    TimeSpan t2 = t1.Add(duration);
                    timeTakenLabel[panel].Text = t2.ToString();
                }
                else
                    timeTakenLabel[panel].Text = duration.ToString();
                comboBox_PanelNumbers.Enabled = true;
                radioButton_Left.Enabled = true;
                radioButton_Right.Enabled = true;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnExit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Update panel run count for all panels in database
        /// </summary>
        private void updatePanelRuncount()
        {
            panelList_RunCount[panelDetails[panel] - 1] = panelList_RunCount[panelDetails[panel] - 1] + 1;
            int panelsRun_Count = 0;
            foreach (var i in panelList_RunCount)
            {
                if (i != 0)
                    panelsRun_Count = panelsRun_Count + 1;
            }
            //Update number of panels which have been run irrespective of whether data has been taken for the panel or not
            try
            {
                db_connect.UpdatePanelRunCountDetails(panelsRun_Count, panelList_RunCount);
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }



        /// <summary>
        /// Open Tolerance File click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToleranceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp;
            Workbook wb;
            try
            {
                xlsApp = new Microsoft.Office.Interop.Excel.Application();
                Workbooks wbs = xlsApp.Workbooks;

                //Open Tolerance file and display in Read Only mode
                wb = wbs.Open(tol_FileLoc, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlsApp.Visible = true;
            }
            catch (COMException com_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.tolerancefilelocation_incorrect + com_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + com_ex.GetType().ToString() + ", Target Site : " + com_ex.TargetSite + ",\tError Message : " + com_ex.Message + ", Inner Exception : " + com_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException); 
                return;
            }
        }

        /// <summary>
        /// Exit menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Trends form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SQCChartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TrendsForm sqcCharts_form = new TrendsForm())
            {
                sqcCharts_form.StartPosition = FormStartPosition.CenterScreen;
                sqcCharts_form.ShowDialog();
            }
        }


        /// <summary>
        /// History form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HistoryForm history_form = new HistoryForm())
            {
                history_form.StartPosition = FormStartPosition.CenterScreen;
                history_form.ShowDialog();
            }
        }


        /// <summary>
        /// PI Settings form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pISettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PISettingsForm piSettings_form = new PISettingsForm())
            {
                piSettings_form.StartPosition = FormStartPosition.CenterScreen;
                piSettings_form.ShowDialog();
            }
        }


        /// <summary>
        /// Serial port settings form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPortSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SerialPortSettingsForm serialportSettings_form = new SerialPortSettingsForm())
            {
                serialportSettings_form.StartPosition = FormStartPosition.CenterScreen;
                serialportSettings_form.ShowDialog();
            }
        }


        /// <summary>
        /// Artnet settings form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aRTNetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ARTNetSettingsForm artnetsettings_form = new ARTNetSettingsForm())
            {
                artnetsettings_form.StartPosition = FormStartPosition.CenterScreen;
                artnetsettings_form.ShowDialog();
            }
        }


        /// <summary>
        /// Log File location form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LogFileLocationForm logfilesettings_form = new LogFileLocationForm())
            {
                logfilesettings_form.StartPosition = FormStartPosition.CenterScreen;
                logfilesettings_form.ShowDialog();
            }
        }


        /// <summary>
        /// SMTP Settings form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smtpSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SMTPSettingsForm smtpSettings_form = new SMTPSettingsForm())
            {
                smtpSettings_form.StartPosition = FormStartPosition.CenterScreen;
                smtpSettings_form.ShowDialog();
            }
        }


        /// <summary>
        /// Email Recipient form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailRecipientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (EmailRecipientsForm emailrecipient_form = new EmailRecipientsForm())
            {
                emailrecipient_form.StartPosition = FormStartPosition.CenterScreen;
                emailrecipient_form.ShowDialog();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TestEmailForm testemail_form = new TestEmailForm())
            {
                testemail_form.StartPosition = FormStartPosition.CenterScreen;
                testemail_form.ShowDialog();
            }
        }

        /// <summary>
        /// Users Form call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UsersForm viewusers_form = new UsersForm())
            {
                viewusers_form.StartPosition = FormStartPosition.CenterScreen;
                viewusers_form.ShowDialog();
            }
        }


        /// <summary>
        /// Create New User Form Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Users_CreateNewUserForm createuser_form = new Users_CreateNewUserForm())
            {
                createuser_form.StartPosition = FormStartPosition.CenterScreen;
                createuser_form.ShowDialog();
            }
        }


        /// <summary>
        /// User Profile Form Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userProfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (UserProfileForm userprof_form = new UserProfileForm())
            {
                userprof_form.StartPosition = FormStartPosition.CenterScreen;
                userprof_form.ShowDialog();
            }
        }


        /// <summary>
        /// About Menu Form Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm abt_form = new AboutForm();
            abt_form.StartPosition = FormStartPosition.CenterScreen;
            abt_form.Show();
        }



        /// <summary>
        /// Method called when btn Exit is clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                //session_status = false;
                message = String.Empty;
                panelsSaveData.Clear();
                MyTimer.Stop();
                MyTimer.Enabled = false;

                //Store the panels for which data has been saved
                for (int i = 1; i <= panelList_SaveCount.Length; i++)
                {
                    if (panelList_SaveCount[i - 1] > 0)
                    {
                        if (!panelsSaveData.Contains(panelDetails.FirstOrDefault(x => x.Value == i).Key)) //get key from value
                            panelsSaveData.Add(panelDetails.FirstOrDefault(x => x.Value == i).Key);
                    }
                }
                //Store in variable message - panels for which data has not been saved
                foreach (var i in panelDetails.Keys)
                {
                    if (!panelsSaveData.Contains(i))
                        message = message + Environment.NewLine + i.Substring(0, 7) + " " + i.Substring(7);
                }

                //Display message to confirm from user if user wants to exit session
                DialogResult dialogResult = new DialogResult();
                if (!String.IsNullOrEmpty(message))
                    dialogResult = MessageBox.Show("Data has not been taken for:" + Environment.NewLine + message + Environment.NewLine + Environment.NewLine + DisplayMessages.session_exit, "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    dialogResult = MessageBox.Show(DisplayMessages.session_exit, "Confirm ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (dialogResult == DialogResult.Yes)
                {
                    //Check if report sending for previous sesssion is in progress
                    if (thread_ChkPrevSessionRepSent != null)
                    {
                        if (thread_ChkPrevSessionRepSent.IsAlive)
                        {
                            MessageBox.Show(Form.ActiveForm, DisplayMessages.prevsession_reportsending);
                            return;
                        }
                    }
                    btnExit_updates(false);
                }
            }
            catch (Exception ex)
            {  
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Updates done when btn Exit is clicked on
        /// </summary>
        /// <param name="auto_exit"></param>
        void btnExit_updates(bool auto_exit)
        {
            //Checking if background thread is getting data from Konica device
            if (backgroundWorker1 != null)
            {
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
            }

            labelSessionEndTimeData.Text = DateTime.Now.ToString();
            temp = "end";

            //Update session details - session end time , number of panels run
            try
            {
                db_connect.UpdateSessionDetailsOnExit(labelSessionEndTimeData.Text, panelList_RunCount);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                //return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                //return;
            }

            //Checking if session has been auto exited
            if (auto_exit == false)
            {   
                //Display message to user if no data has been saved in database
                if (panelsSaveData.Count == 0)
                {
                        //Display message box to enter comments as to why the session was simply opened and closed.
                        Program.comments = "";
                        Comments comment = new Comments(usercomments_Tag, DisplayMessages.comments_nopanelrun, db_connect.PiServer, 1);
                        comment.ShowDialog();
                        if (Program.comments != "")
                        {
                            textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Comments Entered By User : ";
                            textBox_Status.Text = textBox_Status.Text + Program.comments + Environment.NewLine;
                        }  
                }
                else
                {
                    //Send tolerance report for the session
                    tolchk.reportDetails(db_connect.CurrentSession_ID, temp);
                }
            }
            //If auto-exit is true
            else 
            {
                    //Control byte to switch off DLE
                    byte[] controlturnoffbyte = new DLEControl().createbytearray(new DLEControlByteConstants().header, new DLEControlByteConstants().controlturnoff);
                    new DLEControl().senddpdmxmessage(controlturnoffbyte);

                    //UI realted settings
                    if(panel!="")
                        listBTN[panel.Substring(0, 7)].BackColor = Color.Gainsboro;
                    btnDLESetting.Visible = false;
                    labelDLESetting.Text = "";
                    if (err_Flag == 1)
                    {
                        if (panel_datasaved == true)
                        {
                            locationLabel[panel].ForeColor = Color.ForestGreen;//Change colour if data previously saved for the panel
                            locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Bold);
                        }
                        else
                        {
                            locationLabel[panel].ForeColor = Color.Black;//Change colour if data has not been saved for the panel
                            locationLabel[panel].Font = new System.Drawing.Font(locationLabel[panel].Font, FontStyle.Regular);
                        }
                    }
                    //Update status log that session has been auto exited
                    textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Session has been Auto Exited after " + (session_timeout/60) + " mins  " + Environment.NewLine;
              
                if (panelsSaveData.Count != 0)
                {
                    //Send tolerance report for the session
                    tolchk.reportDetails(db_connect.CurrentSession_ID, "auto_exit");
                }
            }

            comboBox_PanelNumbers.Enabled = true;
            radioButton_Left.Enabled = true;
            radioButton_Right.Enabled = true;
            comboBox_PanelNumbers.Enabled = true;
            radioButton_Left.Enabled = true;
            radioButton_Right.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnExit.Enabled = true;

            //UI related variables
            groupBox_DLE.Visible = false;
            labelSessionEndTimeData.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
            btnExit.Visible = false;
            newToolStripMenuItem.Enabled = true;
            textBox_Status.Text = textBox_Status.Text + Environment.NewLine + "Session End Time: " + labelSessionEndTimeData.Text;
            labelSessionEndTime.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Bold);
            labelDLESetting.Visible = false;
            btnDLESetting.Visible = false;
            this.BackgroundImage = Resource1.LOGO_1932x1015;
            this.BackgroundImage = Resource1.LOGO_1932X1050_center;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //Close serial port
            try
            {
                multipleSensors.closeSerialPort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                //return;
            }
            labelSessionEndTime.Visible = true;
            labelSessionEndTimeData.Visible = true;

            //Save textbox status contents in notepad Logs file in Read Only format 
            //FileAttributes logsFile = File.GetAttributes(logs_FilePath_WithFileName);
            File.SetAttributes(logs_FilePath_WithFileName, FileAttributes.ReadOnly);
            
          }

        /// <summary>
        /// Method called when Calibration form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calibration_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check if program has been auto exited
            if (Program.exit_app_flag == 1)
            {
                //Close serial port
                try
                {
                    multipleSensors.closeSerialPort();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                }

                //Save Error Logs File in Read Only format if error legs has been created
                if (File.Exists(Program.errorlogs_PathName + Program.errorlogs_FileName + ".txt"))
                    File.SetAttributes((Program.errorlogs_PathName + Program.errorlogs_FileName + ".txt"), FileAttributes.ReadOnly);
    
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                //Confirm with user if Application should exit
                if (MessageBox.Show(Form.ActiveForm, DisplayMessages.application_closebtnclick, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {   
                    if ((labelSessionStartTimeData.Text != "") && (labelSessionEndTimeData.Text == ""))
                    {
                        //Display message if session is open
                        if (MessageBox.Show(Form.ActiveForm, DisplayMessages.session_open, "Confirm", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        //Close serial port
                        try
                        {
                            multipleSensors.closeSerialPort();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                            Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                            //return;
                        }
                        
                        //Save Error Logs File in Read Only format if error legs has been created
                        if (File.Exists(Program.errorlogs_PathName + Program.errorlogs_FileName + ".txt"))
                            File.SetAttributes((Program.errorlogs_PathName + Program.errorlogs_FileName + ".txt"), FileAttributes.ReadOnly);

                        e.Cancel = false;
                        Environment.Exit(0);
                    }
                }
            }
        }

        

        /*
        /// <summary>
        /// Function to adjust timer interval when system has been locked and unlocked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (session_status == true)
            {
                //Identify if system has been locked
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    if (temp_var == 0)
                    {
                        timerstartTime = Convert.ToDateTime(labelSessionStartTimeData.Text);
                        lockoutTime = DateTime.Now;
                        TimeSpan completed_time = lockoutTime.Subtract(timerstartTime);
                        elapsed_time = completed_time;
                        Program.Err.ErrorLog(Environment.NewLine + temp_var + " Lock time: " + DateTime.Now.ToString() + " Completed Time :" + completed_time);              
                    }
                    else
                    {
                        timerstartTime = unlockTime;
                        lockoutTime = DateTime.Now;
                        TimeSpan completed_time = lockoutTime.Subtract(timerstartTime);
                        elapsed_time = completed_time;
                        temp_var = 1;
                        Program.Err.ErrorLog(Environment.NewLine + temp_var + " Lock time: " + DateTime.Now.ToString() + " Completed Time :" + completed_time);
                    }
                    MyTimer.Stop();
                    MyTimer.Enabled = false;
                }

                //Identify if system has been unlocked
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    if (temp_var == 0)
                    {
                        MyTimer = new System.Windows.Forms.Timer();
                        MyTimer.Interval = ((session_timeout * 1000) - Convert.ToInt32(elapsed_time.TotalMilliseconds));
                        timer_interval = MyTimer.Interval;
                        MyTimer.Enabled = true;
                        MyTimer.Tick += new EventHandler(MyTimer_Tick);
                        MyTimer.Start();
                        unlockTime = DateTime.Now;
                        Program.Err.ErrorLog(Environment.NewLine + temp_var + " Unlock time: " + DateTime.Now.ToString() + "  New interval :" + timer_interval);
                        temp_var = 1;
                    }
                    else
                    {
                        temp_var = 1;
                        MyTimer = new System.Windows.Forms.Timer();
                        MyTimer.Interval = (timer_interval - Convert.ToInt32(elapsed_time.TotalMilliseconds));
                        timer_interval = MyTimer.Interval;
                        MyTimer.Enabled = true;
                        MyTimer.Tick += new EventHandler(MyTimer_Tick);
                        MyTimer.Start();
                        unlockTime = DateTime.Now;
                        Program.Err.ErrorLog(Environment.NewLine + temp_var + " Unlock time: " + DateTime.Now.ToString() + "  New interval :" + timer_interval);
                    }
                }
            }
            else
            {
                return;
            }       
        }
        */

    }
}
