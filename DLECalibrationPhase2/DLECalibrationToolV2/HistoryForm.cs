using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to display the logs file contents
    /// </summary>
    public partial class HistoryForm : Form
    {
        DirectoryInfo dinfo;
        DateTime date_from;
        DateTime date_to;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public HistoryForm()
        {
            InitializeComponent();
        }
        
        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            date_from = dateTimePickerDateFrom.Value.Date;
            date_to = dateTimePickerDateTo.Value.Date;
            if (date_to == DateTime.Today)
                date_to = DateTime.Now;
            else
                date_to = date_to.AddHours(23).AddMinutes(59).AddSeconds(59);
            comboBoxFileNames.Items.Clear();

            //Display message if date to is before date from
            if (date_to < date_from)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.datefromdateto_incorrectorder);
                return;
            }
            try
            {
                //Get logs file location from database
                dbconnect.GetLogFileLocation();
                dinfo = new DirectoryInfo(dbconnect.LogsFilePath_FromDB);
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
            try
            {
                //Get the logs file based on from and to date
                var files = dinfo.GetFiles("*.*").Where(file => file.LastWriteTime >= date_from && file.LastWriteTime <= date_to); ;
                foreach (FileInfo file in files)
                    this.comboBoxFileNames.Items.Add(file.Name);
            }
            catch (DirectoryNotFoundException dr_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + dr_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + dr_ex.GetType().ToString() + ", Target Site : " + dr_ex.TargetSite + ",\tError Message : " + dr_ex.Message + ", Inner Exception : " + dr_ex.InnerException);
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
            comboBoxFileNames.Enabled = true;
            btnOpen.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string selected = comboBoxFileNames.Text;
            //Display message if no file is selected
            if (string.IsNullOrWhiteSpace(selected))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.fileselect_msg);
                return;
            }
            try
            {
                //Get contents from logs file and display in textbox
                string fileString = File.ReadAllText(dinfo.ToString() + "\\" + selected);
                textBoxLogFileContents.Text = fileString;
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
        }

    }
}
