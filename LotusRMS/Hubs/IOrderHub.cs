namespace LotusRMSweb.Hubs
{
    public interface IOrderHub
    {
        Task OrderReceived(Guid tableId);
        Task OrderComplete(List<string> data);
    }
}