using System;
using System.Linq;
using System.Threading.Tasks;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Errors;
using Refit;

namespace ExternDotnetSDK.Clients.Common
{
    public class InnerCommonClient
    {
        protected readonly ILogError LogError;

        public InnerCommonClient(ILogError logError) => LogError = logError;

        public async Task<T> TryExecuteTask<T>(Task<T> task)
        {
            try
            {
                return await task;
            }
            catch (ApiException e)
            {
                await ProcessException(e);
                throw;
            }
        }

        public async Task TryExecuteTask(Task task)
        {
            try
            {
                await task;
            }
            catch (ApiException e)
            {
                await ProcessException(e);
                throw;
            }
        }

        private async Task ProcessException(ApiException exception)
        {
            try
            {
                var error = await exception.GetContentAsAsync<ExternApiError>();
                error.TraceId = exception.Headers.FirstOrDefault(x => x.Key == "X-Kontur-Trace-Id").Value.FirstOrDefault();
                LogError.Error(error);
            }
            catch (Exception e)
            {
                LogError.Error(e.Message);
            }
        }
    }
}