#nullable enable
using Kontur.Extern.Client.Exceptions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.HttpLevel
{
    public readonly struct HttpStatus
    {
        private readonly ResponseCode? statusCode;

        internal HttpStatus(ResponseCode statusCode) => this.statusCode = statusCode;

        public int StatusCode => (int) (statusCode ?? ResponseCode.Ok);

        public bool IsSuccessful => statusCode == null || statusCode.Value.IsSuccessful();
        public bool IsBadRequest => statusCode == ResponseCode.BadRequest;

        public void EnsureSuccess()
        {
            if (statusCode.HasValue && !statusCode.Value.IsSuccessful())
                throw Errors.UnsuccessfulResponse(statusCode.Value);
        }
    }
}