using Amazon.SQS.Model;

namespace Vacation_API.Messaging
{
    public interface ISqsMessenger
    {
        Task<SendMessageResponse> SendMessageAsync<T>(T message, bool PM);
    }
}
