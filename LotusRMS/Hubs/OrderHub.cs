using Microsoft.AspNetCore.SignalR;

namespace LotusRMSweb.Hubs
{
    public class OrderHub : Hub<IOrderHub>
    {
        public async Task OrderReceived(Guid tableId)
        {
            await Clients.All.OrderReceived(tableId);
        }

        public async Task OrderComplete(List<string> data)
        {
            await Clients.All.OrderComplete(data);
        }
    }
}
