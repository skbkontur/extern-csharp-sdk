using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Common.Logging;

namespace Kontur.Extern.Client.ApiLevel.Clients.Common.ResponseMessages
{
    // ReSharper disable once InconsistentNaming
    internal static class IResponseMessageExtensions
    {
        public static async Task<string> TryGetResponseAsync(this IResponseMessage response, ILogger logger)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                logger.Log(response, e);
                throw;
            }
        }
    }
}