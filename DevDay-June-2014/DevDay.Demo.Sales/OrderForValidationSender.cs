using Microsoft.ServiceBus.Messaging;

namespace DevDay.Demo.Sales
{
    public class OrderForValidationSender
    {
        private readonly MessagingFactory _messagingFactory;

        public OrderForValidationSender(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public void Send(string orderNumber)
        {
            var ordersQueueClient = _messagingFactory.CreateQueueClient("orders");
            ordersQueueClient.Send(new BrokeredMessage(orderNumber));
        }
    }
}