# Azure Service Bus NodeJS sample to read data from an Azure Topic Subscription

This solution will provide sample console application to access a Service Bus entity (a Subscription) using Client Credentials and read messages from the entity (a Subscription).

In order to access the Service Bus entity, the application is first authenticated based on the client credentials and an OAuth 2.0 token is returned. Then this token is passed as part of a request to the Service Bus service to authorize access to the specified resource.

## Setup

The project has two relevant npm packages:
1. [@azure/service-bus](https://www.npmjs.com/package/@azure/service-bus)
1. [@azure/identity](https://www.npmjs.com/package/@azure/identity)

At the time that this sample was created the following software is needed and versions can be checked:
```bash
C:\> node --version
v16.16.0

C:\> npm --version
8.15.1

C:\> tsc --version
Version 4.7.4
```

Install these packages using the Node Package Manager (npm).
```bash
npm install
```

## Running the sample

To run the sample, update the tenant id, client credentials and the service bus details in the `serviceBusInfo.ts` file.

```typescript
public TenantId: string = '<TENANT_ID>';
public ClientId: string = '<CLIENT_ID>';
public ClientSecret: string = '<CLIENT_SECRET>';
public Namespace: string = '<SERVICE_BUS_NAMESPACE>.servicebus.windows.net';
public TopicName: string = '<SERVICE_BUS_TOPIC_NAME>';
public SubscriptionName: string = '<SERVICE_BUS_SUBSCRIPTION_NAME>';
```

Then in the `app.ts` file, update the `subscribeOptions` and the `serviceBusReceiverOptions` variables as needed:
```typescript
const serviceBusInfo: ServiceBusInfo = new ServiceBusInfo();
const subscribeOptions: SubscribeOptions = {
    /* After executing a callback, the receiver will remove the message from the queue */
    autoCompleteMessages: false
    //autoCompleteMessages: true
};
const serviceBusReceiverOptions: ServiceBusReceiverOptions = {
    /***
     * You can choose between two receive modes:
     *   - In "peekLock" (default) mode, the receiver has a lock on the message for the duration specified on the queue.
     *   - In "receiveAndDelete" mode, messages are deleted from Service Bus as they are received.
    */
    receiveMode: "peekLock",
    //receiveMode: "receiveAndDelete",

    /**
     * Option to disable the client from running JSON.parse() on the message body when receiving the message.
    */
    skipParsingBodyAsJson: true,
    // skipParsingBodyAsJson: false,

    /***
     * You can choose to read the sub queue "deadLetter"
    */
    //subQueueType: "deadLetter"
};
```

After making the changes, build and run the application.
```bash
npm start
```
