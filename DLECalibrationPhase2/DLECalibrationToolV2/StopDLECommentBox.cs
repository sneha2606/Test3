using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Form to display Comment Box to enter comments when User clicks on Stop button 
    /// </summary>
    public partial class StopDLECommentBox : Form
    {
        PIServer _piserver;
        // Variable used to store the reason for aborting the DLE
        public string stopbox_comments = "";
        string stopsessionComments_Tag = "";
     
        public StopDLECommentBox(PIServer piserver, string annotatetag)
        {
            InitializeComponent();
            _piserver = piserver;
            stopsessionComments_Tag = annotatetag;
        }

        private void StopDLECommentBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.exit_app_flag == 1)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //Display message if no reason is specified
            if (textBoxReason.Text == "")
                MessageBox.Show(Form.ActiveForm, DisplayMessages.dlestopbutton_click);
            //Display message if text entered is less than 10 characters
            else if (textBoxReason.Text.Length < 10)
                MessageBox.Show(Form.ActiveForm, DisplayMessages.comments_min10);
            else if (textBoxReason.Text != "")
            {
                stopbox_comments = textBoxReason.Text + Environment.NewLine;
                Program.comments =  textBoxReason.Text + Environment.NewLine;
                try
                {
                    var point = PIPoint.FindPIPoint(_piserver, stopsessionComments_Tag);
                    if (point != null)
                    {
                        //point.UpdateValue(new AFValue(Program.comments, AFTime.Now), AFUpdateOption.Insert);
                        point.UpdateValue(new AFValue(stopbox_comments, AFTime.Now), AFUpdateOption.Insert, AFBufferOption.BufferIfPossible);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException); return;  
                }
                this.Hide();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
