using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace Vacation_API.Messaging
{
    public class SqsMessenger : ISqsMessenger
    {
        private readonly IAmazonSQS _sqs;
        private string queueNameIntern;
        private string queueNamePM;
        public SqsMessenger(IAmazonSQS sqs, IConfiguration configuration)
        {
            _sqs = sqs;
            queueNameIntern = configuration.GetValue<string>("AwsServices:SqsInternQueue")!;
            queueNamePM = configuration.GetValue<string>("AwsServices:SqsPMQueue")!;
        }
        public async Task<SendMessageResponse> SendMessageAsync<T>(T message, bool PM)
        {
            GetQueueUrlResponse queueUrlResponse = null;
            if (PM == true)
            {
                queueUrlResponse = await _sqs.GetQueueUrlAsync(queueNamePM);
            }
            else
            {
                queueUrlResponse = await _sqs.GetQueueUrlAsync(queueNameIntern);
            }

            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MessageBody = JsonSerializer.Serialize(message),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>()
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = typeof(T).Name
                        }
                    }
                }
            };

            return await _sqs.SendMessageAsync(sendMessageRequest);
        }
    }
}
