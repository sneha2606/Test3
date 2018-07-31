using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using MySql.Data.MySqlClient;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to view and update the Serial Port Settings
    /// </summary>
    public partial class SerialPortSettingsForm : Form
    {
        string comPort;
        string baudRate;
        string parity;
        string stopBits;
        string dataBits;
        Dictionary<string, string> comport_withDesc = new Dictionary<string, string>();
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public SerialPortSettingsForm()
        {
            InitializeComponent();
        }

        private void SerialPortSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Get serial port settings from database
                dbconnect.GetSerialPortSettings();
                comboBoxCOMPort.Text = dbconnect.Com_Port;
                comboBoxBaudRate.Text = dbconnect.Baud_Rate.ToString();
                comboBoxParity.Text = dbconnect.Parity.ToString();
                textBoxStopBits.Text = dbconnect.Stop_Bits.ToString();
                textBoxDataBits.Text = dbconnect.Data_Bits.ToString();
                comPort = comboBoxCOMPort.Text;
                baudRate = comboBoxBaudRate.Text;
                parity = comboBoxParity.Text;
                stopBits = textBoxStopBits.Text;
                dataBits = textBoxDataBits.Text;
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
            //If logged in user is not Admin , Baud rate and Parity combo box will be disabled
            if (Program.UserType != "Admin")
            {
                comboBoxBaudRate.Enabled = false;
                comboBoxParity.Enabled = false;
                textBoxDataBits.Enabled = false;
                textBoxStopBits.Enabled = false;
            }
            else
            {
                comboBoxBaudRate.Enabled = true;
                comboBoxParity.Enabled = true;
                textBoxDataBits.Enabled = true;
                textBoxStopBits.Enabled = true;
            }

            comboBoxCOMPort.Enabled = true;
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            btnEdit.Enabled = false;
            comboBoxCOMPort.Items.Clear();
            

            //Get details of COM Port and display in comport combo box
            foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
                comboBoxCOMPort.Items.Add(comPort.Name + "-" + comPort.Description);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            btnEdit.Enabled = true;
            comboBoxCOMPort.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            comboBoxParity.Enabled = false;
            textBoxStopBits.Enabled = false;
            textBoxDataBits.Enabled = false;
            labelBaudRateError.Text = "";
            labelCOMPortError.Text = "";
            labelDataBitsError.Text = "";
            labelParityError.Text = "";
            labelStopBitsError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxStopBits.Text == "" || textBoxDataBits.Text == "" || comboBoxCOMPort.Text == "" || comboBoxBaudRate.Text == "" || comboBoxParity.Text == "")
            {
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                comboBoxCOMPort.Enabled = true;
              
                if (Program.UserType != "Admin")
                {
                    comboBoxBaudRate.Enabled = false;
                    comboBoxParity.Enabled = false;
                    textBoxStopBits.Enabled = false;
                    textBoxDataBits.Enabled = false;
                }
                else
                {
                    comboBoxBaudRate.Enabled = true;
                    comboBoxParity.Enabled = true;
                    textBoxStopBits.Enabled = true;
                    textBoxDataBits.Enabled = true;
                }

                if (comboBoxCOMPort.Text == "")
                    labelCOMPortError.Text = DisplayMessages.comport_mandatory;
                if (comboBoxBaudRate.Text == "")
                    labelBaudRateError.Text = DisplayMessages.baudrate_mandatory;
                if (comboBoxParity.Text == "")
                    labelParityError.Text = DisplayMessages.parity_mandatory;
                if (textBoxDataBits.Text == "")
                    textBoxDataBits.Text = DisplayMessages.databits_mandatory;
                if (textBoxStopBits.Text == "")
                    textBoxStopBits.Text = DisplayMessages.stopbits_mandatory;
                return;
            }

            //Display message if Update button is clicked and no updates are detected
            else if ((comPort == comboBoxCOMPort.Text) && (baudRate == comboBoxBaudRate.Text) && (parity == comboBoxParity.Text))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                return;
            }
            else
            {
                comPort = comboBoxCOMPort.Text.Substring(0, 4);
                baudRate = comboBoxBaudRate.Text;
                parity = comboBoxParity.Text;
                stopBits = textBoxStopBits.Text;
                dataBits = textBoxDataBits.Text;
                Update(comPort, baudRate, parity, stopBits, dataBits);
            }
        }

        private void Update(string comPort, string baudRate, string parity, string stopBits, string dataBits)
        {
            try
            {
                //Update Seial port settings - Com Port, Baud rate, Parity, Stop Bits and Data Bits
                dbconnect.UpdateSerialPortSettings(comPort, baudRate, parity, stopBits, dataBits);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.serialportupdate_success);
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
                dbconnect.UpdateConfigLogs(DisplayMessages.serialportupdate_success);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            btnEdit.Enabled = true;
            comboBoxCOMPort.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            comboBoxParity.Enabled = false;
            textBoxDataBits.Enabled = false;
            textBoxStopBits.Enabled = false;

            //On clicking Reset, previous value of ComPort is stored
            if (!comboBoxCOMPort.Items.Contains(comPort))
            {
                comboBoxCOMPort.Items.Add(comPort);
                comboBoxCOMPort.Text = comPort;
            }

            comboBoxBaudRate.Text = baudRate;
            comboBoxParity.Text = parity;
            textBoxStopBits.Text = stopBits;
            textBoxDataBits.Text = dataBits;
            labelBaudRateError.Text = "";
            labelCOMPortError.Text = "";
            labelDataBitsError.Text = "";
            labelParityError.Text = "";
            labelStopBitsError.Text = "";
        }
    }
}
