using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Http;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Route("api/uploadfile")]
    public class UploadFileController : BaseController
    {
        private readonly IBlobService _blobService;
        private readonly IValidator<FileTransferRequest> _requestValidator;

        public UploadFileController(IBlobService blobService,
            IValidator<FileTransferRequest> requestValidator,
            ILogger<UploadFileController> logger) : base(logger)
        {
            _blobService = blobService;
            _requestValidator = requestValidator;
        }

        [HttpPost]
        public Task<IActionResult> UploadFile([FromForm] FileTransferRequest request, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                FluentValidation.Results.ValidationResult validationResult = await _requestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    if (validationResult.Errors[0].PropertyName == "Email")
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.InvalidEmail,
                            Message = "Invalid email"
                        });
                    }

                    if (validationResult.Errors[0].PropertyName == "File.ContentType")
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.InvalidFile,
                            Message = "Invalid file type"
                        });
                    }
                }

                using Stream fileStream = request.File.OpenReadStream();

                return await _blobService.TryUploadAsync(request.Email, fileStream, request.File.FileName, cancellationToken)
                    ? Ok()
                    : ToActionResult(new()
                    {
                        Code = ErrorCode.FileAlreadyExists,
                        Message = "File already exists"
                    });
            }, cancellationToken);
        }
    }
}