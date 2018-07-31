using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to display the email recipient - add new recipients and delete recipients
    /// </summary>
    public partial class EmailRecipientsForm : Form
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public EmailRecipientsForm()
        {
            InitializeComponent();
        }

        private void ViewEmailRecipients_Load(object sender, EventArgs e)
        {
            //If logged in user is not Admin , buttons Add and Delete will be disabled
            if (Program.UserType != "Admin")
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
            dataGridViewEmailRecipients.AllowUserToAddRows = false;
            try
            {
                //Get all users email details and store in datatable
                DataTable table = dbconnect.ViewEmailDetails();
                this.dataGridViewEmailRecipients.DefaultCellStyle.Font = new Font("Calibiri", 11);
                this.dataGridViewEmailRecipients.ColumnHeadersDefaultCellStyle.Font = new Font("Calibiri", 11, FontStyle.Bold);
                
                //Bind datatable to gridview
                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;
                dataGridViewEmailRecipients.DataSource = bSource;
                dataGridViewEmailRecipients.Columns[0].Width = 150;
                dataGridViewEmailRecipients.Columns[1].Width = 300;
                dataGridViewEmailRecipients.Columns[2].Width = 150;
              
                dataGridViewEmailRecipients.AutoResizeRows();
                dataGridViewEmailRecipients.ClearSelection();

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Display email add new user form
            Email_AddNewUser ea = new Email_AddNewUser();
            ea.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(DisplayMessages.deleteemail_confirmation, "Confirmation Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //Get the current user details based on row selected
            string username_delete = this.dataGridViewEmailRecipients.CurrentRow.Cells[0].Value.ToString();
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //Delete user details from database
                    dbconnect.DeleteEmailDetail(username_delete);
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.emailupdate_success);
                    btnDelete.Enabled = false;
                    this.ViewEmailRecipients_Load(sender, e);
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
                    dbconnect.UpdateConfigLogs("Ëmail details for user " + username_delete + " deleted successfully.");
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
            else
            {
                return;
            }
        }

        private void dataGridViewEmailRecipients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEmailRecipients.AutoResizeRows();
            dataGridViewEmailRecipients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            if (e.RowIndex >= 0)
            {
                //Enable Delete button if User type is Admin and 
                if (Program.UserType == "Admin")
                    btnDelete.Enabled = true;
            }
        }

        private void dataGridViewEmailRecipients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEmailRecipients.AutoResizeRows();
            dataGridViewEmailRecipients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            if (e.RowIndex >= 0)
            {
                //Enable Delete button if User type is Admin
                if (Program.UserType == "Admin")
                    btnDelete.Enabled = true;
            }
        }

        private void dataGridViewEmailRecipients_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEmailRecipients.AutoResizeRows();
            dataGridViewEmailRecipients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            if (e.RowIndex >= 0)
            {
                //Enable Delete button if User type is Admin
                if (Program.UserType == "Admin")
                     btnDelete.Enabled = true;
            }
        }

        private void dataGridViewEmailRecipients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Disable delete button if column header is clicked
            btnDelete.Enabled = false;
            dataGridViewEmailRecipients.AutoResizeRows();
            dataGridViewEmailRecipients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void dataGridViewEmailRecipients_Sorted(object sender, EventArgs e)
        {
            dataGridViewEmailRecipients.ClearSelection();
            dataGridViewEmailRecipients.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

    }
}
