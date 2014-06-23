using System;

using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;

namespace DevDay.Demo.OrderValidations
{
    public class OrderForValidationReceiver
    {
        private readonly MessagingFactory _messagingFactory;

        public OrderForValidationReceiver(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public void StartReceiving()
        {
            var queueClient = _messagingFactory.CreateQueueClient("orders");
            queueClient.OnMessage(Receive);
        }

        private static void Receive(BrokeredMessage message)
        {
            Console.WriteLine("Message received...");

            var hub = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
            hub.Clients.All.addMessage("message from server", message);
        }
    }
}