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
    /// Form to enter annotation to PI tags displayed in GridView in main Calibration form
    /// </summary>
    public partial class AnnotatePITag : Form
    {
        PIServer _piServer;
        string tag;
        
        public AnnotatePITag(string tagname,  PIServer piServer)
        {
            InitializeComponent();
            label_tagcomments.Text = "Enter Annotation for tag " + tagname;
            _piServer = piServer;
            tag = tagname;
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Annotate.Text.Length < 10)
                {
                    //Display message if comments contain less than 10 characters
                    MessageBox.Show(Form.ActiveForm, DisplayMessages.comments_min10);
                    return;
                }
                else
                {
                    //Find PI point
                    var point = PIPoint.FindPIPoint(_piServer, tag);
                    if (point != null)
                    {
                        //Get snapshot value of point from PI
                        AFValue value = point.CurrentValue();

                        //Add annotation to the snapshot value
                        value.SetAnnotation(textBox_Annotate.Text);

                        /*Update annotation to existing PI snapshot value - 
                         Replace Only Option replaces an existing value in the archive at the specified time. 
                         If no existing value is found, the passed value is ignored.
                        */
                        point.UpdateValue(value, AFUpdateOption.ReplaceOnly);
                        CalibrationForm.tags.Add(tag);
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

    }
}
