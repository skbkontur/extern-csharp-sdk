using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    public interface IExtern
    {
        Task ReauthenticateAsync(TimeSpan? timeout = null);
        AccountListPath Accounts { get; }
    }
}