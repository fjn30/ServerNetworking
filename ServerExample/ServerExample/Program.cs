using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerNetworking;
using System.Threading;
using System.Windows.Forms;

namespace ServerExample
{
    class Program
    {

        public static void Main()
        {
            Application.Run(new ServerControlForm());
        }
    }
}
