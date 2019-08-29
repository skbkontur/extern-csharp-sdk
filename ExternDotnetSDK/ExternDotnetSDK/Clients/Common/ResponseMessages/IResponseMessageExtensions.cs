using System;
using System.Threading.Tasks;
using KeApiClientOpenSdk.Clients.Common.Logging;

namespace KeApiClientOpenSdk.Clients.Common.ResponseMessages
{
    // ReSharper disable once InconsistentNaming
    internal static class IResponseMessageExtensions
    {
        public static async Task<string> TryGetResponseAsync(this IResponseMessage response, ILogger logger)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                logger.Log(response, e);
                throw;
            }
        }
    }
}