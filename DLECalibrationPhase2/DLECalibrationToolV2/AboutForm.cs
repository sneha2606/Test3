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
    /// Form to display About the DLE Calibration 
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            //Contents to be displayed in About form
            textBoxAbout.Text = "\tDLE Calibration and Data Logging Tool is used to control each of the Panels from Panel 1 to Panel 8 independently which are present in the Day Light Emulator device , collect data from sensors which are connected to Konica Minolta device and store the data in PI System." + Environment.NewLine;
            textBoxAbout.Text += "\tThe application is interfaced with Konica Minolta CL-200A device to fetch the CCT (Corelated Colour Temperature ) and Lux (Kelvin ) values for all 8 panels for Warm(Yellow Light), Cool(White Light) and Combined(Yellow + White Light) conditions at DMX values of 16,64 and 128 using 3 sensors (Sensor 0 - positioned at bottom , Sensor 1- positioned in the middle and Sensor 2- positioned at the Top) at two Measurement Locations - Left and Right for each panel.";
            textBoxAbout.Text += "The sensor data collected is stored in PI Server for further analysis.";
            textBoxAbout.Text += "The data collected from the sensors is compared against Tolerance limits - Upper and Lower limits. The data which is outside the Tolerance limits - Lower than the Lower limit and Higher than the Upper Limit will be highlighted in different colours in the table.";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
   
    }
}
