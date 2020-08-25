using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseGenerator
{
    public static class TestCaseToHttpConverter
    {
        static readonly HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> ConvertTestCaseToHttpPOST<T>(string requestUri, T testCase) where T : ITestCase, new()
        {
            _client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string requestBody = JsonConvert.SerializeObject(testCase);
            StringContent content = new StringContent(requestBody, Encoding.Default, "application/json");
            HttpResponseMessage httpResponse = await _client.PostAsync(requestUri, content);
            return httpResponse;
        }
    }
}
