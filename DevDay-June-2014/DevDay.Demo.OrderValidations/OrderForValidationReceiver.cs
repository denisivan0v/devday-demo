using System;

using DevDay.Demo.Messages;

using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;

using NLog;

namespace DevDay.Demo.OrderValidations
{
    public class OrderForValidationReceiver
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly MessagingFactory _messagingFactory;
        private readonly OrderValidator _orderValidator = new OrderValidator();

        public OrderForValidationReceiver(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public void StartReceiving()
        {
            Logger.Info("Event receiving started");
            var queueClient = _messagingFactory.CreateQueueClient("orders");
            queueClient.OnMessage(Receive);
        }

        private void Receive(BrokeredMessage message)
        {
            var orderCreatedEvent = message.GetBody<OrderCreatedEvent>();
            Logger.Info("OrderCreated event received for order '{0}'", orderCreatedEvent.OrderNumber);

            string validationMessage;
            var validationResult = _orderValidator.Validate(orderCreatedEvent.OrderNumber, out validationMessage)
                                       ? "success"
                                       : "error";

            Logger.Info("Order validation completed with result '{0}'", validationResult);

            var hub = GlobalHost.ConnectionManager.GetHubContext<ValidationsHub>();
            hub.Clients.All.sendStatus(validationResult, validationMessage);

            Logger.Info("Validation for order '{0}' message pushed to all clients", orderCreatedEvent.OrderNumber);
        }
    }
}