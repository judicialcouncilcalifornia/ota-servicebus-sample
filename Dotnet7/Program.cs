using Azure;
using Azure.Messaging.ServiceBus;
using System.Configuration;
using System.Text;

namespace MyNamespace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string queueName = ConfigurationManager.AppSettings["QueueName"] ?? string.Empty;
            string serviceBusNamespace = ConfigurationManager.AppSettings["ServiceBusNamespace"] ?? string.Empty;

            var options = new ServiceBusProcessorOptions
            {
                // By default or when AutoCompleteMessages is set to true, the processor will complete the message after executing the message handler
                // Set AutoCompleteMessages to false to [settle messages](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock) on your own.
                // In both cases, if the message handler throws an exception without settling the message, the processor will abandon the message.
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            };

            var nameKeyCredential = GetAzureNamedKeyCredential();
            await using ServiceBusClient client = new ServiceBusClient(serviceBusNamespace, nameKeyCredential);
            ServiceBusProcessor processor = client.CreateProcessor(queueName, options);

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
            Console.WriteLine("Service has started.");

            Console.WriteLine("Press any key to stop processing...");
            Console.ReadKey();

            await processor.StopProcessingAsync();
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }

        private static AzureNamedKeyCredential GetAzureNamedKeyCredential()
        {
            var sharedAccessKey = ConfigurationManager.AppSettings["SharedAccessKey"] ?? string.Empty;
            var sharedAccessKeyName = ConfigurationManager.AppSettings["SharedAccessKeyName"] ?? string.Empty;

            return new AzureNamedKeyCredential(sharedAccessKeyName, sharedAccessKey);
        }

        private static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                Console.WriteLine($"Received message: {Encoding.UTF8.GetString(args.Message.Body)}");
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                await args.DeadLetterMessageAsync(args.Message, "Error", ex.Message);
            }
        }

        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"\n------------Failed to read from the service bus------------\n{args.Exception}");
            return Task.CompletedTask;
        }
    }
}