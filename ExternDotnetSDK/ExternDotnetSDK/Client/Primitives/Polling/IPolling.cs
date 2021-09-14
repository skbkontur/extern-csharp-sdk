using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Primitives.Polling
{
    public interface IPolling
    {
        Task WaitForNextAttempt();
    }
}