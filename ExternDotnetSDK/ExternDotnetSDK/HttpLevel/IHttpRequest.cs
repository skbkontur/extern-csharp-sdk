using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpRequest
    {
        /// <summary>
        /// Add accept header to the request
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        IHttpRequest Accept(string contentType);
        
        /// <summary>
        /// Send a request and ensures successful response 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IHttpResponse> SendAsync(TimeSpan? timeout = null);

        /// <summary>
        /// Send a request and return successful or failed response 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IHttpResponse> TrySendAsync(TimeSpan? timeout = null);
    }
}