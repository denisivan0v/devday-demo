using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceBus.Messaging;

using NLog;

using Owin;

namespace DevDay.Demo.Sales
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class Program
    {
        private const string Url = "http://uk-rnd-168.2gis.local:8082";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            using (WebApp.Start(Url))
            {
                Logger.Info("Server running on {0}", Url);
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
    public class SalesHub : Hub
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void CreateOrder(string orderNumber)
        {
            Logger.Info("Order creation request received. Connection Id={0}, client origin={1}, user agent={2}",
                        Context.ConnectionId,
                        Context.Request.Headers["Origin"],
                        Context.Request.Headers["User-Agent"]);

            var orderForValidationSender = new OrderCreationService(MessagingFactory.Create());
            orderForValidationSender.Create(orderNumber);
        }
    }
}