using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalr_lab.Commands;
using signalr_lab.Hubs;
using signalr_lab.Model;

namespace signalr_lab.Controllers
{
    [Route("api/v1/case-alerts")]
    public class CaseAlertController : Controller
    {
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;
        private static List<CaseAlert> _caseAlerts = new();

        public CaseAlertController(IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost]
        public string PostCasAlerts([FromBody] CreateCaseAlertCommand command)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Sleep for 2 seconds.");

                _caseAlerts.Add(new CaseAlert($"Alert {i}", $"Description {i}"));

                var result = _caseAlerts.OrderBy(o => o.CreateAt).ToList();

                _messageHub.Clients.All.SendCaseAlertToUser(result);

                Thread.Sleep(2000);
            }

            //_caseAlerts.Add(new CaseAlert(command.Name, command.Description));

            //var result = _caseAlerts.OrderBy(o => o.CreateAt).ToList();

            //_messageHub.Clients.All.SendCaseAlertToUser(result);

            return "Case alert sent successfully to all users!";
        }

        [HttpGet]
        public List<CaseAlert> GetCasAlerts()
        {
            return _caseAlerts;
        }

        [HttpPatch("{id}")]
        public string PatchCaseAlertsStatus([FromRoute]Guid id, [FromBody] UpdateCaseAlertCommand command)
        {
            _caseAlerts.Where(i => i.Id == id)
                          .ToList()
                          .ForEach(i =>
                          {
                              i.IsActive = command.IsActive ?? i.IsActive;
                              i.Analyst = command.Analyst ?? i.Analyst;
                              i.UpdateAt = DateTime.Now;
                          });

            var result = _caseAlerts.OrderBy(o => o.CreateAt).ToList();

            _messageHub.Clients.All.SendCaseAlertToUser(result);

            return "Case alert update and sent successfully to all users!";
        }

        [HttpDelete]
        public string DeleteCaseAlerts()
        {
            _caseAlerts.Clear();

            _messageHub.Clients.All.SendCaseAlertToUser(_caseAlerts);

            return "Case alert delete and sent successfully to all users!";
        }
    }
}
