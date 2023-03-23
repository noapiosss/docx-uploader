using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.Models
{
    public class FileTransferRequest
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public IFormFile File { get; init; }
    }

    public class FileTransferResponse
    {

    }
}