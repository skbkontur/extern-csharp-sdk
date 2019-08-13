using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Logging;

namespace ExternDotnetSDK.Clients.Common
{
    public class InnerCommonClient
    {
        protected readonly ILog Log;

        public InnerCommonClient(ILog log) => Log = log;

        public async Task<T> TryExecuteTask<T>(Task<T> task)
        {
            try
            {
                return await task;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public async Task TryExecuteTask(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}