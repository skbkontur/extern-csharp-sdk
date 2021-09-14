using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocumentReplyPathExtension
    {
        public static Task<IDocflowWithDocuments> SendReplyAsync(this DocumentReplyPath path, IPAddress address, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Replies.SendReplyAsync(
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                path.ReplyId,
                address.ToString(),
                timeout);
        }
    }
}