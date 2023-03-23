using System.ComponentModel.DataAnnotations;

namespace Contracts.Http
{
    public class LogicAppTriggerRequest
    {
        [Required]
        public string EmailTo { get; init; }

        [Required]
        public string FileName { get; init; }

        [Required]
        public string FileUrl { get; init; }
    }

    public class LogicAppTriggerResponse
    {

    }
}