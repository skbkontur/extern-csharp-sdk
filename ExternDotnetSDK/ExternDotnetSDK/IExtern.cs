using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    public interface IExtern
    {
        Task ReauthenticateAsync(TimeSpan? timeout = null);
        AccountListPath Accounts { get; }
    }
}