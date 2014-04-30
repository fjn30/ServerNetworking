using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNetworking
{
    public interface ExceptionMessageHandlerInterface
    {
        void exceptionMessage(String title, String text); 
    }
}
