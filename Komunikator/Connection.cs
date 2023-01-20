using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Komunikator.Model;
using System.Net.NetworkInformation;

namespace Komunikator
{
    class Connection
    {

        public static int port = 9191;
        
     

        public static bool SendUdpPackage(UserModel user,string messageContent)
        {


            try
            {
                UdpClient udpClient = new UdpClient();

                string messagePacket = string.Format("{0};{1};{2}", LocalIPAddress(), port, messageContent.Trim());

                byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(messagePacket);

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(user.Ip), port);

                udpClient.Send(buffer, buffer.Length, ep);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
           

            
        }

       

        public static void StartListening()
        {


            _ = Task.Run(async () =>
            {
                using (var udpClient = new UdpClient(port))
                {
                    string message = "";
                    string senderIP;
                    int senderPort;
                    string messageContent;

                    while (true)
                    { 
                        var receivedResults = await udpClient.ReceiveAsync();

                        message = Encoding.ASCII.GetString(receivedResults.Buffer);
                        string[] messageTab = message.Split(';');
                        senderIP = messageTab[0];
                        senderPort = int.Parse(messageTab[1]);
                        messageContent = messageTab[2];

                        if (messageContent != "RECIVED"&&messageContent!="ONLINE"&&messageContent!="OFFLINE")
                        {
                            string confirmation = string.Format("{0};{1};{2}", LocalIPAddress(), senderPort, "RECIVED");
                            byte[] buffer= System.Text.ASCIIEncoding.ASCII.GetBytes(confirmation);
                            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(senderIP), senderPort);
                            udpClient.Send(buffer, buffer.Length, ep);

                            DataAccess.getInstance().AddNewUser(senderIP);
                            string login = DataAccess.getInstance().getUserLogin(senderIP);
                            DataAccess.getInstance().AddNewMessage(senderIP, messageContent,login);
                        }

                        if (messageContent == "ONLINE"||messageContent=="RECIVED")
                        {
                            DataAccess.getInstance().SetUserOnline(senderIP);
                        }
                        if (messageContent == "OFFLINE")
                        {
                            DataAccess.getInstance().SetUserOffline(senderIP);
                        }

                       

                        
                        
                        

                    }

                }
            });
           
        }

        public static void SendAvailabilitySignal()
        {
            DataAccess.getInstance().GetAllIps().ForEach(ip =>
            {
                UdpClient udpClient = new UdpClient();
                string messagePacket = string.Format("{0};{1};{2}", LocalIPAddress(), port, "ONLINE");

                byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(messagePacket);

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                udpClient.Send(buffer, buffer.Length, ep);
                
            });
        }

        public static void SendUnavailebilatySignal()
        {
            DataAccess.getInstance().GetAllIps().ForEach(ip =>
            {
                UdpClient udpClient = new UdpClient();
                string messagePacket = string.Format("{0};{1};{2}", LocalIPAddress(), port, "OFFLINE");

                byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(messagePacket);

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                udpClient.Send(buffer, buffer.Length, ep);

            });
        }

        private static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
