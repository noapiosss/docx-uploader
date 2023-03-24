using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.Clients.Interfaces;
using Web.Configurations;

namespace Web.Clients
{
    public class FunctionTrigger : ITriggerable
    {
        private readonly string _postUrl;
        private readonly HttpClient _client;

        public FunctionTrigger(IOptionsMonitor<AppConfiguration> configuration, HttpClient client)
        {
            _postUrl = configuration.CurrentValue.FunctionTriggerPostUrl;
            _client = client;
        }

        public async Task RunAsync(string email, string fileName, string fileUrl, CancellationToken cancellationToken)
        {
            SendEmailTriggerRequest request = new()
            {
                EmailTo = email,
                FileName = fileName,
                FileUrl = fileUrl
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            _ = await _client.PostAsync(_postUrl, stringContent, cancellationToken);
        }
    }
}