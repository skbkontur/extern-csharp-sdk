using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Api.Client.Http
{
    public static class HttpRequestFactoryRecipesExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IHttpRequestFactory httpRequestFactory, string url, TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            var response = await httpRequestFactory.Get(url.ToUrl()).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetBytesAsync().ConfigureAwait(false);
        }

        public static async Task<byte[]?> TryGetBytesAsync(this IHttpRequestFactory httpRequestFactory, string url, TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            var response = await httpRequestFactory.Get(url.ToUrl()).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            return response.Status.IsNotFound
                ? default
                : await response.GetBytesAsync().ConfigureAwait(false);
        }

        public static Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return TryGetAsync<TResponseDto>(httpRequestFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto?> TryGetAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestFactory.Get(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            return response.Status.IsNotFound
                ? default
                : await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return GetAsync<TResponseDto>(httpRequestFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto> GetAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestFactory.Get(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestFactory httpRequestFactory, string url, TRequestDto requestDto, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return PutAsync<TRequestDto, TResponseDto>(httpRequestFactory, url.ToUrl(), requestDto, timeout);
        }

        public static async Task<TResponseDto> PutAsync<TRequestDto, TResponseDto>(this IHttpRequestFactory httpRequestFactory, Uri url, TRequestDto requestDto, TimeoutSpecification timeout = default)
        {
            if (requestDto is null)
                throw new ArgumentNullException(nameof(requestDto));

            var response = await httpRequestFactory.Put(url)
                .WithObject(requestDto)
                .SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestFactory httpRequestFactory, string url, TRequestDto? requestDto, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return PostAsync<TRequestDto, TResponseDto>(httpRequestFactory, url.ToUrl(), requestDto, timeout);
        }

        public static async Task<TResponseDto> PostAsync<TRequestDto, TResponseDto>(this IHttpRequestFactory httpRequestFactory, Uri url, TRequestDto? requestDto, TimeoutSpecification timeout = default)
        {
            var request = httpRequestFactory.Post(url);

            var sendTask = requestDto is not null
                ? request.WithObject(requestDto).SendAsync(timeout)
                : request.SendAsync(timeout);

            var response = await sendTask.ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return PostAsync<TResponseDto>(httpRequestFactory, url.ToUrl(), timeout);
        }

        public static async Task<TResponseDto> PostAsync<TResponseDto>(this IHttpRequestFactory httpRequestFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var response = await httpRequestFactory.Post(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<TResponseDto>().ConfigureAwait(false);
        }

        public static async Task<IHttpResponse> RawPostAsync<TRequestDto>(
            this IHttpRequestFactory httpRequestFactory,
            Uri url,
            TRequestDto? requestDto,
            TimeoutSpecification timeout = default)
        {
            var request = httpRequestFactory.Post(url);

            var sendTask = requestDto is not null
                ? request.WithObject(requestDto).SendAsync(timeout, IgnoreAllErrors)
                : request.SendAsync(timeout, IgnoreAllErrors);

            return await sendTask.ConfigureAwait(false);
        }

        public static Task DeleteAsync(this IHttpRequestFactory httpRequestFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return DeleteAsync(httpRequestFactory, url.ToUrl(), timeout);
        }

        public static Task DeleteAsync(this IHttpRequestFactory httpRequestFactory, Uri url, in TimeoutSpecification timeout = default) =>
            httpRequestFactory.Delete(url).SendAsync(timeout);

        public static Task<bool> TryDeleteAsync(this IHttpRequestFactory httpRequestFactory, string url, in TimeoutSpecification timeout = default)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return TryDeleteAsync(httpRequestFactory, url.ToUrl(), timeout);
        }

        public static async Task<bool> TryDeleteAsync(this IHttpRequestFactory httpRequestFactory, Uri url, TimeoutSpecification timeout = default)
        {
            var httpResponse = await httpRequestFactory.Delete(url).SendAsync(timeout, IgnoreNotFoundApiErrors).ConfigureAwait(false);
            var httpStatus = httpResponse.Status;
            return !httpStatus.IsNotFound;
        }

        private static bool IgnoreNotFoundApiErrors(IHttpResponse response) => response.Status.IsNotFound && response.HasPayload;

        private static bool IgnoreAllErrors(IHttpResponse response) => true;
    }
}