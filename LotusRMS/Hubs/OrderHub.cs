using LotusRMS.Models.Viewmodels.signalRVM;
using Microsoft.AspNetCore.SignalR;

namespace LotusRMSweb.Hubs
{
    public class OrderHub : Hub<IOrderHub>
    {
        public async Task OrderReceived(tableReturnVM vm)
        {
            await Clients.All.OrderReceived(vm);
        } 
        public async Task CheckoutComplete(tableReturnVM vm)
        {
            await Clients.All.CheckoutComplete(vm);
        }

        public async Task OrderComplete(List<string> data)
        {
            await Clients.All.OrderComplete(data);
        }
    }
}
