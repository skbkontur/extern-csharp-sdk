using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClientVerify
    {
        private readonly FakeHttpMessages httpMessages;

        public FakeClusterClientVerify(FakeHttpMessages httpMessages) => 
            this.httpMessages = httpMessages;
            
        public string? SentContentString
        {
            get
            {
                var request = SentRequest;
                if (request == null)
                    return null;

                if (request.Content != null)
                    return request.Content.ToString();

                if (request.StreamContent != null)
                {
                    var memoryStream = (MemoryStream) request.StreamContent.Stream;
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                return null;
            }
        }
            
        public Request? SentRequest => httpMessages.SentRequests.LastOrDefault();
        public IEnumerable<Request> SentRequests => httpMessages.SentRequests;
    }
}