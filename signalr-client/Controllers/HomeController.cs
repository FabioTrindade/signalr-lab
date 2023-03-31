using Microsoft.AspNetCore.Mvc;
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
        const string URL_BASE = "https://localhost:7289";

        public HomeController(ILogger<HomeController> logger
            , IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
            _httpClient.BaseAddress = new Uri(URL_BASE);
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
            var response = await _httpClient.GetAsync("/api/v1/case-alerts");
            var result = await HttpClientHelper.DeserializeObjectResponse<List<CaseAlert>>(response);

            return Json(result);
        }

        [HttpPatch("update-analyst-case-alert/{id}")]
        public async Task<IActionResult> UpdateAnalystCaseAlertAsync(string id, UpdateCaseAlertCommand command)
        {
            var content = HttpClientHelper.GetContent(command);
            var result = await _httpClient.PatchAsync($"/api/v1/case-alerts/{id}", content);

            return Json(result.StatusCode);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}