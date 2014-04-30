using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNetworking
{
    public class Client
    {
        private String ipAddress;
        private int portNum;
        private NetworkStream networkStream;
        private TcpClient tcpClient;

        private Thread clientManagerThread;
        private Boolean stop = false;

        private ExceptionMessageHandlerInterface exceptionMsgHandler;
        private MessageHandlerInterface msgHandler;

        private ClientProcessingBase proccessing;

        public int ReceiveTickRate = 0;

        public Client(String ip, int port, ClientProcessingBase cpi, ExceptionMessageHandlerInterface emi, MessageHandlerInterface mhi)
        {
            this.ipAddress = ip;
            this.portNum = port;
            this.msgHandler = mhi;
            this.exceptionMsgHandler = emi;
            this.proccessing = cpi;
        }

        protected virtual void doMessage(String title, String text)
        {
            if (this.msgHandler != null)
            {
                this.msgHandler.broadcastMessage(title, text);
            }
        }

        protected virtual void doExceptionMessage(String title, String text)
        {
            if (this.exceptionMsgHandler != null)
            {
                this.exceptionMsgHandler.exceptionMessage(title, text);
            }
        }

        public void Start()
        {
            try
            {
                this.tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, portNum);
                networkStream = tcpClient.GetStream();

                this.clientManagerThread = new Thread(Process);
                clientManagerThread.Name = "CLIENTMANAGERTHREAD";
                clientManagerThread.Start();
            }
            catch (SocketException)
            {
                this.msgHandler.broadcastMessage("ClientNetworking", "The server refused the connection or the address is wrong.");
            }
        }

        public void Stop()
        {
            try
            {
                this.stop = true;
                this.tcpClient.Client.Close();
                this.clientManagerThread.Interrupt();
            }
            catch (Exception e)
            {
                this.doExceptionMessage("Exception", "Server was never started.");
            }
        }

        private void Process()
        {
            
            byte[] queueBytes;
            int opCode = 0;
            int packetSize = 0;
            byte[] opBytes = new byte[4];
            byte[] packetSizeBytes = new byte[4];
            while (this.stop == false && networkStream.CanRead)
            {
                 try
                 {
                       if (networkStream.CanRead)
                       {
                           if (packetSize == 0)
                           {
                                networkStream.Read(opBytes, 0, 4);
                                networkStream.Read(packetSizeBytes, 0, 4);
                                opCode = BitConverter.ToInt32(opBytes, 0);
                                packetSize = BitConverter.ToInt32(packetSizeBytes, 0);
                                queueBytes = new byte[packetSize];
                            }
                            else
                            {
                                queueBytes = new byte[packetSize];
                                networkStream.Read(queueBytes, 0, packetSize);

                                if (this.proccessing != null)
                                {
                                    proccessing.doProcess(opCode, queueBytes);
                                }

                                packetSize = 0;
                            }
                       }

                 }
                 catch (IOException) { }
                 catch (SocketException) {
                    this.doExceptionMessage("SocketException, IOException", "Connection broken");
                    break;
                 }
                 Thread.Sleep(ReceiveTickRate);
             }
             networkStream.Close();
             tcpClient.Close();
                
        }

        public Boolean SendData<T>(int OpCode, T p) where T : new()
        {

            if (p != null && networkStream != null && networkStream.CanWrite)
            {
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize<T>(ms, (T)p);
                    bytes = ms.ToArray();
                }

                byte[] opBytes = BitConverter.GetBytes(OpCode);

                if (opBytes.Length > 4)
                {
                    throw new Exception("OPCODE CAN'T BE BIGGER THAN 4 BYTES WORTH, 32BIT INT!!!");
                }

                byte[] lengthBytes = BitConverter.GetBytes(bytes.Length);
                byte[] headerBytes = new byte[lengthBytes.Length + opBytes.Length];
                Buffer.BlockCopy(opBytes, 0, headerBytes, 0, opBytes.Length);
                Buffer.BlockCopy(lengthBytes, 0, headerBytes, opBytes.Length, lengthBytes.Length);

                byte[] concrete = new byte[headerBytes.Length + bytes.Length];
                Buffer.BlockCopy(headerBytes, 0, concrete, 0, headerBytes.Length);
                Buffer.BlockCopy(bytes, 0, concrete, headerBytes.Length, bytes.Length);

                //write to stream
                networkStream.Write(concrete, 0, concrete.Length);
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean SendRaw(int OpCode, byte[] bytes)
        {
            if (bytes != null && networkStream != null && networkStream.CanWrite)
            {

                byte[] opBytes = BitConverter.GetBytes(OpCode);

                if (opBytes.Length > 4)
                {
                    throw new Exception("OPCODE CAN'T BE BIGGER THAN 4 BYTES WORTH, 32BIT INT!!!");
                }

                byte[] lengthBytes = BitConverter.GetBytes(bytes.Length);
                byte[] headerBytes = new byte[lengthBytes.Length + opBytes.Length];
                Buffer.BlockCopy(opBytes, 0, headerBytes, 0, opBytes.Length);
                Buffer.BlockCopy(lengthBytes, 0, headerBytes, opBytes.Length, lengthBytes.Length);

                byte[] concrete = new byte[headerBytes.Length + bytes.Length];
                Buffer.BlockCopy(headerBytes, 0, concrete, 0, headerBytes.Length);
                Buffer.BlockCopy(bytes, 0, concrete, headerBytes.Length, bytes.Length);

                //write to stream
                networkStream.Write(concrete, 0, concrete.Length);
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
