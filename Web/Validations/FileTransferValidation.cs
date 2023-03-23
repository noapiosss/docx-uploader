using FluentValidation;
using Web.Models;

namespace Web.Validations
{
    public class FileTransferValidation : AbstractValidator<FileTransferRequest>
    {
        public FileTransferValidation()
        {
            _ = RuleFor(r => r.Email).EmailAddress();
            _ = RuleFor(r => r.File.ContentType).NotNull().Must(type => type.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document", System.StringComparison.Ordinal));
        }
    }
}