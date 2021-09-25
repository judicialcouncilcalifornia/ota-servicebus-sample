# Azure Service Bus .NET Framework sample to read data from an Azure Topic Subscription

This solution will provide sample console application to access a Service Bus entity (a Subscription) using Client Credentials and read messages from the entity (a Subscription).

In order to access the Service Bus entity, the application is first authenticated based on the client credentials and an OAuth 2.0 token is returned. Then this token is passed as part of a request to the Service Bus service to authorize access to the specified resource.

## Setup

The project has two relevant Nuget packages.
1. Microsoft.Azure.ServiceBus - contains methods for interacting with Service Bus 
2. Microsoft.Identity.Client - contains methods to obtain tokens from the Microsoft identity platform.

Install these packages using the Package Manager.

## Running the sample

To run the sample, update the tenant id, client credentials and the service bus details in the App.config file.

```csharp 
  <appSettings>
    <add key="TenantId" value="<your-tenant-id>"/>
    <add key="ClientId" value="<your-client-id>"/>
    <add key="ClientSecret" value="<your-client-secret>"/>
    <add key="ServiceBusNamespace" value="<your-service-bus-namespace>.servicebus.windows.net"/>
    <add key="TopicName" value="<your-topic-name>"/>
    <add key="SubscriptionName" value="<your-subscription-name>"/>
  </appSettings>
```

After making the changes, build and run the application.
