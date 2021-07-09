using System.Threading.Tasks;

namespace Kontur.Extern.Client.Primitives.Polling
{
    public interface IPolling
    {
        Task WaitForNextAttempt();
    }
}