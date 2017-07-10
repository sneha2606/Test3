using DLECalibrationToolV2;
using MySql.Data.MySqlClient;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.PI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2.PI_AFSDK
{
    /// <summary>
    /// Class to store PI Tolerance tags and get tolerance data based on Panel selected by User. 
    /// </summary>
    class PIToleranceTags
    {
        #region List to store PI tags for panel
        public List<string> panelMPTSensor0LUX_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor0CCT_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor1LUX_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor1CCT_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor2LUX_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor2CCT_UpperLimit_tag = new List<string>();
        public List<string> panelMPTSensor0LUX_LowerLimit_tag = new List<string>();
        public List<string> panelMPTSensor0CCT_LowerLimit_tag = new List<string>();
        public List<string> panelMPTSensor1LUX_LowerLimit_tag = new List<string>();
        public List<string> panelMPTSensor1CCT_LowerLimit_tag = new List<string>();
        public List<string> panelMPTSensor2LUX_LowerLimit_tag = new List<string>();
        public List<string> panelMPTSensor2CCT_LowerLimit_tag = new List<string>();
        #endregion

        #region Array to store Upper and Lower Limit Values from PI
        public double[] panelMPTSensor0LUX_UpperLimit_values = new double[7];
        public double[] panelMPTSensor0CCT_UpperLimit_values = new double[7];
        public double[] panelMPTSensor1LUX_UpperLimit_values = new double[7];
        public double[] panelMPTSensor1CCT_UpperLimit_values = new double[7];
        public double[] panelMPTSensor2LUX_UpperLimit_values = new double[7];
        public double[] panelMPTSensor2CCT_UpperLimit_values = new double[7];
        public double[] panelMPTSensor0LUX_LowerLimit_values = new double[7];
        public double[] panelMPTSensor0CCT_LowerLimit_values = new double[7];
        public double[] panelMPTSensor1LUX_LowerLimit_values = new double[7];
        public double[] panelMPTSensor1CCT_LowerLimit_values = new double[7];
        public double[] panelMPTSensor2LUX_LowerLimit_values = new double[7];
        public double[] panelMPTSensor2CCT_LowerLimit_values = new double[7];
        #endregion

        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        /// <summary>
        /// Function to get Tags for Panel-1 Left
        /// </summary>
        public void Panel1Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }

        /// <summary>
        /// Function to get Tags for Panel-1 Right
        /// </summary>
        public void Panel1Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL1.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");


        }

        /// <summary>
        /// Function to get Tags for Panel-2 Left
        /// </summary>
        public void Panel2Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");


        }

        /// <summary>
        /// Function to get Tags for Panel-2 Right
        /// </summary>
        public void Panel2Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL2.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-3 Left
        /// </summary>
        public void Panel3Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");


        }

        /// <summary>
        /// Function to get Tags for Panel-3 Right
        /// </summary>
        public void Panel3Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL3.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-4 Left
        /// </summary>
        public void Panel4Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }

        /// <summary>
        /// Function to get Tags for Panel-4 Right
        /// </summary>
        public void Panel4Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL4.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }

        /// <summary>
        /// Function to get Tags for Panel-5 Left
        /// </summary>
        public void Panel5Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-5 Right
        /// </summary>
        public void Panel5Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL5.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-6 Left
        /// </summary>
        public void Panel6Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-6 Right
        /// </summary>
        public void Panel6Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL6.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-7 Left
        /// </summary>
        public void Panel7Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to get Tags for Panel-7 Right
        /// </summary>
        public void Panel7Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL7.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
        }


        /// <summary>
        /// Function to get Tags for Panel-8 Left
        /// </summary>
        public void Panel8Left()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT1.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }

        /// <summary>
        /// Function to get Tags for Panel-8 Right
        /// </summary>
        public void Panel8Right()
        {
            clearvalues();

            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor0LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR0_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor0LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR0_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor0CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR0_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR0_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor0CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR0_DMX_64.CCT_LOW_TOL_LIM");


            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor1LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR1_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor1LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR1_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor1CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR1_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR1_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor1CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR1_DMX_64.CCT_LOW_TOL_LIM");

            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_16.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_64.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_128.LUX_UP_TOL_LIM");
            panelMPTSensor2LUX_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR2_DMX_64.LUX_UP_TOL_LIM");

            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_16.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_128.LUX_LOW_TOL_LIM");
            panelMPTSensor2LUX_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR2_DMX_64.LUX_LOW_TOL_LIM");

            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_16.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_64.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_128.CCT_UP_TOL_LIM");
            panelMPTSensor2CCT_UpperLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR2_DMX_64.CCT_UP_TOL_LIM");

            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.WARM.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_16.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COOL.SENSOR2_DMX_128.CCT_LOW_TOL_LIM");
            panelMPTSensor2CCT_LowerLimit_tag.Add("SG.CR11.DLECT.PANEL8.MPT2.COMBINED.SENSOR2_DMX_64.CCT_LOW_TOL_LIM");

        }


        /// <summary>
        /// Function to clear values of tags and array values
        /// </summary>
        private void clearvalues()
        {
            panelMPTSensor0CCT_LowerLimit_tag.Clear();
            panelMPTSensor0CCT_UpperLimit_tag.Clear();
            panelMPTSensor0LUX_LowerLimit_tag.Clear();
            panelMPTSensor0LUX_UpperLimit_tag.Clear();

            panelMPTSensor1CCT_LowerLimit_tag.Clear();
            panelMPTSensor1CCT_UpperLimit_tag.Clear();
            panelMPTSensor1LUX_LowerLimit_tag.Clear();
            panelMPTSensor1LUX_UpperLimit_tag.Clear();

            panelMPTSensor2CCT_LowerLimit_tag.Clear();
            panelMPTSensor2CCT_UpperLimit_tag.Clear();
            panelMPTSensor2LUX_LowerLimit_tag.Clear();
            panelMPTSensor2LUX_UpperLimit_tag.Clear();

            Array.Clear(panelMPTSensor0CCT_LowerLimit_values, 0, panelMPTSensor0CCT_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor0CCT_UpperLimit_values, 0, panelMPTSensor0CCT_UpperLimit_values.Length);
            Array.Clear(panelMPTSensor0LUX_LowerLimit_values, 0, panelMPTSensor0LUX_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor0LUX_UpperLimit_values, 0, panelMPTSensor0LUX_UpperLimit_values.Length);
            Array.Clear(panelMPTSensor1CCT_LowerLimit_values, 0, panelMPTSensor1CCT_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor1CCT_UpperLimit_values, 0, panelMPTSensor1CCT_UpperLimit_values.Length);
            Array.Clear(panelMPTSensor1LUX_LowerLimit_values, 0, panelMPTSensor1LUX_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor1LUX_UpperLimit_values, 0, panelMPTSensor1LUX_UpperLimit_values.Length);
            Array.Clear(panelMPTSensor2CCT_LowerLimit_values, 0, panelMPTSensor2CCT_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor2CCT_UpperLimit_values, 0, panelMPTSensor2CCT_UpperLimit_values.Length);
            Array.Clear(panelMPTSensor2LUX_LowerLimit_values, 0, panelMPTSensor2LUX_LowerLimit_values.Length);
            Array.Clear(panelMPTSensor2LUX_UpperLimit_values, 0, panelMPTSensor2LUX_UpperLimit_values.Length);
            
        }


        /// <summary>
        /// Function to get Tolerance Values from PI
        /// </summary>
        public void GetToleranceValuesFromPI()
        {
            //Get the PIPoints from the PIServer on the local computer 
            PISystems myPISystems = new PISystems();
            PISystem myPISystem = myPISystems.DefaultPISystem;

            try
            {
                dbconnect.GetPIServerName();
            }
            catch (MySqlException mysqlex)
            {
                throw mysqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            } 

            PIServer myPIServer = PIServer.FindPIServer(myPISystem,dbconnect.PiServer.ToString());
            PIPoint myPIPoint;

            //Get the lower and upper tolerance values of tags from PI
            int j = 0;
            //SENSOR-0 CCT Lower Limit Values
            foreach (string i in panelMPTSensor0CCT_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor0CCT_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-0 CCT Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor0CCT_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor0CCT_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-0 Lux Lower Limit Values
            j = 0;
            foreach (string i in panelMPTSensor0LUX_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor0LUX_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-0 Lux Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor0LUX_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor0LUX_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-1 CCT Lower Limit Values
            j = 0;
            foreach (string i in panelMPTSensor1CCT_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor1CCT_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-1 CCT Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor1CCT_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor1CCT_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-1 Lux Lower Limit Values
            j = 0;
            foreach (string i in panelMPTSensor1LUX_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor1LUX_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-1 Lux Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor1LUX_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor1LUX_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-2 CCT Lower Limit Values
            j = 0;
            foreach (string i in panelMPTSensor2CCT_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor2CCT_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-2 CCT Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor2CCT_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor2CCT_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-2 Lux Lower Limit Values
            j = 0;
            foreach (string i in panelMPTSensor2LUX_LowerLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor2LUX_LowerLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
            //SENSOR-2 Lux Upper Limit Values
            j = 0;
            foreach (string i in panelMPTSensor2LUX_UpperLimit_tag)
            {
                myPIPoint = PIPoint.FindPIPoint(myPIServer, i);
                AFValue value = myPIPoint.CurrentValue();
                panelMPTSensor2LUX_UpperLimit_values[j] = Convert.ToDouble(value.ToString());
                j++;
            }
        }

    }
}
