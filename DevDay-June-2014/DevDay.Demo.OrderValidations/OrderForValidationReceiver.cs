using System;

using DevDay.Demo.Messages;

using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;

namespace DevDay.Demo.OrderValidations
{
    public class OrderForValidationReceiver
    {
        private readonly MessagingFactory _messagingFactory;
        private readonly OrderValidator _orderValidator = new OrderValidator();

        public OrderForValidationReceiver(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public void StartReceiving()
        {
            var queueClient = _messagingFactory.CreateQueueClient("orders");
            queueClient.OnMessage(Receive);
        }

        private void Receive(BrokeredMessage message)
        {
            Console.WriteLine("OrderCreated event received...");
            var orderCreatedEvent = message.GetBody<OrderCreatedEvent>();

            string validationMessage;
            var validationResult = _orderValidator.Validate(orderCreatedEvent.OrderNumber, out validationMessage)
                                       ? "success"
                                       : "error";

            var hub = GlobalHost.ConnectionManager.GetHubContext<ValidationsHub>();
            hub.Clients.All.sendStatus(validationResult, validationMessage);
        }
    }
}