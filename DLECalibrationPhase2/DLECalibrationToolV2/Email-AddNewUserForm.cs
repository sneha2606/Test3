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
    /// Form to add new email recipient
    /// </summary>
    public partial class Email_AddNewUser : Form
    {
        string username;
        string email;
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public Email_AddNewUser()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //Check if email id entered is in correct format
            string usernameemail_exists = "";
            labelUsernameError.Text = "";
            labelEmailIDError.Text = "";
            string pattern = null;
            pattern=@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            //Check if username or pwd is empty
            if (textBoxUsername.Text == "" || textBoxEmailID.Text == "")
            {
                if (textBoxUsername.Text == "")
                    labelUsernameError.Text = DisplayMessages.username_mandatory;
                if (textBoxEmailID.Text == "")
                    labelEmailIDError.Text = DisplayMessages.email_mandatory;
                return;
            }
            else
            {
                //Check if email is in correct format
                if (!Regex.IsMatch(textBoxEmailID.Text, pattern))
                {
                    labelEmailIDError.Text = DisplayMessages.emailid_incorrect;
                    return;
                }
                else
                {
                    try
                    {
                        //Check if username and email exists
                        usernameemail_exists = dbconnect.CheckUsernameEmailExistsInEmailDetails(textBoxUsername.Text.Replace(" ", String.Empty), textBoxEmailID.Text);
                        if (usernameemail_exists == "Yes")
                        {
                            MessageBox.Show(Form.ActiveForm, DisplayMessages.usernameemail_exists);
                            return;
                        }
                        else
                        {
                            username = textBoxUsername.Text;
                            email = textBoxEmailID.Text;
                            //Create new email details - username and email
                            Update(username, email);
                        }
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
            }
           
        }


        private void Update(string username, string email)
        {
            string username_exists = "";
            try
            {
                //Check if username exists
                username_exists=dbconnect.CheckUsernameExists(username);
                if (username_exists == "Yes")
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.usernameemail_exists);
                    return;
                }        
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
                //Create Email details - username and email
                dbconnect.CreateEmailDetails(username,email);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.emailcreate_success);
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
                //Update config logs
                dbconnect.UpdateConfigLogs("Email details for user "+ username+" created successfully.");
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
           
            this.Close();

            //Display Email Recipients form
            EmailRecipientsForm er_form = new EmailRecipientsForm();
            er_form.ShowDialog();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }


    }
}
