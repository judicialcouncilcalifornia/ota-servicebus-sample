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
```

Install these packages using the Node Package Manager (npm).
```bash
npm install
```

## Running the sample

To run the sample, update the tenant id, client credentials and the service bus details in the `serviceBusInfo.js` file.

```javascript
this.TenantId = '<TENANT_ID>';
this.ClientId = '<CLIENT_ID>';
this.ClientSecret = '<CLIENT_SECRET>';
this.Namespace = '<SERVICE_BUS_NAMESPACE>.servicebus.windows.net';
this.TopicName = '<SERVICE_BUS_TOPIC_NAME>';
this.SubscriptionName = '<SERVICE_BUS_SUBSCRIPTION_NAME>';
```

Then in the `app.js` file, update the `subscribeOptions` and the `serviceBusReceiverOptions` variables as needed:
```javacript
const subscribeOptions = {
    /* After executing a callback, the receiver will remove the message from the queue */
    autoCompleteMessages: false
    //autoCompleteMessages: true
};
const serviceBusReceiverOptions = {
    /***
     * You can choose between two receive modes:
     *   - In "peekLock" (default) mode, the receiver has a lock on the message for the duration specified on the queue.
     *   - In "receiveAndDelete" mode, messages are deleted from Service Bus as they are received.
     * Messages that are not settled within the lock duration will be redelivered up to 10 times, after which they get sent to a separate dead letter queue.
     * To learn more about how peekLock and message settlement, see https://docs.microsoft.com/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock
    */
    receiveMode: "peekLock",
    //receiveMode: "receiveAndDelete",
    skipParsingBodyAsJson: true,
    /***
     * You can choose to read the sub queue "deadLetter"
     * To learn more about dead letter queues, see https://docs.microsoft.com/azure/service-bus-messaging/service-bus-dead-letter-queues
    */
    //subQueueType: "deadLetter"
};
```

After making the changes, build and run the application.
```bash
node app.js
```
