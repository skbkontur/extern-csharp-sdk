using System;
using System.Net.Http;
using System.Threading;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers
{
    internal class DisposableState : IDisposable
    {
        private int disposeBarrier;

        public HttpRequestMessage? Request { get; set; }

        public HttpResponseMessage? Response { get; set; }

        public void PreventNextDispose()
        {
            Interlocked.Exchange(ref disposeBarrier, 1);
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposeBarrier, 0) > 0)
                return;

            DisposeRequest();
            DisposeResponse();
        }

        private void DisposeRequest()
        {
            try
            {
                Request?.Dispose();
            }
            catch
            {
                // ignored
            }
            finally
            {
                Request = null;
            }
        }

        private void DisposeResponse()
        {
            try
            {
                Response?.Dispose();
            }
            catch
            {
                // ignored
            }
            finally
            {
                Response = null;
            }
        }
    }
}
