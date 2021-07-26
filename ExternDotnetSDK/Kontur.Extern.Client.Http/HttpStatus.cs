#nullable enable
using Kontur.Extern.Client.Http.Exceptions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http
{
    public readonly struct HttpStatus
    {
        private readonly ResponseCode? statusCode;

        internal HttpStatus(ResponseCode statusCode) => this.statusCode = statusCode;

        public int StatusCode => (int) (statusCode ?? ResponseCode.Ok);

        public bool IsSuccessful => statusCode == null || statusCode.Value.IsSuccessful();
        public bool IsBadRequest => statusCode == ResponseCode.BadRequest;
        public bool IsNotFound => statusCode == ResponseCode.NotFound;
        
        public bool IsClientError
        {
            get
            {
                if (statusCode == null)
                    return false;
                return (int) statusCode.Value is >= 400 and < 500;
            }
        }

        public void EnsureSuccess()
        {
            if (statusCode.HasValue && !statusCode.Value.IsSuccessful())
                throw Errors.UnsuccessfulResponse(statusCode.Value);
        }
    }
}