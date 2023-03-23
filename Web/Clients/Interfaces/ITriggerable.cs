using System.Threading;
using System.Threading.Tasks;

namespace Web.Clients.Interfaces
{
    public interface ITriggerable
    {
        public Task RunTriggerAsync(string email, string fileName, string fileUrl, CancellationToken cancellationToken);
    }
}