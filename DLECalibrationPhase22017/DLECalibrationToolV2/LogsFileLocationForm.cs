using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to view and update the Logs file location 
    /// </summary>
    public partial class LogFileLocationForm : Form
    {
        string original_Text;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public LogFileLocationForm()
        {
            InitializeComponent();
        }

        private void LogFileLocation_Form_Load(object sender, EventArgs e)
        {
            //If logged in user is not Admin , buttons Edit,Save and Reset will be disabled
            if (Program.UserType != "Admin")
            {
                btnEdit.Enabled = false;
                btnReset.Enabled = false;
                btnSave.Enabled = false;
            }
            try 
            {
                //Get logs file location from database
                dbconnect.GetLogFileLocation();
                original_Text = dbconnect.LogsFilePath_FromDB;
                textBoxLogFileLoc.Text = original_Text;
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                this.Close();
                return;
            }
             catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException); 
                 this.Close();
                return;
            }
          
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            //Display folder browsing dialog to select the new folder location
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            textBoxLogFileLoc.Enabled = true;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxLogFileLoc.Text = fbd.SelectedPath;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
            }
            else if (result == DialogResult.Cancel)
            {
                textBoxLogFileLoc.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = false;
                btnEdit.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            btnEdit.Enabled = true;
            labelFileLocationError.Text = "";

            //Display message if textbox file location is left blank
            if (textBoxLogFileLoc.Text == "")
            {
                labelFileLocationError.Text = DisplayMessages.logsfilelocation_mandatory;
                return;
            }
            //Display message if there are no changes detected
            else if ( original_Text  == textBoxLogFileLoc.Text)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                return;
            }
            else
            {
                original_Text = textBoxLogFileLoc.Text;
                textBoxLogFileLoc.Enabled = false;
                Update(textBoxLogFileLoc.Text);
            }
            
        }

        private void Update(string logs)
        {
            try
            {
                //Update logs file location 
                dbconnect.UpdateLogsFileLoc(logs);
                textBoxLogFileLoc.Text = logs;
                MessageBox.Show(Form.ActiveForm, DisplayMessages.logsfile_updatesuccess);
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
          

            try 
            {
                //Update config logs
                dbconnect.UpdateConfigLogs(DisplayMessages.logsfile_updatesuccess);
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBoxLogFileLoc.Text = original_Text;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxLogFileLoc.Enabled = false;
            btnEdit.Enabled = true;
        }
        }
    }
