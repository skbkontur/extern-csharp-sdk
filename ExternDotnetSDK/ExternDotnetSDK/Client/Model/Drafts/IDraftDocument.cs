#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.Uploading;
using Kontur.Extern.Api.Client.Cryptography;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    [PublicAPI]
    public interface IDraftDocument
    {
        Guid DocumentId { get; }

        ValueTask<(Signature? signature, DocumentRequest request)> CreateSignedRequestAsync(
            Guid accountId,
            IContentService uploader,
            ICrypt crypt,
            TimeSpan? uploadTimeout);
    }
}