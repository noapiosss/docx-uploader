using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IBlobService
    {
        public Task UploadAsync(string email, Stream file, string fileName, CancellationToken cancellationToken);
        public Task<bool> TryUploadAsync(string email, Stream file, string fileName, CancellationToken cancellationToken);
    }
}