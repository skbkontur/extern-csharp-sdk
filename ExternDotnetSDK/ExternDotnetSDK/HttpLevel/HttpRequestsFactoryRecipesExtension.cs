#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Errors;

namespace Kontur.Extern.Client.HttpLevel
{
    internal static class HttpRequestsFactoryRecipesExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IHttpRequestsFactory httpRequestsFactory, string url, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Get(url.ToUrl()).SendAsync(timeout).ConfigureAwait(false);
            return response.GetBytes();
        }

        [ItemCanBeNull]
        public static Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TimeSpan? timeout = null) => 
            TryGetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);

        public static async Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            return response.Status.IsNotFound 
                ? default 
                : response.GetMessage<TResponseDto>();
        }

        public static Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TimeSpan? timeout = null) => 
            GetAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);

        public static async Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Get(url).SendAsync(timeout).ConfigureAwait(false);
            return response.GetMessage<TResponseDto>();
        }
        
        public static Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto requestDto, TimeSpan? timeout = null) => 
            PutAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);

        public static async Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto requestDto, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Put(url)
                .WithObject(requestDto)
                .SendAsync(timeout).ConfigureAwait(false);
            return response.GetMessage<TResponseDto>();
        }

        public static Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TRequestDto requestDto, TimeSpan? timeout = null) => 
            PostAsync<TRequestDto, TResponseDto>(httpRequestsFactory, url.ToUrl(), requestDto, timeout);

        public static async Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TRequestDto requestDto, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Post(url)
                .WithObject(requestDto)
                .SendAsync(timeout).ConfigureAwait(false);
            return response.GetMessage<TResponseDto>();
        }
        
        public static Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, string url, TimeSpan? timeout = null) => 
            PostAsync<TResponseDto>(httpRequestsFactory, url.ToUrl(), timeout);
        
        public static async Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeSpan? timeout = null)
        {
            var response = await httpRequestsFactory.Post(url).SendAsync(timeout).ConfigureAwait(false);
            return response.GetMessage<TResponseDto>();
        }

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, string url, TimeSpan? timeout = null) => 
            DeleteAsync(httpRequestsFactory, url.ToUrl(), timeout);

        public static Task DeleteAsync(this IHttpRequestsFactory httpRequestsFactory, Uri url, TimeSpan? timeout = null) => 
            httpRequestsFactory.Delete(url).SendAsync(timeout);

        private static bool IgnoreNotFoundApiErrors(IHttpResponse response) =>
            response.Status.IsNotFound && 
            response.TryGetMessage<Error>(out var errorResponse) && 
            errorResponse.IsNotEmpty;
    }
}