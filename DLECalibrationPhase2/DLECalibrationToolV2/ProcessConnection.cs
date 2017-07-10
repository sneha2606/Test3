using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DLECalibrationToolV2
{
    /// <summary>
    /// Class to specify settings to make a WMI connection  
    /// </summary>
    class ProcessConnection
    {
        public ConnectionOptions ProcessConnectionOptions()
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }
       
        public ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        {
            ManagementScope connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }
    }
}
