using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to view and update the ARTNet settings
    /// </summary>
    public partial class ARTNetSettingsForm : Form
    {
        string artnet_ip;
        int artnet_port;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public ARTNetSettingsForm()
        {
            InitializeComponent();
        }

        private void ARTNetSettings_Form_Load(object sender, EventArgs e)
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
                //Get artnet settings from database
                dbconnect.GetARTNetSettings();
                textBoxIPAddress.Text = dbconnect.ip_address;
                textBoxPort.Text = dbconnect.udp_port.ToString();
                artnet_ip = textBoxIPAddress.Text;
                artnet_port = Convert.ToInt32(textBoxPort.Text);
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
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException); this.Close();
                this.Close();
                return;
            }
        }

        private void textBoxIPAddress_KeyPress(object sender, KeyPressEventArgs e)
       {
            //Check if IP Address textbox contains only numbers from 0-9 and .
            var regex = new Regex(@"[^0-9.\d]");
            if (regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Check if Port textbox contains only numbers from 0-9 and .
            var regex = new Regex(@"[^0-9\d]");
            if (regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            textBoxIPAddress.Enabled = true;
            textBoxPort.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxIPAddress.Enabled = false;
            textBoxPort.Enabled = false;
            int j;
            IPAddress address;
            labelPortError.Text = "";
            labelIPAddressError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxIPAddress.Text == "" || textBoxPort.Text == "")
            {
                if (textBoxIPAddress.Text == "")
                    labelIPAddressError.Text = DisplayMessages.ipadd_mandatory;
                if (textBoxPort.Text == "")
                    labelPortError.Text = DisplayMessages.portmandatory;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                textBoxIPAddress.Enabled = true;
                textBoxPort.Enabled = true;
                return;
            }

            else if (!IPAddress.TryParse(textBoxIPAddress.Text, out address))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.ipadd_incorrect);
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                textBoxIPAddress.Enabled = true;
                textBoxPort.Enabled = true;
                return;
            }

            else if (Int32.TryParse(textBoxPort.Text, out j))
            {
                //Display message if Update button is clicked and no updates are detected
                if ((artnet_port == Convert.ToInt32(textBoxPort.Text) && (artnet_ip == textBoxIPAddress.Text)))
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                    btnEdit.Enabled = true;
                    return;
                }
                else
                {
                    artnet_port = j;
                    artnet_ip = textBoxIPAddress.Text;
                    artnet_port = Convert.ToInt32(textBoxPort.Text);
                    Update(artnet_ip, artnet_port);
                }
            }
            else if (!Int32.TryParse(textBoxPort.Text, out j))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.udpport_incorrect);
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                textBoxIPAddress.Enabled = true;
                textBoxPort.Enabled = true;
            }
        }

        private void Update(string artnetip, int artnetport)
        {
            try
            {
                //Update artnet settings
                dbconnect.UpdateArtNetSettings(artnetip, artnetport.ToString());
                MessageBox.Show(Form.ActiveForm, DisplayMessages.artnetupdate_success);
                btnEdit.Enabled = true;
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name: " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
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
                dbconnect.UpdateConfigLogs("ARTNet settings updated successfully");
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name: " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
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
            textBoxIPAddress.Text = artnet_ip;
            textBoxPort.Text = artnet_port.ToString();
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxIPAddress.Enabled = false;
            textBoxPort.Enabled = false;
            btnEdit.Enabled = true;
            labelPortError.Text = "";
            labelIPAddressError.Text = "";
        }
    }
}
