using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Single_Reponsibility_Principle
{
    public abstract class Message
    {
        public void Send()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SMS : Message
    {
        public void Send()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Email : Message
    {
        public void Send()
        {
            throw new System.NotImplementedException();
        }
    }
}