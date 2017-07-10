using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to display SQC Charts based on Panel and Measurement Location selected
    /// </summary>
    public partial class TrendsForm : Form
    {
        string sqcCharts_FileLoc = "";

        public TrendsForm()
        {
            InitializeComponent();
        }

        private void btnShowdisplay_Click(object sender, EventArgs e)
        {
            try
            {
                //Get SQC charts location from app config file
                sqcCharts_FileLoc = ConfigurationManager.AppSettings["sqccharts-filelocation"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.appsettings_configerror + " " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + "App Settings Error : " + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;

            }

            //Display message if any mandatory fields are left blank
            labelLuxCCTError.Text = "";
            labelMPTLocError.Text = "";
            labelPanelError.Text = "";
            if ((comboBoxLuxCCT.Text == "") || (comboBoxMptLoc.Text == "") || (comboBoxPanel.Text == ""))
            {
                if (comboBoxLuxCCT.Text == "")
                    labelLuxCCTError.Text = DisplayMessages.luxcct_mandatory;
                if (comboBoxMptLoc.Text == "")
                    labelMPTLocError.Text = DisplayMessages.mptloc_mandatory;
                if (comboBoxPanel.Text == "")
                    labelPanelError.Text = DisplayMessages.panel_mandatory;
                return;
            }

            try
            {
                //Based on user selection , display correspoding Process Book display
                switch (comboBoxPanel.Text)
                {
                    case "Panel-1": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL1-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL1-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL1-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL1-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-2": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL2-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL2-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL2-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL2-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-3": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL3-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL3-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL3-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL3-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-4": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL4-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL4-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL4-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL4-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-5": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL5-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL5-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL5-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL5-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-6": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL6-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL6-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL6-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL6-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-7": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL7-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL7-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL7-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL7-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                    case "Panel-8": switch (comboBoxMptLoc.Text)
                        {
                            case "Left": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL8-LEFT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL8-LEFT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                            case "Right": switch (comboBoxLuxCCT.Text)
                                {
                                    case "CCT": Process.Start(sqcCharts_FileLoc + "SQC-PANEL8-RIGHT-ERD-CCT.pdi");
                                        break;
                                    case "Lux": Process.Start(sqcCharts_FileLoc + "SQC-PANEL8-RIGHT-ERD-LUX.pdi");
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            catch (Win32Exception win32_ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.sqcchartslocation_incorrect + win32_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + win32_ex.GetType().ToString() + ", Target Site : " + win32_ex.TargetSite + ",\tError Message : " + win32_ex.Message + ", Inner Exception : " + win32_ex.InnerException);
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
