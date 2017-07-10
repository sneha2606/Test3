using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to view and update the SMTP Settings
    /// </summary>
    public partial class SMTPSettingsForm : Form
    {
        string SMTPServer;
        string SMTPPort;
        string SMTPSendEmail;
        string SMTPSendPassword;
        string EnableSSL;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public SMTPSettingsForm()
        {
            InitializeComponent();
        }

        private void SMTPSettingsForm_Load(object sender, EventArgs e)
        {
            //If logged in user is not Admin , buttons Edit,Save and Reset will be disabled
            if (Program.UserType != "Admin")
            {
                btnEdit.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = false;
            }
            try
            {
                //Get smtp settings from database
                dbconnect.GetSMTPSettings();
                textBoxSMTPServer.Text = dbconnect.SmtpServer;
                textBoxSMTPPort.Text = dbconnect.SmtpPort;
                textBoxSMTPSendEmail.Text = dbconnect.SmtpEmail;
                comboBoxEnableSSL.Text = dbconnect.EnableSSL;
                //textBoxSMTPPassword.Text = "******";
                textBoxSMTPPassword.Text = dbconnect.SmtpEmailPwd;
                SMTPServer = textBoxSMTPServer.Text;
                SMTPPort = textBoxSMTPPort.Text;
                SMTPSendEmail = textBoxSMTPSendEmail.Text;
                SMTPSendPassword = textBoxSMTPPassword.Text;
                EnableSSL = comboBoxEnableSSL.Text;
                comboBoxEnableSSL.BeginInvoke(new Action(() => { comboBoxEnableSSL.Select(0, 0); }));//to prevent blue light from flasing by default
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
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            textBoxSMTPServer.Enabled = true;
            textBoxSMTPPort.Enabled = true;
            textBoxSMTPSendEmail.Enabled = true;
            textBoxSMTPPassword.Enabled = true;
            comboBoxEnableSSL.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxSMTPServer.Enabled = false;
            textBoxSMTPPort.Enabled = false;
            textBoxSMTPSendEmail.Enabled = false;
            textBoxSMTPPassword.Enabled = false;
            comboBoxEnableSSL.Enabled = false;
            labelEnableSSLError.Text = "";
            labelSMTPPortError.Text="";
            labelSMTPSendEmailIDError.Text="";
            labelSMTPPasswordError.Text = "";
            labelSMTPServerError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxSMTPServer.Text == "" || textBoxSMTPPort.Text == "" || textBoxSMTPSendEmail.Text == ""||textBoxSMTPPassword.Text=="")
            {
                if (textBoxSMTPServer.Text == "")
                    labelSMTPServerError.Text = DisplayMessages.smtpserver_mandatory;
                if (textBoxSMTPPort.Text == "")
                    labelSMTPPortError.Text = DisplayMessages.smtpport_mandatory;
                if (textBoxSMTPSendEmail.Text == "")
                    labelSMTPSendEmailIDError.Text = DisplayMessages.smtpsendemail_mandatory;
                if (textBoxSMTPPassword.Text == "")
                    labelSMTPPasswordError.Text = DisplayMessages.smtpsendemailpassword_mandatory;
                if (comboBoxEnableSSL.Text == "")
                    labelEnableSSLError.Text = DisplayMessages.smtpenablessl_mandatory;

                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                textBoxSMTPServer.Enabled = true;
                textBoxSMTPPort.Enabled = true;
                textBoxSMTPSendEmail.Enabled = true;
                textBoxSMTPPassword.Enabled = true;
                comboBoxEnableSSL.Enabled = true;
                return;
            }
            //Display message if Update button is clicked and no updates are detected
            else if ((SMTPServer == textBoxSMTPServer.Text.Replace(" ", String.Empty)) && (SMTPPort == textBoxSMTPPort.Text.Replace(" ", String.Empty)) && (SMTPSendEmail == textBoxSMTPSendEmail.Text.Replace(" ", String.Empty)) && (EnableSSL == comboBoxEnableSSL.Text) && (SMTPSendPassword == textBoxSMTPPassword.Text.Replace(" ", String.Empty)))
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                return;
            }
            else
            {
                SMTPServer = textBoxSMTPServer.Text;
                SMTPPort = textBoxSMTPPort.Text;
                SMTPSendEmail = textBoxSMTPSendEmail.Text;
                EnableSSL = comboBoxEnableSSL.Text;
                SMTPSendPassword = textBoxSMTPPassword.Text;
                Update(SMTPServer, SMTPPort, SMTPSendEmail, SMTPSendPassword,EnableSSL);
            }
        }

        void Update(string SMTPServer, string SMTPPort, string SMTPSendEmail, string SMTPSendPassword,string EnableSSL)
        {
            try
            {
                //Encoding
                string encode_smtppwd = string.Empty;
                byte[] encode = new byte[SMTPSendPassword.Length];
                encode = Encoding.UTF8.GetBytes(SMTPSendPassword);
                encode_smtppwd = Convert.ToBase64String(encode);
 
                //Update SMTP settings - SMTP Server , port ,Email , pwd and Enable SSL
                dbconnect.UpdateSMTPSettings(SMTPServer, SMTPPort, SMTPSendEmail, encode_smtppwd, EnableSSL);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.smtpupdate_success);
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
                //Update Config logs
                dbconnect.UpdateConfigLogs(DisplayMessages.smtpupdate_success);
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
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            textBoxSMTPServer.Enabled = false;
            textBoxSMTPPort.Enabled = false;
            textBoxSMTPSendEmail.Enabled = false;
            comboBoxEnableSSL.Enabled = false;
            textBoxSMTPPassword.Enabled = false;
            textBoxSMTPServer.Text = SMTPServer;
            textBoxSMTPPort.Text = SMTPPort;
            textBoxSMTPSendEmail.Text = SMTPSendEmail;
            textBoxSMTPPassword.Text = SMTPSendPassword;
            comboBoxEnableSSL.Text = EnableSSL;
            labelEnableSSLError.Text = "";
            labelSMTPPortError.Text = "";
            labelSMTPSendEmailIDError.Text = "";
            labelSMTPServerError.Text = "";
            labelSMTPPasswordError.Text = "";
            labelSMTPPasswordError.Text = "";
        }

        private void textBoxSMTPServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //SMTP Server ip address textbox - Allow users to enter only numbers and . 
            var regex = new Regex(@"[^0-9.\s]");
            if (regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBoxSMTPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            //SMTP Port textbox - Allow users to enter only numbers
             var regex = new Regex(@"[^0-9\s]");
            if (regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }


    }
}
