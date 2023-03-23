using System.ComponentModel.DataAnnotations;

namespace Contracts.Http
{
    public class SendEmailTriggerRequest
    {
        [Required]
        public string EmailTo { get; init; }

        [Required]
        public string FileName { get; init; }

        [Required]
        public string FileUrl { get; init; }
    }

    public class SendEmailTriggerResponse
    {

    }
}