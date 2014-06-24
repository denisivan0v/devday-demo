using System;

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
            var result = new OrderValidationMessage();
            if (orderNumber.ToUpper().Contains("INVALID"))
            {
                result.ValidationResult = "error";
                result.Message = string.Format("Order '{0}' did't pass validation checks", orderNumber);
            }
            else
            {
                result.ValidationResult = "success";
                result.Message = string.Format("Order '{0}' is valid", orderNumber);
            }

            var ordersQueueClient = _messagingFactory.CreateQueueClient("orders");
            ordersQueueClient.Send(new BrokeredMessage(result));

            Console.WriteLine("Message sent...");
        }
    }
}