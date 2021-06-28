using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common.Requests;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    public interface IRequestSender
    {
        Task SendAsync(RequestBuilder requestBuilder, TimeSpan? timeout = null);
        Task<TResponse> SendAsync<TResponse>(RequestBuilder requestBuilder, TimeSpan? timeout = null);
    }
}