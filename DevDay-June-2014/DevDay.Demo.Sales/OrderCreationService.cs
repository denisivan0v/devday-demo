using System;
using System.Threading;

using DevDay.Demo.Messages;

using Microsoft.ServiceBus.Messaging;

using NLog;

namespace DevDay.Demo.Sales
{
    public class OrderCreationService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly MessagingFactory _messagingFactory;

        public OrderCreationService(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public void Create(string orderNumber)
        {
            DoCreate(orderNumber);

            var orderCreatedEvent = new OrderCreatedEvent { OrderNumber = orderNumber };
            
            var ordersQueueClient = _messagingFactory.CreateQueueClient("orders");
            ordersQueueClient.Send(new BrokeredMessage(orderCreatedEvent));

            Logger.Info("OrderCreated event for order '{0}' sent...", orderNumber);
        }

        private void DoCreate(string orderNumber)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Logger.Info("Order with number '{0}' created...", orderNumber);
        }
    }
}