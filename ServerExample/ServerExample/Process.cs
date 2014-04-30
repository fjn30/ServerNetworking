using ServerNetworking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerExample
{
    public enum OpCodes { Msg = 1, RandomWhatever = 2 }

    //an example on how to use this library
    public class Processing : ClientProcessingBase
    {
        List<String> msgs = new List<String>(); 
        //implements
        public override void doProcess(int op, byte[] data)
        {
            switch ((OpCodes)op)
            {
                case OpCodes.Msg:
                    Message msg;
                    DeserializeObject<Message>(data, out msg);
                    if (msg != null)
                    {
                        msgs.Add(msg.text + " - " + msg.test);
                        //Confirmation
                        Message conf = new Message();
                        conf.text = "I received your message which was " + msg.text;
                        conf.test = 431.5341f;
                        this.getClientHandlingRef().Send<Message>((int)OpCodes.Msg, conf);
                    }
                    break;
                case OpCodes.RandomWhatever:
                    //this could have different deserialization, encryption / decryption etc
                    break;
                default:
                    break;
            }

        }

    }

    
}
