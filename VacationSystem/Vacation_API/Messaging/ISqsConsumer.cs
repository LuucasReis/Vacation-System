namespace Vacation_API.Messaging
{
    public interface ISqsConsumer
    {
        Task ConsumeQueueAsync(CancellationToken cancellationToken, bool PM);
    }
}
