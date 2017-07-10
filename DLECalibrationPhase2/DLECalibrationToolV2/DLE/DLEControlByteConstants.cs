using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLECalibrationToolV2.DLE
{
    /// <summary>
    /// This class contains byte array to control the Panels in DLE at different DMX values.
    /// </summary>
    class DLEControlByteConstants
    {
        #region Byte Array to control DLE

        public byte[] header = new byte[] { 65, 114, 116, 45, 78, 101, 116, 0, 0, 80, 0, 14, 72, 2, 0, 0, 0, 32 };
        public byte[] controlturnoff = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel1warm16 = new byte[] { 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel1warm64 = new byte[] { 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel1warm128 = new byte[] { 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel2warm16 = new byte[] { 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel2warm64 = new byte[] { 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel2warm128 = new byte[] { 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel3warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel3warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel3warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel4warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel4warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel4warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel5warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel5warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel5warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel6warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel6warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel6warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel7warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0 };
        byte[] controlpanel7warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0 };
        byte[] controlpanel7warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0 };

        byte[] controlpanel8warm16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16 };
        byte[] controlpanel8warm64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64 };
        byte[] controlpanel8warm128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128 };

        byte[] controlpanel1cool16 = new byte[] { 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel1cool64 = new byte[] { 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel1cool128 = new byte[] { 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel2cool16 = new byte[] { 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel2cool64 = new byte[] { 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel2cool128 = new byte[] { 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel3cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel3cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel3cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel4cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel4cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel4cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel5cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel5cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel5cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel6cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel6cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel6cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        byte[] controlpanel7cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0, 0, 0, 0, 0 };
        byte[] controlpanel7cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0, 0, 0, 0, 0 };
        byte[] controlpanel7cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0, 0, 0, 0, 0 };

        byte[] controlpanel8cool16 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 16, 0 };
        byte[] controlpanel8cool64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 64, 0 };
        byte[] controlpanel8cool128 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 128, 0 };

        byte[] controlpanel1combined64 = new byte[] { 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel2combined64 = new byte[] { 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel3combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel4combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel5combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel6combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] controlpanel7combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64, 0, 0, 0, 0 };
        byte[] controlpanel8combined64 = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 64, 64, 64 };

        #endregion


        #region List to store controlbyte array for each Panel

        List<byte[]> controlPanel1 = new List<byte[]>();
        List<byte[]> controlPanel2 = new List<byte[]>();
        List<byte[]> controlPanel3 = new List<byte[]>();
        List<byte[]> controlPanel4 = new List<byte[]>();
        List<byte[]> controlPanel5 = new List<byte[]>();
        List<byte[]> controlPanel6 = new List<byte[]>();
        List<byte[]> controlPanel7 = new List<byte[]>();
        List<byte[]> controlPanel8 = new List<byte[]>();

        #endregion

        /// <summary>
        /// This method contains byte arrays to control Panel 1 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel1Data()
        {
            controlPanel1.Clear();
            controlPanel1.Add(controlpanel1warm16);
            controlPanel1.Add(controlpanel1warm64);
            controlPanel1.Add(controlpanel1warm128);
            controlPanel1.Add(controlpanel1cool16);
            controlPanel1.Add(controlpanel1cool64);
            controlPanel1.Add(controlpanel1cool128);
            controlPanel1.Add(controlpanel1combined64);
            return controlPanel1;
        }

        /// <summary>
        ///  This method contains byte arrays to control Panel 2 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel2Data()
        {
            controlPanel2.Clear();
            controlPanel2.Add(controlpanel2warm16);
            controlPanel2.Add(controlpanel2warm64);
            controlPanel2.Add(controlpanel2warm128);
            controlPanel2.Add(controlpanel2cool16);
            controlPanel2.Add(controlpanel2cool64);
            controlPanel2.Add(controlpanel2cool128);
            controlPanel2.Add(controlpanel2combined64);
            return controlPanel2;
        }

        /// <summary>
        ///  This method contains byte arrays to control Panel 3 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel3Data()
        {
            controlPanel3.Clear();
            controlPanel3.Add(controlpanel3warm16);
            controlPanel3.Add(controlpanel3warm64);
            controlPanel3.Add(controlpanel3warm128);
            controlPanel3.Add(controlpanel3cool16);
            controlPanel3.Add(controlpanel3cool64);
            controlPanel3.Add(controlpanel3cool128);
            controlPanel3.Add(controlpanel3combined64);
            return controlPanel3;
        }

        /// <summary>
        ///  This method contains byte arrays to control Panel 4 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel4Data()
        {
            controlPanel4.Clear();
            controlPanel4.Add(controlpanel4warm16);
            controlPanel4.Add(controlpanel4warm64);
            controlPanel4.Add(controlpanel4warm128);
            controlPanel4.Add(controlpanel4cool16);
            controlPanel4.Add(controlpanel4cool64);
            controlPanel4.Add(controlpanel4cool128);
            controlPanel4.Add(controlpanel4combined64);
            return controlPanel4;
        }


        /// <summary>
        ///  This method contains byte arrays to control Panel 5 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel5Data()
        {
            controlPanel5.Clear();
            controlPanel5.Add(controlpanel5warm16);
            controlPanel5.Add(controlpanel5warm64);
            controlPanel5.Add(controlpanel5warm128);
            controlPanel5.Add(controlpanel5cool16);
            controlPanel5.Add(controlpanel5cool64);
            controlPanel5.Add(controlpanel5cool128);
            controlPanel5.Add(controlpanel5combined64);
            return controlPanel5;
        }


        /// <summary>
        ///  This method contains byte arrays to control Panel 6 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel6Data()
        {
            controlPanel6.Clear();
            controlPanel6.Add(controlpanel6warm16);
            controlPanel6.Add(controlpanel6warm64);
            controlPanel6.Add(controlpanel6warm128);
            controlPanel6.Add(controlpanel6cool16);
            controlPanel6.Add(controlpanel6cool64);
            controlPanel6.Add(controlpanel6cool128);
            controlPanel6.Add(controlpanel6combined64);
            return controlPanel6;
        }


        /// <summary>
        ///  This method contains byte arrays to control Panel 7 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel7Data()
        {
            controlPanel7.Clear();
            controlPanel7.Add(controlpanel7warm16);
            controlPanel7.Add(controlpanel7warm64);
            controlPanel7.Add(controlpanel7warm128);
            controlPanel7.Add(controlpanel7cool16);
            controlPanel7.Add(controlpanel7cool64);
            controlPanel7.Add(controlpanel7cool128);
            controlPanel7.Add(controlpanel7combined64);
            return controlPanel7;
        }


        /// <summary>
        ///  This method contains byte arrays to control Panel 8 at different DMX values
        /// </summary>
        /// <returns></returns>
        public List<byte[]> controlPanel8Data()
        {
            controlPanel8.Clear();
            controlPanel8.Add(controlpanel8warm16);
            controlPanel8.Add(controlpanel8warm64);
            controlPanel8.Add(controlpanel8warm128);
            controlPanel8.Add(controlpanel8cool16);
            controlPanel8.Add(controlpanel8cool64);
            controlPanel8.Add(controlpanel8cool128);
            controlPanel8.Add(controlpanel8combined64);
            return controlPanel8;
        }
    }
}
