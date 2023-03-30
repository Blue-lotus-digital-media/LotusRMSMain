using Microsoft.AspNetCore.SignalR;

namespace LotusRMSweb.Hubs
{
    public class OrderHub : Hub<IOrderHub>
    {
        public async Task OrderReceived(Guid tableId)
        {
            await Clients.All.OrderReceived(tableId);
        }
    }
}
