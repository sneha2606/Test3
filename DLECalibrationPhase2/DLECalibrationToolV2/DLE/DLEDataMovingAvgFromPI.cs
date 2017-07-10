using DLECalibrationToolV2;
using DLECalibrationToolV2.PI_AFSDK;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2.DLE
{
    /// <summary>
    /// This class is used to compute the moving avg - upper and lower tolerance limits. 
    /// Induvidual panel data are checked if they contain specified number of data(as declared in app config file) after the last time the panel tolerance data was updated. 
    /// Once the specified number of data has been received , the moving avg is computed for that panel . 
    /// The updated moving avg is stored in PI as well as in the Tolerance Excel sheet.
    /// </summary>
    class DLEDataMovingAvgFromPI
    {

        #region Variables to store Upper and Lower limit values
        string[] pitags = null;
        List<string> upperlimitvalues = new List<string>();
        List<string> lowerlimitvalues = new List<string>();
        List<double> UpperLimit_Lux_Sensor0 = new List<double>();
        List<double> LowerLimit_Lux_Sensor0 = new List<double>();
        List<double> UpperLimit_CCT_Sensor0 = new List<double>();
        List<double> LowerLimit_CCT_Sensor0 = new List<double>();
        List<double> UpperLimit_Lux_Sensor1 = new List<double>();
        List<double> LowerLimit_Lux_Sensor1 = new List<double>();
        List<double> UpperLimit_CCT_Sensor1 = new List<double>();
        List<double> LowerLimit_CCT_Sensor1 = new List<double>();
        List<double> UpperLimit_Lux_Sensor2 = new List<double>();
        List<double> LowerLimit_Lux_Sensor2 = new List<double>();
        List<double> UpperLimit_CCT_Sensor2 = new List<double>();
        List<double> LowerLimit_CCT_Sensor2 = new List<double>();
        #endregion

        #region Variables to calculate Moving avg
        Dictionary<string, int> panelDetails = new Dictionary<string, int>();
        int movingavg_count = 0;
        List<string> lastupdateddate = new List<string>();
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();
        PIToleranceTags pitoltag = new PIToleranceTags();
        string[] pitags_Array = new string[] { };                                                                                           //Array to store all PI tags
        double[] pitags_Array_values = new double[] { };
        List<int> panelid = new List<int>();
        List<int> movingavg = new List<int>();
        #endregion

        #region Excel variables
        Microsoft.Office.Interop.Excel.Application xlsApp;
        Workbook wb;
        #endregion


        /// <summary>
        /// Method to check if moving avg needs to be calculated
        /// </summary>
        public void CheckMovingAvg()
        {
            PISystems myPISystems = new PISystems();
            PISystem myPISystem = myPISystems.DefaultPISystem;
            panelid.Clear(); movingavg.Clear(); lastupdateddate.Clear();

            //Get moving avg count from App config file
            try
            {
                movingavg_count = Convert.ToInt32(ConfigurationManager.AppSettings["movingavg-count"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message+ Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get panel ids for all panels from database
            try
            {
                panelid = dbconnect.GetAllPanelIDs();
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get last updated date for each panel from db
            try
            {
                lastupdateddate = dbconnect.GetLastUpdatedTolDateForPanels();
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }

            //Get the PI Server name from database
            try
            {
                dbconnect.GetPIServerName();
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }


            try
            { 
                //Gets PI tags for all panel and checks if there are specified count number of values from last moving average updated date.
                foreach (var i in panelid)
                {
                    switch (i)
                    {
                        case 1: pitags = PITags.Panel1Left();
                            break;
                        case 2: pitags = PITags.Panel1Right();
                            break;
                        case 3: pitags = PITags.Panel2Left();
                            break;
                        case 4: pitags = PITags.Panel2Right();
                            break;
                        case 5: pitags = PITags.Panel3Left();
                            break;
                        case 6: pitags = PITags.Panel3Right();
                            break;
                        case 7: pitags = PITags.Panel4Left();
                            break;
                        case 8: pitags = PITags.Panel4Right();
                            break;
                        case 9: pitags = PITags.Panel5Left();
                            break;
                        case 10: pitags = PITags.Panel5Right();
                            break;
                        case 11: pitags = PITags.Panel6Left();
                            break;
                        case 12: pitags = PITags.Panel6Right();
                            break;
                        case 13: pitags = PITags.Panel7Left();
                            break;
                        case 14: pitags = PITags.Panel7Right();
                            break;
                        case 15: pitags = PITags.Panel8Left();
                            break;
                        case 16: pitags = PITags.Panel8Right();
                            break;
                    }


                    int temp = 0;
                    PIPoint myPIPoint;
                    PIServer myPIServer = PIServer.FindPIServer(myPISystem, dbconnect.PiServer.ToString());
                    foreach (string k in pitags)
                    {
                        //Search for  PI point , get values for the PI point . Check if number of PI point values is greater than the specified movingavg count
                        myPIPoint = PIPoint.FindPIPoint(myPIServer, k);
                        AFValues value = myPIPoint.RecordedValues(new AFTimeRange(lastupdateddate[i - 1], DateTime.Now.ToString()), AFBoundaryType.Inside, "", false);
                        if (value.Count >= movingavg_count)
                            temp++;
                    }
                    // Checking if all 42 tags in a panel measurement location have number of values greater than specified moving avg
                    if (temp == 42)         //42 tags in each panel - 21 lux 21 cct
                        movingavg.Add(i);
                }

                
                //If any panel has data more than the specified moving avg count , then moving average will be calculated for that panel.
                if (movingavg.Count > 0)
                {
                    foreach (var i in movingavg)
                    {
                        CalculateMovingAvg(i);
                        UpdateExcelSheet(i);
                    }
                     //Update moving avg calculation date in sql       
                     dbconnect.UpdateLastUpdatedDateForMovingAvg(movingavg);
                }
                MessageBox.Show(Form.ActiveForm, DisplayMessages.movingavg_calccomplete);
            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.mysql_error + mysqlex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + mysqlex.GetType().ToString() + ", Target Site : " + mysqlex.TargetSite + ",\tError Message : " + mysqlex.Message + ", Inner Exception : " + mysqlex.InnerException);
                return;
            }
            catch (COMException com_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.tolerancefilelocation_incorrect + com_ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + com_ex.GetType().ToString() + ", Target Site : " + com_ex.TargetSite + ",\tError Message : " + com_ex.Message + ", Inner Exception : " + com_ex.InnerException);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message + Environment.NewLine + Environment.NewLine + DisplayMessages.movingavg_failed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Class: " + this.GetType().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }


        /// <summary>
        /// Method to calculate Moving Avg and update to PI tags
        /// </summary>
        /// <param name="panelid"></param>
        private void CalculateMovingAvg(int panelid)
        {
            PISystems myPISystems = new PISystems();
            PISystem myPISystem = myPISystems.DefaultPISystem;
            PIPoint myPIPoint;
            PIServer myPIServer = PIServer.FindPIServer(myPISystem, dbconnect.PiServer.ToString());
            pitags = null;

            //Get Tolerance tags for each panel.
            switch (panelid)
            {
                case 1: pitoltag.Panel1Left(); pitags = PITags.Panel1Left();
                    break;
                case 2: pitoltag.Panel1Right(); pitags = PITags.Panel1Right();
                    break;
                case 3: pitoltag.Panel2Left(); pitags = PITags.Panel2Left();
                    break;
                case 4: pitoltag.Panel2Right(); pitags = PITags.Panel2Right();
                    break;
                case 5: pitoltag.Panel3Left(); pitags = PITags.Panel3Left();
                    break;
                case 6: pitoltag.Panel3Right(); pitags = PITags.Panel3Right();
                    break;
                case 7: pitoltag.Panel4Left(); pitags = PITags.Panel4Left();
                    break;
                case 8: pitoltag.Panel4Right(); pitags = PITags.Panel4Right();
                    break;
                case 9: pitoltag.Panel5Left(); pitags = PITags.Panel5Left();
                    break;
                case 10: pitoltag.Panel5Right(); pitags = PITags.Panel5Right();
                    break;
                case 11: pitoltag.Panel6Left(); pitags = PITags.Panel6Left();
                    break;
                case 12: pitoltag.Panel6Right(); pitags = PITags.Panel6Right();
                    break;
                case 13: pitoltag.Panel7Left(); pitags = PITags.Panel7Left();
                    break;
                case 14: pitoltag.Panel7Right(); pitags = PITags.Panel7Right();
                    break;
                case 15: pitoltag.Panel8Left(); pitags = PITags.Panel8Left();
                    break;
                case 16: pitoltag.Panel8Right(); pitags = PITags.Panel8Right();
                    break;
            }

            upperlimitvalues.Clear();
            lowerlimitvalues.Clear();
            AFValues value;

            /*
             Compute the Upper and Lower Tolerance Limit based on below formula
             Upper Tolerance Limit = Avg(Tag) + 3 * Std Dev(Tag)
             Lower Tolerance Limit = Avg(Tag) - 3 * Std Dev(Tag)
             */
            for (int i = 0; i < pitags.Length; i++)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, pitags[i]);
                value = myPIPoint.RecordedValues(new AFTimeRange(lastupdateddate[panelid-1], DateTime.Now.ToString()), AFBoundaryType.Inside, "", false);
                List<Double> value_decimal = new List<Double>();
               foreach (var j in value)
                   value_decimal.Add(Convert.ToDouble(j.ToString()));
               double average = value_decimal.Average();
               double sumOfSquaresOfDifferences = value_decimal.Select(val => (Convert.ToDouble(val.ToString()) - average) * (Convert.ToDouble(val.ToString()) - average)).Sum();
               double sd = Math.Sqrt(sumOfSquaresOfDifferences / (value_decimal.Count)); //Population Std dev
               double UpperLimit = average + 3 * sd;
               double LowerLimit = average - 3 * sd;
               upperlimitvalues.Add(UpperLimit.ToString());
               lowerlimitvalues.Add(LowerLimit.ToString());
            }

            UpperLimit_Lux_Sensor0.Clear();
            UpperLimit_CCT_Sensor0.Clear();
            UpperLimit_Lux_Sensor1.Clear();
            UpperLimit_CCT_Sensor1.Clear();
            UpperLimit_Lux_Sensor2.Clear();
            UpperLimit_CCT_Sensor2.Clear();

            LowerLimit_Lux_Sensor0.Clear();
            LowerLimit_CCT_Sensor0.Clear();
            LowerLimit_Lux_Sensor1.Clear();
            LowerLimit_CCT_Sensor1.Clear();
            LowerLimit_CCT_Sensor2.Clear();
            LowerLimit_CCT_Sensor2.Clear();


            //Update Tolerance tags values in PI
            for (int i = 0, j = 0; i < upperlimitvalues.Count; i = i + 6, j++)
            {

                PIPoint point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor0LUX_UpperLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(upperlimitvalues[i], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor0CCT_UpperLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(upperlimitvalues[i + 1], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor1LUX_UpperLimit_tag[j]);
                if (point != null)
                {
                      point.UpdateValue(new AFValue(upperlimitvalues[i + 2], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor1CCT_UpperLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(upperlimitvalues[i + 3], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor2LUX_UpperLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(upperlimitvalues[i + 4], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor2CCT_UpperLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(upperlimitvalues[i + 5], AFTime.Now), AFUpdateOption.Insert);
                }

                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor0LUX_LowerLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(lowerlimitvalues[i], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor0CCT_LowerLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(lowerlimitvalues[i + 1], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor1LUX_LowerLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(lowerlimitvalues[i + 2], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor1CCT_LowerLimit_tag[j]);
                if (point != null)
                {
                    point.UpdateValue(new AFValue(lowerlimitvalues[i + 3], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor2LUX_LowerLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(lowerlimitvalues[i + 4], AFTime.Now), AFUpdateOption.Insert);
                }
                point = PIPoint.FindPIPoint(dbconnect.PiServer, pitoltag.panelMPTSensor2CCT_LowerLimit_tag[j]);
                if (point != null)
                {
                     point.UpdateValue(new AFValue(lowerlimitvalues[i + 5], AFTime.Now), AFUpdateOption.Insert);
                }

                UpperLimit_Lux_Sensor0.Add(Convert.ToDouble(upperlimitvalues[i]));
                UpperLimit_CCT_Sensor0.Add(Convert.ToDouble(upperlimitvalues[i + 1]));

                UpperLimit_Lux_Sensor1.Add(Convert.ToDouble(upperlimitvalues[i + 2]));
                UpperLimit_CCT_Sensor1.Add(Convert.ToDouble(upperlimitvalues[i + 3]));

                UpperLimit_Lux_Sensor2.Add(Convert.ToDouble(upperlimitvalues[i + 4]));
                UpperLimit_CCT_Sensor2.Add(Convert.ToDouble(upperlimitvalues[i + 5]));

                LowerLimit_Lux_Sensor0.Add(Convert.ToDouble(lowerlimitvalues[i]));
                LowerLimit_CCT_Sensor0.Add(Convert.ToDouble(lowerlimitvalues[i + 1]));

                LowerLimit_Lux_Sensor1.Add(Convert.ToDouble(lowerlimitvalues[i + 2]));
                LowerLimit_CCT_Sensor1.Add(Convert.ToDouble(lowerlimitvalues[i + 3]));

                LowerLimit_Lux_Sensor2.Add(Convert.ToDouble(lowerlimitvalues[i + 4]));
                LowerLimit_CCT_Sensor2.Add(Convert.ToDouble(lowerlimitvalues[i + 5]));
            }

        }


        /// <summary>
        /// Method to update excel sheet
        /// </summary>
        /// <param name="panelid"></param>
        private void UpdateExcelSheet(int panelid)
        {
            string panel = dbconnect.GetPanelNameFromID(panelid);
            xlsApp = new Microsoft.Office.Interop.Excel.Application();
            string panelnumber;
            Worksheet ws;
            Workbooks wbs;
            wbs = xlsApp.Workbooks;

            //Open Tolerance Excel File
            try
            {
                wb = wbs.Open(CalibrationForm.tol_FileLoc, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Thread.Sleep(2000);
            }
            catch (COMException com_ex)
            {
                throw com_ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Sheets sheets = wb.Worksheets;
            panelnumber = panel.Substring(0, 5) + panel.Substring(6, 1);
            ws = (Worksheet)wb.Sheets[panelnumber];

            //Update Excel Sheet with new Tolerance values
            //Updating for Measurement point Left
            if (panel.Substring(7) == "Left")
            {
                Range row = ws.get_Range("G2:G8", Type.Missing);
                int j = 0;
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G9:G15", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G16:G22", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G23:G29", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G30:G36", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G37:G43", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I2:I8", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I9:I15", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I16:I22", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I23:I29", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I30:I36", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I37:I43", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor2[j];
                    j++;
                }
                xlsApp.DisplayAlerts = false;
                xlsApp.Application.ActiveWorkbook.Save();
                Thread.Sleep(1000);
                Marshal.ReleaseComObject(row);
            }
            //Updating for Measurement point Right
            else
            {
                int j = 0;
                Range row = ws.get_Range("G44:G50", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G51:G57", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G58:G64", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G65:G71", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G72:G78", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_Lux_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("G79:G85", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = UpperLimit_CCT_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I44:I50", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I51:I57", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor0[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I58:I64", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I65:I71", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor1[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I72:I78", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_Lux_Sensor2[j];
                    j++;
                }
                j = 0;
                row = ws.get_Range("I79:I85", Type.Missing);
                foreach (Range r in row)
                {
                    r.Cells.Value = LowerLimit_CCT_Sensor2[j];
                    j++;
                }
                xlsApp.DisplayAlerts = false;
                xlsApp.Application.ActiveWorkbook.Save();
                Thread.Sleep(1000);
                Marshal.ReleaseComObject(row);
            }
            //Kill excel processes
            for (int i = 1; i <= sheets.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Worksheet sheet = sheets.Item[i];
                if (sheet != null)
                    Marshal.ReleaseComObject(sheet);
            }

            Marshal.ReleaseComObject(sheets);
            wb.Close(false);
            wbs.Close();
            xlsApp.Quit();
            Marshal.ReleaseComObject(wb);
            Marshal.ReleaseComObject(wbs);
            Marshal.ReleaseComObject(xlsApp);

            foreach (Process clsProcess in Process.GetProcesses())
            { 
                if (clsProcess.ProcessName.Equals("EXCEL"))
                {
                    clsProcess.Kill();
                    break;
                }
            }
        }


    }
}
