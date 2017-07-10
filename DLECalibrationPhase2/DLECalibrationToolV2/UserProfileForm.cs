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
    /// Form to display the logged in users details and update user details 
    /// </summary>
    public partial class UserProfileForm : Form
    {
        string email = "";
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public UserProfileForm()
        {
            InitializeComponent();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            textBoxUsername.Text = Program.Username;
            try
            {
                //Get logged in users email id
                email = dbconnect.GetLoggedInUserEmailID(textBoxUsername.Text);
                textBoxEmail.Text = email;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            labelEmailError.Text = "";
            labelConfirmPasswordError.Text = "";
            labelCurrentPasswordError.Text="";
            labelNewPasswordError.Text="";
          
            //User has chosen not to change password
            if (textBoxConfirmPassword.ReadOnly == true)
            {
                if (textBoxEmail.Text == "")
                    labelEmailError.Text = DisplayMessages.email_mandatory;
                else if (textBoxEmail.Text.Replace(" ", string.Empty) == email)
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                else
                {
                    //check if email id is in email format
                    string pattern = null;
                    pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    if (!Regex.IsMatch(textBoxEmail.Text, pattern))
                    {
                        labelEmailError.Text = DisplayMessages.emailid_incorrect;
                        return;
                    }
                    else
                    {
                        UpdateEmail(textBoxUsername.Text, textBoxEmail.Text);
                        return;
                    }
                }
            }
            else
            {
                //Display message if any mandatory fields are left blank
                if ((textBoxCurrentPassword.Text == "") || (textBoxNewPassword.Text == "") || (textBoxConfirmPassword.Text == "")||(textBoxEmail.Text==""))
                {
                    if (textBoxCurrentPassword.Text == "")
                        labelCurrentPasswordError.Text=DisplayMessages.currentpassword_mandatory;
                    if(textBoxNewPassword.Text == "")
                         labelNewPasswordError.Text=DisplayMessages.newpassword_mandatory;
                     if(textBoxConfirmPassword.Text == "")
                         labelConfirmPasswordError.Text = DisplayMessages.confirmpassword_mandatory;
                    if(textBoxEmail.Text=="")
                        labelEmailError.Text=DisplayMessages.email_mandatory;
                    return;
                }
                else
                {
                    string validate_res = "";
                    try
                    {
                        //Check if username and current password entered is correct
                        validate_res = dbconnect.ValidateLogin(Program.Username, textBoxCurrentPassword.Text);
                        if (validate_res != "")
                        {
                            labelCurrentPasswordError.Text = DisplayMessages.currentpassword_incorrect;
                            return;
                        }
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

                  //Display message if password and confirm password are not same
                  if (textBoxConfirmPassword.Text != textBoxNewPassword.Text)
                  {
                    labelNewPasswordError.Text = DisplayMessages.newpwdconfirmpwd_nomatch;
                    labelConfirmPasswordError.Text = DisplayMessages.newpwdconfirmpwd_nomatch;
                    textBoxCurrentPassword.Enabled = true;
                    textBoxNewPassword.Enabled = true;
                    textBoxConfirmPassword.Enabled = true;
                    return;
                  }
                  else if (textBoxConfirmPassword.Text == textBoxNewPassword.Text)
                  {
                      //Check if email id is in email format
                      string pattern = null;
                      pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                      if (!Regex.IsMatch(textBoxEmail.Text, pattern))
                      {
                          labelEmailError.Text = DisplayMessages.emailid_incorrect;
                          return;
                      }
                      else
                      {
                          Update(textBoxUsername.Text, textBoxConfirmPassword.Text, textBoxEmail.Text);
                      }
                  }
                }
              }
        }
        
        void Update(string username, string newpassword, string email)
        {
            try
            {
                //Update logged in user password and email details
                dbconnect.UpdateLoggedInUserPasswordAndEmailDetails(username, newpassword, email);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.userprofileupdate_success);
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
                dbconnect.UpdateConfigLogs("User Profile details of user "+username+" updated successfully.");
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
            this.Hide();
            this.Close();           
        }

        void UpdateEmail(string username, string email)
        {
            try
            {
                //Update email 
                dbconnect.UpdateOnlyEmail(username, email);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.userdetailsupdate_success);
                
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
                dbconnect.UpdateConfigLogs("User Profile details of user " + username + " updated successfully.");
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
            this.Hide();
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            //UI changes when Change password button is clicked
            labelCurrentPassword.Text = labelCurrentPassword.Text + " *";
            labelConfirmPassword.Text = labelConfirmPassword.Text + " *";
            labelNewPassword.Text = labelNewPassword.Text + " *";
            textBoxCurrentPassword.ReadOnly = false;
            textBoxNewPassword.ReadOnly = false;
            textBoxConfirmPassword.ReadOnly = false;
            textBoxCurrentPassword.Enabled = true;
            textBoxNewPassword.Enabled = true;
            textBoxConfirmPassword.Enabled = true;
            btnChangePassword.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
