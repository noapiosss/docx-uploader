using System;
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

        public FunctionTrigger(IOptionsMonitor<AppConfiguration> configuration)
        {
            _postUrl = configuration.CurrentValue.FunctionTriggerPostUrl;
        }

        public async Task RunAsync(string email, string fileName, string fileUrl, CancellationToken cancellationToken)
        {
            LogicAppTriggerRequest request = new()
            {
                EmailTo = email,
                FileName = fileName,
                FileUrl = fileUrl
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using HttpClient client = new() { Timeout = TimeSpan.FromSeconds(5) };

            _ = await client.PostAsync(_postUrl, stringContent, cancellationToken);
        }
    }
}