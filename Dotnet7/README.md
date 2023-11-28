# Azure Service Bus .NET 7 sample to read data from an Azure Service Bus Queue

This solution will provide sample console application to access a Service Bus entity (a Queue) using Azure Named Key Credential and read messages from the entity (a Queue).


## Setup

The project has one relevant Nuget package.
1. Azure.Messaging.ServiceBus - contains methods for interacting with Service Bus 

Install these packages using the Package Manager.

## Running the sample

To run the sample, update the access key credentials and the service bus details in the App.config file.

```csharp 
  <appSettings>
	<add key="ServiceBusNamespace" value="your-service-bus-namespace.servicebus.windows.net" />
	<add key="QueueName" value="your-queue-name" />
	<add key="SharedAccessKey" value="your-access-key-value" />
	<add key="SharedAccessKeyName" value="your-access-key-name" />
  </appSettings>
```

After making the changes, build and run the application.