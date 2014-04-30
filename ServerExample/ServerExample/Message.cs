using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExample
{
    [ProtoContract]
    public class Message
    {
        [ProtoMember(1)]
        public String text;
        [ProtoMember(2)]
        public float test;
    }
}
