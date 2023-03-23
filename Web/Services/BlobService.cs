using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Web.Clients.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ITriggerable _logicAppClient;

        public BlobService(BlobServiceClient blobServiceClient, ITriggerable logicAppClient)
        {
            _blobServiceClient = blobServiceClient;
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("docs");
            _logicAppClient = logicAppClient;
        }

        public async Task UploadAsync(string email, Stream file, string fileName, CancellationToken cancellationToken)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient($"{email}/{fileName}");
            if (!blobClient.Exists(cancellationToken).Value)
            {
                Dictionary<string, string> metadata = new()
                {
                    { "email", email }
                };

                _ = await blobClient.UploadAsync(file, cancellationToken);
                _ = await blobClient.SetMetadataAsync(metadata, null, cancellationToken);
                System.Console.WriteLine($"File {fileName} already exists");
            }
            else
            {
                System.Console.WriteLine("File already exists");
            }
        }

        public async Task<bool> TryUploadAsync(string email, Stream file, string fileName, CancellationToken cancellationToken)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient($"{email}/{fileName}");
            if (blobClient.Exists(cancellationToken).Value)
            {
                return false;
            }

            _ = await blobClient.UploadAsync(file, cancellationToken);
            await _logicAppClient.RunTriggerAsync(email, fileName, blobClient.Uri.OriginalString, cancellationToken);

            return true;
        }
    }
}