using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace SmartCarClient
{
    class MessageQueuing
    {
        public MessageQueue messageQueue;

        public MessageQueuing()
        {
            if(!MessageQueue.Exists(@".\Private$\queue"))
                MessageQueue.Create(@".\Private$\queue");
            messageQueue = new MessageQueue(@".\Private$\queue");
        }
        public void addMessage(string message, string label)
        {
            messageQueue.Send(message, label);
        }
        public Message[] getAllMessages()
        {
            return messageQueue.GetAllMessages();
        }
    }
}
