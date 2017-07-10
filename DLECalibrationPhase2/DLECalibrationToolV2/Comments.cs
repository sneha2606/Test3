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
    /// Form to enter the annotation comments by user 
    /// </summary>
    public partial class Comments : Form
    {
        string annotate = "";
        PIServer _piServer;
        int source;
        bool btnSubmitWasClicked = false;

        public Comments(string annotatetag, string message, PIServer piserver, int state)
        {
            InitializeComponent();
            annotate = annotatetag;
            _piServer = piserver;
            source = state;
            label_msg.Text = "";
            label_msg.Text = message;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Program.comments = textBox_comments.Text;
            try
            {
                if (Program.comments.Length < 10)
                {
                    //Display message if comments contain less than 10 characters
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.comments_min10);
                    return;
                }
                //Source=1 when comments form is called when Exit session button is clicked
                if (source == 1)
                {
                    if (!string.IsNullOrWhiteSpace(Program.comments))
                    {
                        Program.comments = "Reason for session exit without running panels: " + textBox_comments.Text + Environment.NewLine;
                        btnSubmitWasClicked = true;
                        this.Hide();
                        this.Close();
                    }
                    else
                        MessageBox.Show(Form.ActiveForm, DisplayMessages.sessionexit_reason);
                }
                //Comments form called when panel run is over and user wants to enter comments - not mandatory for user to enter comments
                else 
                {
                     if(!string.IsNullOrWhiteSpace(Program.comments))
                     {
                        btnSubmitWasClicked = true;
                        var point = PIPoint.FindPIPoint(_piServer, annotate);
                        if (point != null)
                            point.UpdateValue(new AFValue(Program.comments, AFTime.Now), AFUpdateOption.Insert, AFBufferOption.BufferIfPossible);
                            //point.UpdateValue(new AFValue(Program.comments, AFTime.Now), AFUpdateOption.Insert);
                        this.Hide();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, DisplayMessages.general_error + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Err.ErrorLog("Form Name : " + this.FindForm().Name + ", Exception Type : " + ex.GetType().ToString() + ", Target Site : " + ex.TargetSite + ",\tError Message : " + ex.Message + ", Inner Exception : " + ex.InnerException);
                return;
            }
        }

        private void AnnotationBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Source=1 when comments form is called when Exit session button is clicked
            if (source == 1)
            {
                if ((((textBox_comments.TextLength < 1) || (textBox_comments.TextLength > 10)) || ((textBox_comments.TextLength < 10))) && (btnSubmitWasClicked == false))
                {
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.sessionexit_reason);
                    e.Cancel = true;
                }
                else if (btnSubmitWasClicked == true)
                {
                    e.Cancel = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Source=1 when comments form is called when Exit session button is clicked
            if (source == 1)
            {
                if ((((textBox_comments.TextLength < 1) || (textBox_comments.TextLength > 10)) || ((textBox_comments.TextLength < 10))) && (btnSubmitWasClicked == false))
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.sessionexit_reason);
                else if (btnSubmitWasClicked)
                      return;
            }
            else if (source == 0)
            {
                this.Hide();
                this.Close();
            }  
        }

    }
}
