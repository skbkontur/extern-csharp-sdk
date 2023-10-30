using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;

// ReSharper disable MethodSupportsCancellation

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers
{
    internal class TimeoutProvider
    {
        private readonly TimeSpan abortWaitTimeout;
        private readonly ILog log;

        public TimeoutProvider(TimeSpan abortWaitTimeout, ILog log)
        {
            this.abortWaitTimeout = abortWaitTimeout;
            this.log = log;
        }

        public async Task<Response> SendWithTimeoutAsync(
            Func<Request, CancellationToken, Task<Response>> send, Request request, TimeSpan timeout, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return Responses.Canceled;

            if (timeout.TotalMilliseconds < 1)
            {
                LogRequestTimeout(request, timeout);
                return Responses.Timeout;
            }

            using (var timeoutCancellation = new CancellationTokenSource())
            using (var requestCancellation = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                var timeoutTask = Task.Delay(timeout, timeoutCancellation.Token);

                var sendTask = send(request, requestCancellation.Token);

                var completedTask = await Task.WhenAny(timeoutTask, sendTask).ConfigureAwait(false);
                if (completedTask is Task<Response> taskWithResponse)
                {
                    timeoutCancellation.Cancel();
                    return taskWithResponse.GetAwaiter().GetResult();
                }

                requestCancellation.Cancel();

                LogRequestTimeout(request, timeout);

                var sendTaskContinuation = sendTask.ContinueWith(
                    task =>
                    {
                        if (task.IsCompleted)
                            task.GetAwaiter().GetResult().Dispose();
                    });

                using (var abortCancellation = new CancellationTokenSource())
                {
                    var abortWaitingDelay = Task.Delay(abortWaitTimeout, abortCancellation.Token);

                    await Task.WhenAny(sendTaskContinuation, abortWaitingDelay).ConfigureAwait(false);

                    abortCancellation.Cancel();
                }

                if (!sendTask.IsCompleted)
                    LogFailedToWaitForRequestAbort();

                return Responses.Timeout;
            }
        }

        private void LogRequestTimeout(Request request, TimeSpan timeout)
            => log.Warn("Request timed out. Target = '{Target}'. Timeout = {Timeout}.", new
            {
                Target = request.Url.Authority,
                Timeout = timeout.ToPrettyString(),
                TimeoutMs = timeout.TotalMilliseconds
            });

        private void LogFailedToWaitForRequestAbort()
            => log.Warn("Timed out request was aborted but did not complete in {RequestAbortTimeout}.", abortWaitTimeout.ToPrettyString());
    }
}