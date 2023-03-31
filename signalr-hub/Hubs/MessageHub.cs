using Microsoft.AspNetCore.SignalR;
using signalr_lab.Model;

namespace signalr_lab.Hubs
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendCaseAlertToUser(IEnumerable<CaseAlert> caseAlerts)
        {
            await Clients.All.SendCaseAlertToUser(caseAlerts);
        }
    }
}
