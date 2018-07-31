using DLECalibrationToolV2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLECalibrationToolV2.DLE
{
    /// <summary>
    /// This class contains methods to create the byte array and send UDP DMX packet to control the DLE
    /// </summary>
    class DLEControl
    {
        DatabaseConnectMySQL dbconnect = new DatabaseConnectMySQL();

        /// <summary>
        ///  Method to create a combined byte array using the Header and Control byte arrays 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public byte[] createbytearray(byte[] header, byte[] control)
        {
            //Header array + Control byte array copy to new array(combined)
            byte[] combined = new byte[header.Length + control.Length];
            System.Buffer.BlockCopy(header, 0, combined, 0, header.Length);
            System.Buffer.BlockCopy(control, 0, combined, header.Length, control.Length);
            return combined;
        }


        /// <summary>
        /// Method to send the UDP DMX packet to the Blizzard device
        /// </summary>
        /// <param name="dmxdatagram"></param>
        public void senddpdmxmessage(byte[] dmxdatagram)
        {
            UdpClient client = null;
            IPEndPoint ep = null;

            client = new UdpClient();

           try
            {
                //Get artnet settings from database
                dbconnect.GetARTNetSettings();
            }
           catch (MySqlException mysqlex)
           {
               throw mysqlex;
           } 
           catch (Exception ex)
           {
               throw ex;
           }  


            try
            {
                //Initialise IPEndPoint class with ip address and udp port
                ep = new IPEndPoint(IPAddress.Parse(dbconnect.ip_address), dbconnect.udp_port);
            }
            catch (ArgumentOutOfRangeException ax)
            {
                throw ax;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            if (client != null && ep != null)
            {
               try
                {
                    //Connect to remote host using IPEndPoint
                    client.Connect(ep);
                }
               catch (ObjectDisposedException od)
               {
                   throw od;
               }
               catch (ArgumentNullException nx)
               {
                   throw nx;
               }
               catch (ArgumentOutOfRangeException ax)
               {
                   throw ax;
               }
               catch (SocketException sx)
               {
                   throw sx;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
            }

            try
            {
                //Send UDP datagram 
                client.Send(dmxdatagram, dmxdatagram.Length);
            }
            catch (ArgumentNullException ax)
            {
                throw ax;
            }
            catch (ObjectDisposedException ox)
            {
                throw ox;
            }
            catch (InvalidOperationException ix)
            {
                throw ix;
            }
            catch (SocketException sx)
            {
                throw sx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Close the UDP connection
            client.Close();
        }
    }
}
