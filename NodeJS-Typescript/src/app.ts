/**
 * @summary Demonstrates how to receive JCC Service Bus messages in a stream
 */

import { delay, isServiceBusError, ProcessErrorArgs, ServiceBusClient, ServiceBusReceivedMessage, SubscribeOptions, ServiceBusReceiverOptions } from "@azure/service-bus";
import { ClientSecretCredential } from "@azure/identity";
import { ServiceBusInfo } from './serviceBusInfo';

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
     * Messages that are not settled within the lock duration will be redelivered up to 10 times, after which they get sent to a separate dead letter queue.
     * To learn more about how peekLock and message settlement, see https://docs.microsoft.com/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock
    */
    receiveMode: "peekLock",
    //receiveMode: "receiveAndDelete",

    /***
     * You can choose to read the sub queue "deadLetter"
     * To learn more about dead letter queues, see https://docs.microsoft.com/azure/service-bus-messaging/service-bus-dead-letter-queues
    */
    //subQueueType: "deadLetter"
};

export async function main() {
    const credential = new ClientSecretCredential(serviceBusInfo.TenantId, serviceBusInfo.ClientId, serviceBusInfo.ClientSecret);
    const sbClient = new ServiceBusClient(serviceBusInfo.Namespace, credential);
    const receiver = sbClient.createReceiver(serviceBusInfo.TopicName, serviceBusInfo.SubscriptionName, serviceBusReceiverOptions);

    try {
        const subscription = receiver.subscribe({
            processMessage: async (brokeredMessage: ServiceBusReceivedMessage) => {
                console.log(`Received message: ${brokeredMessage.body}`);
            },
            // This callback will be called for any error that occurs when either in the receiver when receiving the message
            // or when executing your `processMessage` callback or when the receiver automatically completes or abandons the message.
            processError: async (args: ProcessErrorArgs) => {
                // the `subscribe() call will not stop trying to receive messages without explicit intervention from you.
                console.log(`Error from source ${args.errorSource} occurred: `, args.error);

                if (isServiceBusError(args.error)) {
                    switch (args.error.code) {
                        case "MessagingEntityDisabled":
                        case "MessagingEntityNotFound":
                        case "UnauthorizedAccess":
                            console.log(
                                `An unrecoverable error occurred. Stopping processing. ${args.error.code}`,
                                args.error
                            );
                            await subscription.close();
                            break;
                        case "MessageLockLost":
                            console.log(`Message lock lost for message`, args.error);
                            break;
                        case "ServiceBusy":
                            console.log(`Service Bus Busy`, args.error);
                            // wait for 10 seconds before retrying.
                            await delay(10000);
                            break;
                    }
                }
            }
        }, subscribeOptions);

        // Waiting long enough before closing the receiver to receive messages
        console.log(`Receiving messages for 20 seconds before exiting...`);
        await delay(20000);

        console.log(`Closing...`);
        await receiver.close();
    } finally {
        await sbClient.close();
    }
}

main().catch((err) => {
    console.log("ReceiveMessagesStreaming - Error occurred: ", err);
    process.exit(1);
});
