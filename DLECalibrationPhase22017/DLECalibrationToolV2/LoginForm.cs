using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    /// Form to login to the application
    /// </summary>
    public partial class LoginForm : Form
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();
       
        public LoginForm()
        {
            InitializeComponent();
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0)
            {
                MessageBox.Show(DisplayMessages.appsettings_empty);
                Environment.Exit(0);
            }
            try
            {
                //Connect to database
                DatabaseConnectMySQL.DbConnection();
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                Environment.Exit(0);
            }
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Username field can only contain characters a-z,A-Z,_ and numbers 0-9
            var regex = new Regex(@"[^a-zA-Z0-9_\s]");
            if (regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                validate_login(textBoxUsername.Text, textBoxPassword.Text);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show( DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }

        void validate_login(string user, string pass)
        {
            string status = "";
         
            //Display message if any mandatory fields are left blank
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text) || (string.IsNullOrWhiteSpace(textBoxPassword.Text)))
            {
               MessageBox.Show(DisplayMessages.login_blank);
               return;
            }

                //Check if username and password are correct
                status = dbconnect.ValidateLogin(user, pass);
                if (status != "")
                {
                    MessageBox.Show(Form.ActiveForm, status);
                    return;
                }
                   
                //Get logged in users role
                dbconnect.GetLoggedInUserRole();
                Program.Username = textBoxUsername.Text;
               
                this.Hide();
                
                //Display Calibration Form
                CalibrationForm cf = new CalibrationForm();
                cf.Show();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.exit_app_flag == 1)
                e.Cancel = false;
            else
            {
                if (MessageBox.Show(Form.ActiveForm, DisplayMessages.cancel_logout,
                         "Confirmation Box",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;

                }
            }
        }

    }
}
