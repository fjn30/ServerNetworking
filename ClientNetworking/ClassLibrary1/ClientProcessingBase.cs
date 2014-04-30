using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientNetworking
{
    public class ClientProcessingBase : ClientProcessingInterface
    {
        private Client chRef;
        public void SetClientRef(Client _ref)
        {
            this.chRef = _ref;
        }

        public Client getClientRef()
        {
            return this.chRef;
        }

        public virtual void doProcess(int op, byte[] data) {   }


        public virtual void SerializeObject<T>(T obj, out byte[] data) where T : new()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, obj);
                data = ms.ToArray();
            }
        }

        public virtual void DeserializeObject<T>(byte[] data, out T obj) where T : new()
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                obj = Serializer.Deserialize<T>(ms);
            }
        }
    }
}
