using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Contracts.Http
{
    public class SendEmailTriggerRequest
    {
        [Required]
        [JsonProperty("emailTo")]
        public string EmailTo { get; init; }

        [Required]
        [JsonProperty("fileName")]
        public string FileName { get; init; }

        [Required]
        [JsonProperty("fileUrl")]
        public string FileUrl { get; init; }
    }

    public class SendEmailTriggerResponse
    {

    }
}