using System.IO;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.BodyReading
{
    internal readonly struct BodyReadResult
    {
        public readonly Content? Content;
        public readonly Stream? Stream;
        public readonly ResponseCode? ErrorCode;

        public BodyReadResult(Content content)
            : this(content, null, null)
        {
        }

        public BodyReadResult(Stream stream)
            : this(null, stream, null)
        {
        }

        public BodyReadResult(ResponseCode errorCode)
            : this(null, null, errorCode)
        {
        }

        private BodyReadResult(Content? content, Stream? stream, ResponseCode? code)
        {
            (Content, Stream, ErrorCode) = (content, stream, code);
        }
    }
}
