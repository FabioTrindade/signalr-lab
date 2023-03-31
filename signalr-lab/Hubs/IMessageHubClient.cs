using signalr_lab.Model;

namespace signalr_lab.Hubs
{
    public interface IMessageHubClient
    {
        Task SendCaseAlertToUser(IEnumerable<CaseAlert> caseAlerts);
    }
}
