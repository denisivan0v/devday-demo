using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceBus.Messaging;

using NLog;

using Owin;

namespace DevDay.Demo.OrderValidations
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class Program
    {
        private const string Url = "http://localhost:8081";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            using (WebApp.Start(Url))
            {
                Logger.Info("Server running on {0}", Url);
                
                var receiver = new OrderForValidationReceiver(MessagingFactory.Create());
                receiver.StartReceiving();

                Logger.Info("Waiting for the incoming messages from the Service Bus");

                Console.ReadLine();
            }
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class ValidationsHub : Hub
    {
        public void SendStatus(string validationResult, string message)
        {
        }
    }
}