using System;

using DevDay.Demo.Messages;

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
            var result = message.GetBody<OrderValidationMessage>();

            var hub = GlobalHost.ConnectionManager.GetHubContext<ValidationsHub>();
            hub.Clients.All.sendStatus(result.ValidationResult, result.Message);
        }
    }
}