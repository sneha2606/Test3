using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to  create new login user for the application
    /// </summary>
    public partial class Users_CreateNewUserForm : Form
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public Users_CreateNewUserForm()
        {
            InitializeComponent();
        }

        private void Users_CreateNewUserForm_Load(object sender, EventArgs e)
        {
            List<string> user_role = new List<string>();
            try
            {
                //Get user roles from database
                user_role = dbconnect.GetUserRoles();
                //Add the userroles to Usertype combobox
                comboBoxUserType.DataSource = user_role;
                comboBoxIsActive.SelectedIndex = 0;
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            labelUsernameError.Text = "";
            labelPasswordError.Text = "";
            labelConfirmPasswordError.Text = "";
            labelEmailError.Text = "";
            labelUserTypeError.Text = "";
            labelIsActiveError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxUsername.Text==""|| textBoxConfirmPassword.Text == "" || textBoxPassword.Text == "" || textBoxConfirmPassword.Text == "" || textBoxEmail.Text == "" || comboBoxUserType.Text == "")
            {
                if (textBoxUsername.Text == "")
                    labelUsernameError.Text = DisplayMessages.username_mandatory;
                if (textBoxPassword.Text == "")
                    labelPasswordError.Text = DisplayMessages.passwordmandatory;
                if (textBoxConfirmPassword.Text == "")
                    labelConfirmPasswordError.Text = DisplayMessages.confirmpassword_mandatory;
                if (textBoxEmail.Text == "")
                    labelEmailError.Text = DisplayMessages.email_mandatory;
                if (comboBoxUserType.Text == "")
                    labelUserTypeError.Text = DisplayMessages.usertype_mandatory;
                if (comboBoxIsActive.Text == "")
                    labelIsActiveError.Text = DisplayMessages.isactive_mandatory;
            }
            //Display message if password and confirm password are not same
            else if (textBoxPassword.Text != textBoxConfirmPassword.Text)
            {
                labelPasswordError.Text = DisplayMessages.passwordconfpassword_nomatch;
                labelConfirmPasswordError.Text = DisplayMessages.passwordconfpassword_nomatch;
            }
            else
            {
                //Check if email id is in email id format
                string pattern = null;
                //pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (!Regex.IsMatch(textBoxEmail.Text, pattern))
                {
                    labelEmailError.Text = DisplayMessages.emailid_incorrect;
                    return;
                }
                else
                {
                    Create(textBoxUsername.Text, textBoxPassword.Text, comboBoxUserType.Text, textBoxEmail.Text);
                }
            }
        }

        private void Create(string username, string password, string usertype,string email)
        {
            int role_id = 0;
            string username_exists = "";
            try
            { 
                //Check if username or email id already exists in database
                username_exists = dbconnect.CheckUsernameExistsInLoginUsers(username.Replace(" ",String.Empty),email);
                if (username_exists == "Yes")
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.usernameemail_exists);
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
            try
            {
                //Get role id from database based on role name selected 
                role_id = dbconnect.GetRoleIDBasedOnRoleName(comboBoxUserType.Text);
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
                //Create login user with details - username,password,email,role id and is active status
                dbconnect.CreateLoginUser(textBoxUsername.Text, textBoxPassword.Text, textBoxEmail.Text, role_id, comboBoxIsActive.Text);
                MessageBox.Show(Form.ActiveForm, DisplayMessages.loginusercreate_success);
                
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
                dbconnect.UpdateConfigLogs("New user "+username+" created successfully.");
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
            UsersForm user = new UsersForm();
            user.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }


    }
}
