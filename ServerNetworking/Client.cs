using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerNetworking
{
    /// <summary> 
    /// Client : extends the ClientHandlingBase adding some higher level functionality such as
    /// send to multiple clients
    /// </summary> 
    public class Client : ClientHandlingBase
    {
        private Server serverInstance = null;
        public Client(Server sRef, String clientID, TcpClient ClientSocket, ClientProcessingBase cpi, int ClientReceiveTimeout, int ClientTickTimer, ExceptionMessageHandlerInterface emi, MessageHandlerInterface mhi) : base(clientID,ClientSocket,cpi,ClientReceiveTimeout,ClientTickTimer,emi,mhi)
        {
            this.serverInstance = sRef;
            this.clientProcessing.SetClientHandlingRef(this);
        }

        public void SendToAll<T>(int op, T p) where T : new()
        {
            if (serverInstance != null)
            {
                foreach(Client cs in serverInstance.GetClients())
                {
                    cs.SendData<T>(op, p);
                }
            }
        }

        public void SendToSpecific<T>(int op, T p, Client[] array) where T : new()
        {
            if (serverInstance != null)
            {
                foreach (Client cs in array)
                {
                    cs.SendData<T>(op, p);
                }
            }
        }

        public bool Send<T>(int op, T p) where T : new()
        {
            return base.SendData<T>(op, p);
        }

        public void StartHandling()
        {
            base.Start();
        }

        public void StopHandling()
        {
            base.Stop();
        }

        public Boolean isAlive()
        {
            return base.Alive;
        }

        public String GetClientID()
        {
            return base.GetClientID();
        }

    }
}
