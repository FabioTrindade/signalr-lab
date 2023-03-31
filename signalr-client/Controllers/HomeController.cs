using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using signalr_client.Commands;
using signalr_client.Helpers;
using signalr_client.Models;
using System.Diagnostics;

namespace signalr_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<HubSettings> _config;

        public HomeController(ILogger<HomeController> logger
            , IOptions<HubSettings> config
            , IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
            _httpClient.BaseAddress = new Uri(_config.Value.BaseUrl);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [HttpGet("get-case-alerts")]
        public async Task<IActionResult> GetCaseAlertsAsync()
        {
            _logger.LogInformation($"Stating method {nameof(GetCaseAlertsAsync)}.");

            var response = await _httpClient.GetAsync("/api/v1/case-alerts");
            var result = await HttpClientHelper.DeserializeObjectResponse<List<CaseAlert>>(response);

            _logger.LogInformation($"Finish method {nameof(GetCaseAlertsAsync)}.");

            return Json(result);
        }

        [HttpPatch("update-analyst-case-alert/{id}")]
        public async Task<IActionResult> UpdateAnalystCaseAlertAsync(string id, UpdateCaseAlertCommand command)
        {
            _logger.LogInformation($"Stating method {nameof(UpdateAnalystCaseAlertAsync)}.");

            var content = HttpClientHelper.GetContent(command);
            var result = await _httpClient.PatchAsync($"/api/v1/case-alerts/{id}", content);

            _logger.LogInformation($"Finish method {nameof(UpdateAnalystCaseAlertAsync)}.");

            return Json(result.StatusCode);
        }

        [HttpGet("get-case-alert-hub-url-base")]
        public IActionResult GetCaseAlertHubUrlBase()
        { 
            return Json(_config.Value.BaseUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}