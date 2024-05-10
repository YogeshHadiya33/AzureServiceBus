using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ServiceBusReceiver;

public class TestQueueReceiver(ILogger<TestQueueReceiver> logger)
{
    [Function(nameof(TestQueueReceiver))]
    public async Task Run(
        [ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnectionString")] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}