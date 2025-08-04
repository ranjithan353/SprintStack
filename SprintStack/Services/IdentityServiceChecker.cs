using System.Net.Http;
using System.Threading.Tasks;

namespace SprintStack.Services
{
    public class IdentityServiceChecker
    {
        private readonly HttpClient _httpClient;
        private const string IdentityServiceUrl = "http://identityservice/api/status"; // Change as needed

        public IdentityServiceChecker(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsIdentityServiceAvailable()
        {
            try
            {
                var response = await _httpClient.GetAsync(IdentityServiceUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}