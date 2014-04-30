using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNetworking
{
    public interface MessageHandlerInterface
    {
        void broadcastMessage(String title, String text); 
    }
}
