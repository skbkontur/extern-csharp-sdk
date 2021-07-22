using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.HttpLevel.Models;

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

        IHttpRequest Authorization(string scheme, in Base64String parameter);
        
        /// <summary>
        /// Send a request and ensures successful response 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IHttpResponse> SendAsync(TimeSpan? timeout = null);
    }
}