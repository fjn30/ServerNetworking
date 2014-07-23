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
    /// ClientHandlingBase : The base implementations for each individual TcpClient,
    /// it runs on its own thread and through ClientProcessingInterface & ClientProcessingBase the user
    /// can define his/her own custom functionality.
    /// 
    /// @Packet protocol
    /// Each message has the first 4 bytes define its size and the serialized data by the means of protobuf come after.
    /// SendData(Packet p);
    /// </summary> 
    public class ClientHandlingBase
    {
        protected String ClientID;
        protected TcpClient ClientSocket;
        protected bool stop = false;
        protected Thread ClientThread;
        protected ClientProcessingBase clientProcessing;
        protected ExceptionMessageHandlerInterface exceptionMsgHandler;
        protected MessageHandlerInterface msgHandler;

        protected  int ClientReceiveTimeout = 0;
        protected  int ClientTickTimer = 0;

        protected NetworkStream networkStream;

        protected ClientHandlingBase(String clientID, TcpClient ClientSocket, ClientProcessingBase cpi, int ClientReceiveTimeout, int ClientTickTimer, ExceptionMessageHandlerInterface emi, MessageHandlerInterface mhi)
        {
            this.ClientID = clientID;
            this.ClientSocket = ClientSocket;
            this.clientProcessing = cpi;
            this.ClientReceiveTimeout = ClientReceiveTimeout;
            this.ClientTickTimer = ClientTickTimer;
            this.exceptionMsgHandler = emi;
            this.msgHandler = mhi;
            this.networkStream = ClientSocket.GetStream();
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

        protected virtual String GetClientID()
        {
            return this.ClientID;
        }

        protected virtual void Start()
        {
            this.stop = false;
            this.ClientThread = new Thread(new ThreadStart(Process));
            this.ClientThread.Name = "ClientThread#" + ClientID;
            this.ClientThread.Start();
        }

        protected virtual void Stop()
        {
            this.stop = true;
            this.ClientSocket.Client.Close();
            if (this.ClientThread != null && this.ClientThread.IsAlive)
            { 
                this.ClientThread.Join();
            }
        }

        protected virtual bool Alive
        {
            get
            {
                return (ClientThread != null && ClientThread.IsAlive);
            }
        }


        protected virtual void Process()
        {
            if (ClientSocket != null)
            {
                ClientSocket.ReceiveTimeout = this.ClientReceiveTimeout;
                NetworkStream networkStream = ClientSocket.GetStream();

                byte[] queueBytes;
                int opCode = 0;
                int packetSize = 0;
                byte[] opBytes = new byte[4];
                byte[] packetSizeBytes = new byte[4];
                
                while (!stop && ClientSocket.Connected )
                {
                    
                    try
                    {
                       
                       if (networkStream.CanRead)
                       {
                           if(packetSize == 0)
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

                               if (this.clientProcessing != null)
                               {
                                   this.clientProcessing.doProcess(opCode, queueBytes);
                               }

                               packetSize = 0;
                           }
                       }
                        
                    }
                    catch (IOException) { }
                    catch (SocketException)
                    {
                        this.doExceptionMessage("SocketException, IOException", "Connection broken");
                        break;
                    }

                }

                networkStream.Close();
                ClientSocket.Close();
            }

            
        }


        protected virtual Boolean SendData<T>(int OpCode, T p) where T : new() 
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


        protected virtual Boolean SendRaw(int OpCode, byte[] bytes)
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
