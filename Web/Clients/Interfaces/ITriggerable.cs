using System.Threading;
using System.Threading.Tasks;

namespace Web.Clients.Interfaces
{
    public interface ITriggerable
    {
        public Task RunAsync(string email, string fileName, string fileUrl, CancellationToken cancellationToken);
    }
}