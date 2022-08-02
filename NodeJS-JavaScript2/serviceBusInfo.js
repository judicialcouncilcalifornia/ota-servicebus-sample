Object.defineProperty(exports, "__esModule", { value: true });

class ServiceBusInfo {
    constructor() {
        this.TenantId = '<TENANT_ID>';
        this.ClientId = '<CLIENT_ID>';
        this.ClientSecret = '<CLIENT_SECRET>';
        this.Namespace = '<SERVICE_BUS_NAMESPACE>.servicebus.windows.net';
        this.TopicName = '<SERVICE_BUS_TOPIC_NAME>';
        this.SubscriptionName = '<SERVICE_BUS_SUBSCRIPTION_NAME>';
    }
}

exports.ServiceBusInfo = ServiceBusInfo;
