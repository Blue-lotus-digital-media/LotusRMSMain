namespace LotusRMSweb.Hubs
{
    public interface IOrderHub
    {
        Task OrderReceived(Guid tableId);
    }
}