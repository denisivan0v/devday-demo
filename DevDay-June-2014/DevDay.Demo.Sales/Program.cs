﻿using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceBus.Messaging;

using Owin;

namespace DevDay.Demo.Sales
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class Program
    {
        public static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            const string Url = "http://localhost:8082";
            using (WebApp.Start(Url))
            {
                Console.WriteLine("Server running on {0}", Url);
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
        public void CreateOrder(string orderNumber)
        {
            var orderForValidationSender = new OrderForValidationSender(MessagingFactory.Create());
            orderForValidationSender.Send(orderNumber);
        }
    }
}