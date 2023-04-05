using LotusRMS.Models.Viewmodels.signalRVM;

namespace LotusRMSweb.Hubs
{
    public interface IOrderHub
    {
        Task OrderReceived(tableReturnVM vm);
        Task CheckoutComplete(tableReturnVM vm);
        Task OrderComplete(List<string> data);
    }
}