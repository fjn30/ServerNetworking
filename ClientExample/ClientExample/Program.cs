using ClientNetworking;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientExample
{

    public class Messages : MessageHandlerInterface, ExceptionMessageHandlerInterface
    {
        public void broadcastMessage(String title, String text)
        {
            Console.WriteLine(title + " :: " + text);
        }

        public void exceptionMessage(String title, String text)
        {
            Console.WriteLine(title + " :: " + text);
        }
    }

    public class Processing : ClientProcessingBase
    {
        public override void doProcess(int op, byte[] data)
        {
            switch (op)
            {
                case 0:
                    break;
                case 1:
                    Message msg;
                    DeserializeObject<Message>(data, out msg);
                    if(msg!=null)
                    {
                        Console.WriteLine("\n\n Server response: " + msg.text + " float: " + msg.test);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    class Program
    {

   
        public static void Main()
        {
            ClientProcessingBase proc = new Processing();
            Messages ms = new Messages();
            Client c = new Client("127.0.0.1", 44405, proc, ms, ms);


            while (true)
            {
                Console.WriteLine(" 0 : start tcp client, 1 : stop tcp client, 2 : send");

                Console.WriteLine("Your input: ");
                String input = Console.ReadLine();
                Console.WriteLine("INPUT " + input);

                if (input.Equals("0"))
                {
                    c.Start();
                }
                else if (input.Equals("1"))
                {
                    c.Stop();
                }
                else if (input.Equals("2"))
                {
                    Console.WriteLine("m.text content, input: ");
                    input = Console.ReadLine();
                    Message m = new Message();
                    m.test = 411321.53141f;
                    m.text = input;
                    c.SendData<Message>(1, m);
                }

                Thread.Sleep(1000);
            }

        }

        

    }
}
