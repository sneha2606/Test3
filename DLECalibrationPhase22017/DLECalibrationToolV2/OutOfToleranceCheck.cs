using DLECalibrationToolV2;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Class to generate report for out of tolerance values and send report through email
    /// </summary>
    class OutOfToleranceCheck
    {
        DatabaseConnectMySQL db_connect = new DatabaseConnectMySQL();
        string filepath = "";                                                                                                              //Variable to store Tolerance file location
     
        /// <summary>
        /// Method to create Tolerance report using Microsoft Excel 
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="temp"></param>
        public void reportDetails(string sessionid, string temp)
        {
            string tolRepLoc = "";
            filepath = "";
            
            try
            {
                //Get tolerance report location from app config file
                tolRepLoc = (ConfigurationManager.AppSettings["tolerance-reportlocation"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
            //temp = start when report needs to be sent when session has just started.
            if (temp == "start")
            {
                try
                {
                    //Get session details to prepare Tolerance report
                    db_connect.GetSessionDetailsForReport(sessionid);
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

                filepath = tolRepLoc + "ToleranceReport-" + DateTime.Parse(db_connect.Start_Time).ToString("MMM") + "-" + DateTime.Parse(db_connect.Start_Time).Day.ToString() + "-" + DateTime.Parse(db_connect.Start_Time).Year.ToString() + " " + DateTime.Parse(db_connect.Start_Time).Hour.ToString() + "hrs-" + DateTime.Parse(db_connect.Start_Time).Minute.ToString() + "mins-" + DateTime.Parse(db_connect.Start_Time).Second.ToString() + "secs" + ".xls";

                if (File.Exists(filepath))
                {
                    sendEmail(sessionid, temp);
                    return;
                }
            }
            else if((temp != "start")&&(temp!="auto_exit"))
                MessageBox.Show("Please wait as Tolerance report creation is in progress.");

           
            try
            {
                //To get the count of tolerance values which are not within tolerance limits
                db_connect.OutOfTolValCount(sessionid);
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
                //To get session details from database to send report through email
                db_connect.GetSessionDetailsForReport(sessionid);
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


            /* Creating Excel Tolerance Report */
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show(DisplayMessages.excel_installincorrect, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                xlApp.DisplayAlerts = false;
                xlApp.Visible = false;
                xlApp.ScreenUpdating = false;

                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //Creating Sheet 1 
                Range myRange;
                ws.Name = "Report Details";
                myRange = ws.get_Range("A1");
                ws.Cells[1, 1] = db_connect.Rep_Title;
                ws.Cells[3, 1] = "Username ";
                 Range username_textformat = ws.Cells[3, 2];
                username_textformat.NumberFormat = "@";
                username_textformat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                ws.Cells[3, 2] = db_connect.Username;
                ws.Cells[4, 1] = "Session ID ";
                Range sessionid_textformat = ws.Cells[4, 2];
                sessionid_textformat.NumberFormat = "@";
                sessionid_textformat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                ws.Cells[4, 2] = sessionid;
                ws.Cells[5, 1] = "Start Date Time";
                if (db_connect.Start_Time == "")
                    ws.Cells[5, 2] = "";
                else
                    ws.Cells[5, 2] = db_connect.Start_Time;
                ws.Cells[6, 1] = "End Date Time";
                if (db_connect.End_Time == "")
                    ws.Cells[6, 2] = "";
                else
                    ws.Cells[6, 2] = db_connect.End_Time;
                ws.Cells[7, 1] = "Total Time";
                if ((db_connect.Start_Time == "") || (db_connect.End_Time == ""))
                    ws.Cells[7, 2] = "";
                else
                    ws.Cells[7, 2] = (Convert.ToDateTime(db_connect.End_Time) - Convert.ToDateTime(db_connect.Start_Time)).ToString();
              
                ws.Cells[8, 1] = "Panels Run Count";
                ws.Cells[8, 2] = db_connect.PanelRun;
                ws.Cells[9, 1] = "Panels Data Saved Count";
                ws.Cells[9, 2] = db_connect.PanelSavedData;
                ws.Cells[10, 1] = "Error Count";
                ws.Cells[10, 2] = db_connect.ErrCount;

                ws.Cells[1, 1].Font.Bold = true;
                ws.Cells[3, 1].Font.Bold = true;
                ws.Cells[4, 1].Font.Bold = true;
                ws.Cells[5, 1].Font.Bold = true;
                ws.Cells[6, 1].Font.Bold = true;
                ws.Cells[7, 1].Font.Bold = true;
                ws.Cells[8, 1].Font.Bold = true;
                ws.Cells[9, 1].Font.Bold = true;
                ws.Cells[10, 1].Font.Bold = true;
                ws.Name = "Contents";

                //Creating Sheet 2
                Microsoft.Office.Interop.Excel.Worksheet ws1 = xlWorkBook.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing)
                         as Microsoft.Office.Interop.Excel.Worksheet;
                myRange = ws1.get_Range("A1");
                ws1.Cells[1, 1] = "Tag Name";
                ws1.Cells[1, 2] = "Current Value";
                ws1.Cells[1, 3] = "Tolerance Status";
                ws1.Cells[1, 4] = "Upper Tolerance";
                ws1.Cells[1, 5] = "Lower Tolerance";
                ws1.Cells[1, 1].Font.Bold = true;
                ws1.Cells[1, 2].Font.Bold = true;
                ws1.Cells[1, 3].Font.Bold = true;
                ws1.Cells[1, 4].Font.Bold = true;
                ws1.Cells[1, 5].Font.Bold = true;
                ws1.Name = "Tolerance Report";

                if (db_connect.ErrCount > 0)
                {
                    //To get the count of tolerance values which are not within tolerance limits
                    try
                    {
                        db_connect.GetOutOfTolValues(sessionid);
                    }
                    catch (MySqlException mysqlex)
                    {
                        throw mysqlex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    // Transpose the keys to column A
                    myRange = ws1.get_Range("A2");
                    myRange.get_Resize(db_connect.Tag.Count(), 1).Value =
                                     ws1.Parent.Parent.Transpose(db_connect.Tag.ToArray());

                    myRange = ws1.get_Range("B2");
                    myRange.get_Resize(db_connect.ToleranceStatus.Count(), 1).Value =
                    ws1.Parent.Parent.Transpose(db_connect.ToleranceStatus.ToArray());

                    myRange = ws1.get_Range("C2");
                    myRange.get_Resize(db_connect.ActualValue.Count(), 1).Value =
                    ws1.Parent.Parent.Transpose(db_connect.ActualValue.ToArray());

                    myRange = ws1.get_Range("D2");
                    myRange.get_Resize(db_connect.UpperLimit.Count(), 1).Value =
                    ws1.Parent.Parent.Transpose(db_connect.UpperLimit.ToArray());

                    myRange = ws1.get_Range("E2");
                    myRange.get_Resize(db_connect.LowerLimit.Count(), 1).Value =
                    ws1.Parent.Parent.Transpose(db_connect.LowerLimit.ToArray());

                }
                ws1.Move(System.Reflection.Missing.Value, xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                /* Progressive Trends Calculation */
                
                db_connect.Panelids.Clear();

                for (int i = 1; i <= 16; i++)
                    db_connect.Panelids.Add(i);

                db_connect.Increasing.Clear(); db_connect.Decreasing.Clear();

                for (int j = 1; j <= 16; j++)
                {
                    for (int k = 1; k <= 21; k++)
                    {
                        db_connect.Lux.Clear(); 
                        db_connect.Cct.Clear(); 
                        db_connect.Rows = 0;

                        //Check if progressively ncreasing or decreasing values exist in database
                        try
                        {
                            db_connect.CheckForProgressiveTrends(j.ToString(), k.ToString());
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
                    }
                }
                Worksheet ws2 = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                ws2.Name = "Progressive Trend";
                if ((db_connect.Increasing.Count > 0) || (db_connect.Decreasing.Count > 0))
                {
                    myRange = ws2.get_Range("A1");
                    if (db_connect.Increasing.Count > 0)
                    {
                        myRange = ws2.get_Range("A2");
                        myRange.get_Resize(db_connect.Increasing.Count(), 1).Value = ws2.Parent.Parent.Transpose(db_connect.Increasing.ToArray());
                    }
                    if (db_connect.Decreasing.Count > 0)
                    {
                        myRange = ws2.get_Range("B2");
                        myRange.get_Resize(db_connect.Decreasing.Count(), 1).Value = ws2.Parent.Parent.Transpose(db_connect.Decreasing.ToArray());
                    }
                }

                ws2.Cells[1, 1] = "Progressively Increasing over last 6 values";
                ws2.Cells[1, 2] = "Progressively Decreasing over last 6 values";
                ws2.Cells[1, 1].Font.Bold = true;
                ws2.Cells[1, 2].Font.Bold = true;
                ws.Cells[11, 1] = "Progressively Increasing Count";
                ws.Cells[12, 1] = "Progressively Decreasing Count";
                ws.Cells[11, 1].Font.Bold = true;
                ws.Cells[12, 1].Font.Bold = true;
                ws.Cells[11, 2] = db_connect.Increasing.Count;
                ws.Cells[12, 2] = db_connect.Decreasing.Count;
           
                filepath = tolRepLoc + "ToleranceReport-" + DateTime.Parse(db_connect.Start_Time).ToString("MMM") + "-" + DateTime.Parse(db_connect.Start_Time).Day.ToString() + "-" + DateTime.Parse(db_connect.Start_Time).Year.ToString() + " " + DateTime.Parse(db_connect.Start_Time).Hour.ToString() + "hrs-" + DateTime.Parse(db_connect.Start_Time).Minute.ToString() + "mins-" + DateTime.Parse(db_connect.Start_Time).Second.ToString() + "secs" + ".xls";
                xlWorkBook.SaveAs(filepath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, true, true, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, false, false, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                Marshal.FinalReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
           
            //Call method to send email
            sendEmail(sessionid, temp);
        }

        /// <summary>
        /// Method to send email 
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="temp"></param>
        private void sendEmail(string sessionid, string temp)
        {
            string email_Sent = "";
             
            try
            {
                //Get email ids from database
                db_connect.GetEmailID();
                //Display message if no email ids have been stored
                if (db_connect.EmailID.Count == 0)
                {
                    MessageBox.Show(DisplayMessages.emailid_notfound);
                    Program.Err.ErrorLog("Form Name : " + this.GetType().Name + " , "+DisplayMessages.emailid_notfound);
                    return;
                }
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
                //Get smtp settings to send email
                db_connect.GetSMTPSettings();
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

            //Sending email
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(db_connect.SmtpServer);
                mail.From = new MailAddress(db_connect.SmtpEmail);
                foreach (var i in db_connect.EmailID)
                    mail.To.Add(i);
                mail.Subject = "DLE Calibration - " + db_connect.Start_Time ;
                mail.Body = "This is an auto-generated email. Please do not reply to this email.";
                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(filepath);
                mail.Attachments.Add(attachment1);
                SmtpServer.Port = Convert.ToInt32(db_connect.SmtpPort);//Port can be 587 or 465 or 25
                SmtpServer.EnableSsl = Convert.ToBoolean(db_connect.EnableSSL == "Yes" ? true : false); 
                //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new NetworkCredential(db_connect.SmtpEmail, db_connect.SmtpEmailPwd);
                SmtpServer.Send(mail);
                if((temp!="start")&&(temp!="auto_exit"))
                MessageBox.Show( DisplayMessages.emailsent_msg);
                email_Sent = "Yes";
            }
            catch (MySqlException mysql_ex)
            {
                MessageBox.Show(DisplayMessages.general_error + mysql_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + mysql_ex.GetType().ToString() + ", Target Site : " + mysql_ex.TargetSite + ",\tError Message : " + mysql_ex.Message + ", Inner Exception : " + mysql_ex.InnerException);
                email_Sent = "No";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                email_Sent = "No";
                return;
            }

            
            try
            {
                //Update email sent status detail in database
                db_connect.UpdateEmailSent(email_Sent, sessionid);
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
        }


    }
}
