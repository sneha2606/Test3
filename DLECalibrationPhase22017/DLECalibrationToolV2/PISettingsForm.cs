using MySql.Data.MySqlClient;
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

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to view and update PI Settings
    /// </summary>
    public partial class PISettingsForm : Form
    {
        string pi_Server ;
        string af_Server ;
        string af_DB;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();
     
        public PISettingsForm()
        {
            InitializeComponent();
        }

        private void PISettingsForm_Load(object sender, EventArgs e)
        {
            //If logged in user is not Admin, buttons Edit,Save and Reset will be disabled
            if (Program.UserType != "Admin")
            {
                btnEdit.Enabled = false;
                btnReset.Enabled = false;
                btnSave.Enabled = false;
            }
            try 
            {
                //Get PI Settings details from database
                dbconnect.GetPISettings();
                textBoxPIServer.Text = dbconnect.pi_Server;
                textBoxAFserver.Text = dbconnect.af_Server;
                textBoxAFDB.Text = dbconnect.af_DB;
                pi_Server = textBoxPIServer.Text;
                af_Server = textBoxAFserver.Text;
                af_DB = textBoxAFDB.Text;
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name: " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
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
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            textBoxPIServer.Enabled = true;
            textBoxAFserver.Enabled = true;
            textBoxAFDB.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxPIServer.Enabled = false;
            textBoxAFserver.Enabled = false;
            textBoxAFDB.Enabled = false;
            labelPIServerError.Text = "";
            labelAFServerError.Text = "";
            labelAFDBError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxPIServer.Text == "" || textBoxAFserver.Text == "" || textBoxAFDB.Text == "")
            {
                if (textBoxPIServer.Text == "")
                    labelPIServerError.Text = DisplayMessages.piserver_mandatory;
                if (textBoxAFserver.Text == "")
                    labelAFServerError.Text = DisplayMessages.afserver_mandatory;
                if (textBoxAFDB.Text == "")
                    labelAFDBError.Text = DisplayMessages.afdb_mandatory;

                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                textBoxPIServer.Enabled = true;
                textBoxAFserver.Enabled = true;
                textBoxAFDB.Enabled = true;
                return;
            }

            //Display message if Update button is clicked and no updates are detected
            else if ((pi_Server == textBoxPIServer.Text.Replace(" ", String.Empty)) && (af_Server == textBoxAFserver.Text.Replace(" ", String.Empty)) && (af_DB == textBoxAFDB.Text.Replace(" ", String.Empty)))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                return;
            }

            else
            {
                pi_Server = textBoxPIServer.Text;
                af_Server = textBoxAFserver.Text;
                af_DB = textBoxAFDB.Text;
                Update(pi_Server, af_Server, af_DB);
            }
        }

        private void Update(string piserver, string afserver, string afdb)
        {
            try 
            {
                //Update PI Settings
                dbconnect.UpdatePISettings(piserver, afserver, afdb);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.pisettings_updatesuccess);
                btnEdit.Enabled = true;
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
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
                //Update Config logs
                dbconnect.UpdateConfigLogs(DisplayMessages.pisettings_updatesuccess);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
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
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxPIServer.Enabled = false;
            textBoxAFserver.Enabled = false;
            textBoxAFDB.Enabled = false;
            textBoxPIServer.Text = pi_Server;
            textBoxAFserver.Text = af_Server;
            textBoxAFDB.Text = af_DB;
            labelPIServerError.Text = "";
            labelAFServerError.Text = "";
            labelAFDBError.Text = "";
        }

    }
}
