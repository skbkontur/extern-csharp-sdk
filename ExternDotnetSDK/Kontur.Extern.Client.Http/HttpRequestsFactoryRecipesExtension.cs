using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Http
{
    public static class HttpRequestsFactoryRecipesExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IHttpRequestsFactory httpRequestsFactory, string url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Get(url.ToUrl()).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetBytesAsync().ConfigureAwait(false);
        }

        public static Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default) => 
            TryGetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);

        public static async Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            return response.Status.IsNotFound 
                ? default 
                : await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default) => 
            GetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);

        public static async Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }
        
        public static Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto requestDto, in TimeoutSpecification timeout = default) => 
            PutAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);

        public static async Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto requestDto, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Put(url)
                .WithObject(requestDto)
                .SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto? requestDto, in TimeoutSpecification timeout = default) => 
            PostAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);

        public static async Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto? requestDto, TimeoutSpecification timeout = default)
        {
            var request = httpRequestsFactory.Post(url);
            
            var sendTask = requestDto is not null 
                ? request.WithObject(requestDto).SendAsync(timeout) 
                : request.SendAsync(timeout);

            var response = await sendTask.ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }
        
        public static Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default) => 
            PostAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);
        
        public static async Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestsFactory.Post(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, string url, in TimeoutSpecification timeout = default) => 
            DeleteAsync(httpRequestsFactory, url.ToUrl(), timeout);

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, Uri url, in TimeoutSpecification timeout = default) => 
            httpRequestsFactory.Delete(url).SendAsync(timeout);

        private static bool IgnoreNotFoundApiErrors(IHttpResponse response) =>
            response.Status.IsNotFound && 
            response.HasPayload;
    }
}