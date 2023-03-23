using Microsoft.AspNetCore.Http;

namespace Web.Models
{
    public class FileTransferRequest
    {
        public string Email { get; init; }
        public IFormFile File { get; init; }
    }
}