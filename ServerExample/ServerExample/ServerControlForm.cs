using ServerNetworking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerExample
{
    public partial class ServerControlForm : Form
    {

        public static Boolean scrolltouched = false;

        public Server sn;
        public Thread serverThread;

        public ServerControlForm()
        {
            InitializeComponent();
        
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (sn == null)
            {
                ClientProcessingBase proc = new Processing();
                MessageSimple ms = new MessageSimple(listBoxMessages);
                sn = new Server("127.0.0.1", 44405, proc, ms, ms);
                sn.ClientTickTimer = 0;
                sn.Start();

                //register to the events
                sn.GetStatus().DataChangedEvent += new EventHandler<ServerStatus>(OnDataChanged);
            }
        }


        private void OnDataChanged(object sender, ServerStatus status)
        {
            labelStatus.BeginInvoke(new MethodInvoker(delegate
            {
                if (status.IsOnline())
                {
                    labelStatus.Text = "Online " + status.GetNumOnlineClients() + "/" + status.GetMaxClients();
                }
                else
                {
                    labelStatus.Text = "Offline 0/0";
                }

                if (comboBoxConnectedUsers.Items.Count != status.GetClientIdsConnected().Count)
                {
                    comboBoxConnectedUsersRefresh(status);
                }

            }));
        }

        private void comboBoxConnectedUsersRefresh(ServerStatus status)
        {
            comboBoxConnectedUsers.Items.Clear();
            comboBoxConnectedUsers.Items.Add(" ");
            foreach(String s in status.GetClientIdsConnected())
            {
                comboBoxConnectedUsers.Items.Add(s);
            }
            comboBoxConnectedUsers.Invalidate();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.Stop();
        }

        private void Stop()
        {
            if (sn != null)
            {
                sn.Stop();
                sn = null;
            }
            else
            {
                MessageBox.Show("Server is not open");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public class MessageSimple : MessageHandlerInterface, ExceptionMessageHandlerInterface
        {
            private ListBox messages;

            public MessageSimple(ListBox _messages)
            {
                this.messages = _messages;
                this.broadcastMessage("Server", "Messaging initialized.");
            }

            public void broadcastMessage(String title, String text)
            {
                messages.BeginInvoke(new MethodInvoker(delegate
                {
                    int remainder;
                    int lines = Math.DivRem(text.Length, 120, out remainder);
                    messages.Items.Add(title + " :: " + text);
                    messages.Refresh();
                }));

            }

            public void exceptionMessage(String title, String text)
            {
                messages.BeginInvoke(new MethodInvoker(delegate
                {
                    String s = "EXCEPTION || " + title + " :: " + text;
                    Console.WriteLine(s);
                    messages.Items.Add(s);
                    messages.Refresh();
                }));
            }

        }

        private void ServerControlForm_Load(object sender, EventArgs e)
        {

        }

    }
}
