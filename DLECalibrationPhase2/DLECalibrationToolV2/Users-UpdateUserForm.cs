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
    /// Form to update users details
    /// </summary>
    public partial class Users_UpdateUserForm : Form
    {
        string username = "";
        string email = "";
        string isactive = "";
        string usertype = "";
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public Users_UpdateUserForm()
        {
            InitializeComponent();
        }

        private void UpdateUserForm_Load(object sender, EventArgs e)
        {
            username = textBoxUserName.Text;
            email = textBoxEmail.Text;
            isactive = comboBoxIsActive.Text;
            usertype = comboBoxUserType.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            labelEmailError.Text = "";
            labelUserTypeError.Text = "";
            labelIsActiveError.Text = "";

            //Display message if any mandatory fields are left blank
            if (textBoxEmail.Text == "" || comboBoxUserType.Text == "" || comboBoxIsActive.Text == "")
            {
                if (textBoxEmail.Text == "")
                    labelEmailError.Text = DisplayMessages.email_mandatory;
                if (comboBoxUserType.Text == "")
                    labelUserTypeError.Text = DisplayMessages.usertype_mandatory;
                if (comboBoxIsActive.Text == "")
                    labelIsActiveError.Text = DisplayMessages.isactive_mandatory;
                return;
            }
            //Display message if Update button is clicked and no updates are detected
            else if (email == textBoxEmail.Text.Replace(" ", string.Empty) && usertype == comboBoxUserType.Text && isactive == comboBoxIsActive.Text)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.confsettings_noupdates);
                return;
            }
            else
            {
                Update(textBoxUserName.Text, textBoxEmail.Text, comboBoxUserType.Text, comboBoxIsActive.Text);
            }
        }

        void Update(string username, string email, string usertype, string isactive)
        {
            int role_id = 0;
            string email_exists = "";
            try 
            {
                //Check if email id already exists for any other user in database
                email_exists=dbconnect.CheckEmailExistsInLogin(username,email);
                if (email_exists == "Yes")
                {
                    labelEmailError.Text = DisplayMessages.email_exists;
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
                //Get role id for user based on user type
                role_id = dbconnect.GetRoleIDBasedOnRoleName(usertype);
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

            //Check if email id is in correct email id format
            string pattern = null;
            pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(textBoxEmail.Text, pattern))
            {
                labelEmailError.Text = DisplayMessages.emailid_incorrect;
                return;
            }
            else
            {
                try
                {
                    //Update user details - Username,Email,Role id,IsActive
                    dbconnect.UpdateUserDetails(username, textBoxEmail.Text, role_id, comboBoxIsActive.Text);
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
                    //Update config logs 
                    dbconnect.UpdateConfigLogs("User details for user " + username + " updated successfully.");
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
                this.Close();
                UsersForm user = new UsersForm();
                user.ShowDialog();
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

    }
}
