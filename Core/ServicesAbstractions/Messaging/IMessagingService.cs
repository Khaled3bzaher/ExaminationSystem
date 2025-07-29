namespace ServicesAbstractions.Messaging
{
    public interface IMessagingService
    {
        Task PublishAsync<T>(string queueName, T message);
        Task SubscribeAsync<T>(string queueName, Func<T, Task> handler);
    }
}
