using OSIsoft.AF.PI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DLECalibrationToolV2.PI_AFSDK
{
    /// <summary>
    /// Class for PI Connectivity
    /// </summary>
    class PIConnector
    {
        PIServers _piServers;
        PIServer _piServer;

        /// <summary>
        /// Constructor that gets the list of available PI servers
        /// </summary>
        /// <param name="ServerName"></param>
        public PIConnector(string ServerName)
        {
            _piServers = new PIServers();
            if (!_piServers.Contains(ServerName))
            {
                throw new ArgumentException(DisplayMessages.piserver_error);
            }
            _piServer = _piServers.First(s => s.Name == ServerName);
        }


        /// <summary>
        /// Connects to PI server
        /// </summary>
        /// <returns></returns>
        public PIServer Connect()
        {
            _piServer.Connect();
            return _piServer;
        }


        /// <summary>
        /// Disconnects from PI Server
        /// </summary>
        public void Disconnect()
        {
            _piServer.Disconnect();
        }

        /// <summary>
        /// Checks if PI server is connected or not
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return _piServer.ConnectionInfo.IsConnected;
        }
    }
}
