using System;
using System.Threading;

using DevDay.Demo.Messages;

using Microsoft.ServiceBus.Messaging;

namespace DevDay.Demo.Sales
{
    public class OrderCreationService
    {
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

            Console.WriteLine("OrderCreated event sent...");
        }

        private void DoCreate(string orderNumber)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}