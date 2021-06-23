using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Client;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Responses;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Requests;

namespace Kontur.Extern.Client.Clients.Authentication.Providers
{
    public interface IAuthenticationProvider
    {
        Task<ServiceResult> AuthenticateAsync(TimeSpan? timeout = null);
        Request ApplyAuth(Request request);

    }
}