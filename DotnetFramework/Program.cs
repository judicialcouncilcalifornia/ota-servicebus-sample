using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.Identity.Client;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Management;

namespace ReadFromTopicSubscription
{
    class Program
    {
        // declare a subscription client to receive the messages from the subscription
        private SubscriptionClient receiveClient;

        static async Task Main(string[] args)
        {
            // read the settings from Application configuration file
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string clientSecret = appSettings["ClientSecret"] ?? string.Empty;
            string clientId = appSettings["ClientId"] ?? string.Empty;
            string serviceBusAudience = appSettings["ServiceBusAudience"] ?? string.Empty;
            string serviceBusNamespace = appSettings["ServiceBusNamespace"] ?? string.Empty;
            string subscriptionName = appSettings["SubscriptionName"] ?? string.Empty;
            string tenantId = appSettings["TenantId"] ?? string.Empty;
            string topicName = appSettings["TopicName"] ?? string.Empty;

            if (string.IsNullOrEmpty(clientSecret)
                || string.IsNullOrEmpty(clientId)
                || string.IsNullOrEmpty(serviceBusAudience)
                || string.IsNullOrEmpty(serviceBusNamespace)
                || string.IsNullOrEmpty(subscriptionName)
                || string.IsNullOrEmpty(tenantId)
                || string.IsNullOrEmpty(topicName))
            {
                Console.WriteLine("All the configuration settings are mandatory");
                throw new ArgumentException("All the configuration settings are mandatory");
            }

            Program P = new Program();

            // authenticate the application and authorize access to the service bus entity to read the messages
            await P.ClientCredentialsScenario(clientSecret, clientId, serviceBusAudience, serviceBusNamespace, subscriptionName, tenantId, topicName);
        }

        private async Task ClientCredentialsScenario(string clientSecret, string clientId, string serviceBusAudience, string serviceBusNamespace, string subscriptionName, string tenantId, string topicName)
        {
            // get the OAuth 2.0 token
            TokenProvider aadTokenProvider = TokenProvider.CreateAzureActiveDirectoryTokenProvider(async (audience, authority, state) =>
            {
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithAuthority(authority)
                    .WithClientSecret(clientSecret)
                    .Build();

                AuthenticationResult authResult = await app.AcquireTokenForClient(new string[] {$"{serviceBusAudience}"}).ExecuteAsync();
                return authResult.AccessToken;

            }, $"https://login.windows.net/{tenantId}");

            // create a subscription client to read messages from the subscription
            SubscriptionClient sc = new SubscriptionClient(new Uri($"sb://{serviceBusNamespace}/").ToString(), topicName, subscriptionName, aadTokenProvider);

            sc.PrefetchCount = 10;

            // receive the messages from the subscription
            await ReceiveAsync(sc);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }

        private void InitializeReceiver()
        {
            try
            {
                // define a message handler and register the same
                MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };

                this.receiveClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                this.receiveClient.CloseAsync();
            }
        }

        private async Task ReceiveAsync(SubscriptionClient sc)
        {
            this.receiveClient = sc;

            // initialize the receiver
            this.InitializeReceiver();

            // shut down the receiver
            await this.receiveClient.CloseAsync();
        }

        private async Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");
            await this.receiveClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
