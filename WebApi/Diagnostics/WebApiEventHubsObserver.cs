using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Prysm.Monitoring.WebApi.Logger
{
    /// <summary>
    /// Represents observer witch listens event from the event source 
    /// </summary>
    public class WebApiEventHubsObserver : IObserver<EventEntry>
    {
        public EventHubClient Client
        {
            get;
            private set;
        }

        public WebApiEventHubsObserver()
        {
            string keyName = ConfigurationManager.AppSettings["ServiceBus.KeyName"];
            string accessKey = ConfigurationManager.AppSettings["ServiceBus.Key"];
            string nameSpace = ConfigurationManager.AppSettings["ServiceBus.Namespace"];

            if(string.IsNullOrWhiteSpace(nameSpace))
            {
                throw new KeyNotFoundException("ServiceBus.Namespace");
            }

            if (string.IsNullOrWhiteSpace(accessKey))
            {
                throw new KeyNotFoundException("ServiceBus.Key");
            }

            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new KeyNotFoundException("ServiceBus.KeyName");
            }

            MessagingFactorySettings settings = new MessagingFactorySettings()
            {
                TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(keyName, accessKey),
                TransportType = TransportType.Amqp
            };

            MessagingFactory factory = MessagingFactory.Create(ServiceBusEnvironment.CreateServiceUri("sb", nameSpace, ""), settings);
            Client = factory.CreateEventHubClient("Logs");
        }

        public void OnCompleted()
        {
            if (Client != null)
            {
                try
                {
                    Client.Close();
                }
                catch
                {
                    // Should be ingored any of exceptions to avoid main app crashing
                }
            }
        }

        public void OnError(Exception error)
        {
            //Just ignore any of the errors
        }

        public void OnNext(EventEntry value)
        {
            try
            {
                var content = Encoding.Default.GetBytes(JsonConvert.SerializeObject(value));
                Client.Send(new EventData(content) { PartitionKey = value.ActivityId.ToString() });
            }
            catch
            {
                // Should be ingored any of exceptions to avoid main app crashing
            }
        }
    }

    /// <summary>
    /// Extends ObservableEventListener with extension method to configure 
    /// logging system to send events to events hub
    /// </summary>
    public static class WebApiEventHubsObserverExtensions
    {
        /// <summary>
        ///  Initializes the event hub observer which collects data in events hub
        /// </summary>
        /// <param name="listener">Instance of the ObservableEventListener class</param>
        public static void LogToServiceBus(this ObservableEventListener listener)
        {
            listener.Subscribe(new WebApiEventHubsObserver());
        }
    }
}