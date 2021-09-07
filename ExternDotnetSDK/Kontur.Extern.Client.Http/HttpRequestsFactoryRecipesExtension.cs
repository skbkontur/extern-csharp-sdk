using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Http
{
    public static class HttpRequestsFactoryRecipesExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IHttpRequestsFactory httpRequestsFactory, string url, TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            var response = await httpRequestsFactory.Get(url.ToUrl()).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetBytesAsync().ConfigureAwait(false);
        }

        public static Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return TryGetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            return response.Status.IsNotFound 
                ? default 
                : await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return GetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }
        
        public static Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto requestDto, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return PutAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);
        }

        public static async Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto requestDto, TimeoutSpecification timeout = default)
        {
            if (requestDto is null)
                throw new ArgumentNullException(nameof(requestDto));
            
            var response = await httpRequestsFactory.Put(url)
                .WithObject(requestDto)
                .SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto? requestDto, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return PostAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);
        }

        public static async Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto? requestDto, TimeoutSpecification timeout = default)
        {
            var request = httpRequestsFactory.Post(url);
            
            var sendTask = requestDto is not null 
                ? request.WithObject(requestDto).SendAsync(timeout) 
                : request.SendAsync(timeout);

            var response = await sendTask.ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }
        
        public static Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return PostAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Post(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return DeleteAsync(httpRequestsFactory, url.ToUrl(), timeout);
        }

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, Uri url, in TimeoutSpecification timeout = default) => 
            httpRequestsFactory.Delete(url).SendAsync(timeout);
        
        public static Task<bool> TryDeleteAsync(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            return TryDeleteAsync(httpRequestsFactory, url.ToUrl(), timeout);
        }
        
        public static async Task<bool> TryDeleteAsync(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var httpResponse = await httpRequestsFactory.Delete(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            var httpStatus = httpResponse.Status;
            return !httpStatus.IsNotFound;
        }

        private static bool IgnoreNotFoundApiErrors(IHttpResponse response) => response.Status.IsNotFound && response.HasPayload;
    }
}