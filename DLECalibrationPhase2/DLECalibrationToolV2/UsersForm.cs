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
    /// Form to display login users list-add new user and delete user from user list
    /// </summary>
    public partial class UsersForm : Form
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public UsersForm()
        {
            InitializeComponent();
        }

        private void ViewUsers_Load(object sender, EventArgs e)
        {
            dataGridViewUserList.AllowUserToAddRows = false;
            try
            {
                //Get logged in users list from database and store it in datatable
                DataTable table = dbconnect.GetLoggedInUsersList();

                //Grid view formatting
                this.dataGridViewUserList.DefaultCellStyle.Font = new Font("Calibiri", 11);
                this.dataGridViewUserList.ColumnHeadersDefaultCellStyle.Font = new Font("Calibiri", 11, FontStyle.Bold);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;
                dataGridViewUserList.DataSource = bSource;
                dataGridViewUserList.Columns[0].Width = 180;
                dataGridViewUserList.Columns[1].Width = 300;
                dataGridViewUserList.Columns[2].Width = 80;
                dataGridViewUserList.Columns[3].Width = 80;
                dataGridViewUserList.Columns[4].Width = 150;
                dataGridViewUserList.Columns[5].Width = 150;
                dataGridViewUserList.AutoResizeRows();
                dataGridViewUserList.ClearSelection();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Create New User form is displayed when Add button is clicked on
            Users_CreateNewUserForm createnewuser_form = new Users_CreateNewUserForm();
            createnewuser_form.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Send details to Update User form
            Users_UpdateUserForm updateuser = new Users_UpdateUserForm();
            updateuser.textBoxUserName.Text = this.dataGridViewUserList.CurrentRow.Cells[0].Value.ToString();
            updateuser.textBoxEmail.Text = this.dataGridViewUserList.CurrentRow.Cells[1].Value.ToString();
            updateuser.comboBoxIsActive.Text = this.dataGridViewUserList.CurrentRow.Cells[2].Value.ToString();
            updateuser.comboBoxUserType.Text = this.dataGridViewUserList.CurrentRow.Cells[3].Value.ToString();
            updateuser.ShowDialog();
        }

        private void dataGridViewUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUserList.AutoResizeRows();
            //Enable Edit button when user clicks on any row other than row header
            if (e.RowIndex >= 0)
                btnEdit.Enabled = true;
        }

        private void dataGridViewUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUserList.AutoResizeRows();
            //Enable Edit button when user clicks on any row other than row header
            if (e.RowIndex >= 0)
                btnEdit.Enabled = true;
        }

        private void dataGridViewUserList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUserList.AutoResizeRows();
            //Enable Edit button when user clicks on any row other than row header
            if (e.RowIndex >= 0)
                btnEdit.Enabled = true;
        }

        private void dataGridViewUserList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Disable Edit button when Column header is clicked on
            btnEdit.Enabled = false;
            dataGridViewUserList.AutoResizeRows();
        }

        private void dataGridViewUserList_Sorted(object sender, EventArgs e)
        {
            //Clears row selection when column header is clicked on and column is sorted in ascending or descending order
            dataGridViewUserList.ClearSelection();
        }

    }
}