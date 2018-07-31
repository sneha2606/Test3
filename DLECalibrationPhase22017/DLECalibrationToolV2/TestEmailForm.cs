using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    public partial class TestEmailForm : Form
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        public TestEmailForm()
        {
            InitializeComponent();
        }

        private void TestEmailForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Get email id from database
                dbconnect.GetEmailID();
                foreach (var i in dbconnect.EmailID)
                {
                    comboBox_EmailID.Items.Add(i);
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_EmailID.Text == "")
            {
                MessageBox.Show("Please select an email id from the dropdown list or enter a valid email id");
                return;
            }
            else
            {
                try
                {
                    //Get smtp settings to send email
                    dbconnect.GetSMTPSettings();
                }
                catch (MySqlException mysqlex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                    return;
                }
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(dbconnect.SmtpServer);
                    mail.From = new MailAddress(dbconnect.SmtpEmail);
                    mail.To.Add(comboBox_EmailID.Text);
                    mail.Subject = "Test Email";
                    mail.Body = "This is an auto-generated email. Please do not reply to this email.";
                    SmtpServer.Port = Convert.ToInt32(dbconnect.SmtpPort);//Port can be 587 or 465 or 25
                    SmtpServer.EnableSsl = Convert.ToBoolean(dbconnect.EnableSSL == "Yes" ? true : false);
                    SmtpServer.Credentials = new NetworkCredential(dbconnect.SmtpEmail, dbconnect.SmtpEmailPwd);
                    SmtpServer.Send(mail);
                    MessageBox.Show("Email has been sent to email id : " + comboBox_EmailID.Text);
                }
                catch (MySqlException mysql_ex)
                {
                    MessageBox.Show(DisplayMessages.general_error + mysql_ex.Message + Environment.NewLine + "Email has not been sent to email id : " + comboBox_EmailID.Text + " Please check if the email id is correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(DisplayMessages.general_error + ex.Message + Environment.NewLine + "Email has not been sent to email id : " + comboBox_EmailID.Text + " Please check if the email id is correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                    return;
                }
            }
        }
    }
}
