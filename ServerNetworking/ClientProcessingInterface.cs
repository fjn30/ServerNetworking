using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNetworking
{
    public interface ClientProcessingInterface
    {
        void doProcess(int op, byte[] data);
        Client getClientHandlingRef();
        void SetClientHandlingRef(Client _ref);
        void SerializeObject<T>(T obj, out byte[] data) where T : new();
        void DeserializeObject<T>(byte[] data, out T obj) where T : new();
    }
}
