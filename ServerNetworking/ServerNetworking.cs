using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace ServerNetworking
{
    public sealed class Server
    {

        private ArrayList ClientSockets;
        public ArrayList GetClients()
        {
            return this.ClientSockets;
        }

        private bool ContinueReclaim = true;
        private Thread ThreadReclaim;

        private IPAddress ipaddress;
        private int port;
        private volatile Boolean stop = false;

        public volatile MessageHandlerInterface messageHandler;
        public volatile ExceptionMessageHandlerInterface exceptionMessageHandler;

        public volatile ClientProcessingBase clientProcessing;

        public int ClientNum;
        public int ClientReceiveTimeout = 100;
        public int ClientTickTimer = 200;

        public volatile Thread serverManagerThread;

        public int TickTimer;

        private TcpListener listener;

        private volatile ServerStatus status = null;
        public ServerStatus GetStatus()
        {
            return this.status;
        }

        private void doMessage(String title, String text) {
            if (this.messageHandler != null)
            {
                this.messageHandler.broadcastMessage(title, text);
            }
        }

        private void doExceptionMessage(String title, String text) {
            if (this.exceptionMessageHandler != null)
            {
                this.exceptionMessageHandler.exceptionMessage(title, text);
            }
        }

        public Server(String ip, int port, ClientProcessingBase clientProcessing, MessageHandlerInterface msgHandler, ExceptionMessageHandlerInterface excMsgHandler)
        {
            this.status = new ServerStatus();
            this.status.SetMaxClients(100);
            this.stop = false;
            this.ipaddress = IPAddress.Parse(ip);
            this.port = port;
            this.clientProcessing = clientProcessing;
            this.messageHandler = msgHandler;
            this.exceptionMessageHandler = excMsgHandler;
            this.status.SetOffline();
    
            this.doMessage("ServerNetworking", "ServerNetworking initialized.");
        }

        public void Stop()
        {
            this.stop = true;

            //this.DisconnectAllClients();

            this.listener.Server.Close();
            this.serverManagerThread.Interrupt();
        }

        public void Start()
        {
            serverManagerThread = new Thread(new ThreadStart(Process));
            serverManagerThread.Name = "ServerNetworkingManagerThread";
            this.serverManagerThread.Start();
        }

        public void Process()
        {
            try
            {
                this.status.SetOnline();
                if (stop != true)
                {
                    ClientSockets = new ArrayList();
                    ThreadReclaim = new Thread(new ThreadStart(Reclaim));
                    ThreadReclaim.Start();

                    this.listener = new TcpListener(this.ipaddress, this.port);
                    try
                    {
                        listener.Start();
                        ClientNum = 0;

                        this.doMessage("ServerNetworking", "Waiting for a connection...");

                        while (stop == false)
                        {

                            TcpClient handler = null;
                            if (!this.status.ServerFull())
                            {
                                handler = listener.AcceptTcpClient();

                                if (handler != null)
                                {
                                    ++ClientNum;

                                    lock (ClientSockets.SyncRoot)
                                    {
                                        Client c = new Client(this, "#c" + ClientNum, handler, clientProcessing, ClientReceiveTimeout, ClientTickTimer, exceptionMessageHandler, messageHandler);
                                        int i = ClientSockets.Add(c);
                                        ((Client)ClientSockets[i]).StartHandling();
                                        this.status.IncOnline();
                                        this.status.RefreshClientIdsConnected(ClientSockets); 
                                        this.doMessage("ServerNetworking", "Client " + ClientNum + " connected");
                                    }

                                }

                            }

                            Thread.Sleep(TickTimer);

                        }
                        listener.Stop();

                        ContinueReclaim = false;
                        ThreadReclaim.Join();

                        this.DisconnectAllClients();


                    }
                    catch (Exception e)
                    {
                        //0x800004005 error, should be avoided in the first place, TODO, (has to do
                        //with invoking from a dif thread?)
                        if (e.GetType() == typeof(SocketException) && e.HResult == -2147467259)
                        {
                            this.doMessage("ServerNetworking::Socket", "Socket interrupted...");
                            this.DisconnectAllClients();
                        }
                        else
                        {
                            this.doExceptionMessage("ServerNetworking::Exception", e.ToString());
                        }
                    }

                }
            }
            catch (ThreadInterruptedException tie) //handling, any?!
            {
                this.doMessage("ServerNetworking", "Server managing thread (accepting clients) interrupted..");
            }
            doMessage("ServerNetworking", "Closing server...");
        }

        public void DisconnectAllClients()
        {
            foreach (Object Client in ClientSockets.ToArray())
            {
                ((Client)Client).StopHandling();
            }
        }
  
        public void Reclaim()
        {
            while (ContinueReclaim)
            {
                lock (ClientSockets.SyncRoot)
                {
                    for (int x = ClientSockets.Count - 1; x >= 0; x--)
                    {
                        Object client = ClientSockets[x];
                        if (!((Client)client).isAlive())
                        {
                            String id = ((Client)client).GetClientID();
                            ClientSockets.Remove(client);
                            doMessage("ServerNetworking", "Client " + id + " left");
                            this.status.DecOnline();
                        }
                    }
                }
                Thread.Sleep(200);
            }
        }



    }
}
