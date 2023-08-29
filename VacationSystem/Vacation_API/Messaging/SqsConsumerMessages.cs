using Amazon.SQS;
using Amazon.SQS.Model;
using Vacation_API.Email;
using System.Text.Json;
using Newtonsoft.Json;
using Utility;

namespace Vacation_API.Messaging
{
    public class SqsConsumerMessages : ISqsConsumer
    {
        private readonly IAmazonSQS _sqs;
        private readonly IEmailService _emailService;
        private string queueNameIntern;
        private string messageEmail;
        private string internsBoss;
        private string queueNamePM;
        public SqsConsumerMessages(IAmazonSQS sqs, IConfiguration configuration, IEmailService emailService)
        {
            _sqs = sqs;
            queueNameIntern = configuration.GetValue<string>("AwsServices:SqsInternQueue")!;
            queueNamePM = configuration.GetValue<string>("AwsServices:SqsPMQueue")!;
            _emailService = emailService;
            internsBoss = "emaildoPM@gmail.com";
            messageEmail = "";

        }

        public async Task ConsumeQueueAsync(CancellationToken cancellationToken, bool PM)
        {
            GetQueueUrlResponse queueUrlResponse = null;
            if (PM)
            {
                queueUrlResponse = await _sqs.GetQueueUrlAsync(queueNamePM, cancellationToken);
            }
            else
            {
                queueUrlResponse = await _sqs.GetQueueUrlAsync(queueNameIntern, cancellationToken);
            }

            var receiveMessageRequest = new ReceiveMessageRequest()
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                AttributeNames = new List<string> { "All" },
                MessageAttributeNames = new List<string> { "All" }
            };
            
            while(!cancellationToken.IsCancellationRequested)
            {
                var response = await _sqs.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);
                foreach (var message in response.Messages)
                {
                    var messageType = message.MessageAttributes["MessageType"].StringValue;
                    var obj = JsonConvert.DeserializeObject<HistVacationMessage>(message.Body);
                    
                    if(obj!.Status == SD.defaultStatus)
                    {
                        messageEmail = "Boa tarde. \nVocê tem novas férias para aprovar!!\nAtenciosamente,\nEquipe Apropriação.";
                        _emailService.Enviar(internsBoss, "Nova Solicitação de Férias", messageEmail);

                    }
                    else
                    {
                        messageEmail = $"Olá, {obj.Name}. \nSua solicitação foi {obj.Status}!.\nAtenciosamente,\nEquipe Apropriação.";
                        _emailService.Enviar(obj.Email, "Novo status das suas Férias", messageEmail);

                    }
                    await _sqs.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
                }
                break;
            }
            

        }
    }
}
