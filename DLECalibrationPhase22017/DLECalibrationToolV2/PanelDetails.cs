using DLECalibrationToolV2.DLE;
using DLECalibrationToolV2.PI_AFSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Class to get panel details such as control panel data ,panel tags and panel tolerance tags.
    /// </summary>
    class PanelDetails
    {
        public string[] paneltags = new string[] { };
        public List<byte[]> controlpaneldata;

        /// <summary>
        /// Method to get the PI Tags,control data and PI Tolerance tags based on Panel and Measurement Location selected by the user 
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="pitoltag"></param>
        public void panelTags(string panel,PIToleranceTags pitoltag)
        {
            //controlpaneldata.Clear();
            Array.Clear(paneltags, 0, paneltags.Length);

            switch (panel.Substring(0,7))
            {
                case "Panel-1": controlpaneldata = new DLEControlByteConstants().controlPanel1Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel1Left();
                                    paneltags = PITags.Panel1Left();
                                }
                                else
                                {
                                    pitoltag.Panel1Right();
                                    paneltags = PITags.Panel1Right();
                                }
                                break;
                case "Panel-2": controlpaneldata = new DLEControlByteConstants().controlPanel2Data();
                                 if (panel.Substring(7) == "Left")
                                 {
                                    pitoltag.Panel2Left();
                                    paneltags = PITags.Panel2Left();
                                 }
                                else
                                {
                                    pitoltag.Panel2Right();
                                    paneltags = PITags.Panel2Right();
                                }
                                break;
                case "Panel-3": controlpaneldata = new DLEControlByteConstants().controlPanel3Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel3Left();
                                    paneltags = PITags.Panel3Left();
                                }
                                else
                                {
                                    pitoltag.Panel3Right();
                                    paneltags = PITags.Panel3Right();
                                }
                                break;
                case "Panel-4": controlpaneldata = new DLEControlByteConstants().controlPanel4Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel4Left();
                                    paneltags = PITags.Panel4Left();
                                }
                                else
                                {
                                    pitoltag.Panel4Right();
                                    paneltags = PITags.Panel4Right();
                                }
                                break;
                case "Panel-5": controlpaneldata = new DLEControlByteConstants().controlPanel5Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel5Left();
                                    paneltags = PITags.Panel5Left();
                                }
                                else
                                {
                                    pitoltag.Panel5Right();
                                    paneltags = PITags.Panel5Right();
                                }
                                break; ;
                case "Panel-6": controlpaneldata = new DLEControlByteConstants().controlPanel6Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel6Left();
                                    paneltags = PITags.Panel6Left();
                                }
                                else
                                {
                                    pitoltag.Panel6Right();
                                    paneltags = PITags.Panel6Right();
                                }
                                break;
                case "Panel-7": controlpaneldata = new DLEControlByteConstants().controlPanel7Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel7Left();
                                    paneltags = PITags.Panel7Left();
                                }
                                else
                                {
                                    pitoltag.Panel7Right();
                                    paneltags = PITags.Panel7Right();
                                }
                                break;
                case "Panel-8":controlpaneldata = new DLEControlByteConstants().controlPanel8Data();
                                if (panel.Substring(7) == "Left")
                                {
                                    pitoltag.Panel8Left();
                                    paneltags = PITags.Panel8Left();
                                }
                                else
                                {
                                    pitoltag.Panel8Right();
                                    paneltags = PITags.Panel8Right();
                                }
                                break;
            }
            
        }
    }
}
 