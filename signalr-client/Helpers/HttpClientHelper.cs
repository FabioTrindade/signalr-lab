using System.Text.Json;
using System.Text;

namespace signalr_client.Helpers
{
    public static class HttpClientHelper
    {

        public static StringContent GetContent(object data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return new(JsonSerializer.Serialize(data, options), Encoding.UTF8, "application/json");
        }

        public static async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content, options);
        }
    }
}
