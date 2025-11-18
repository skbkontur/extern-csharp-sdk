using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;

[PublicAPI]
public class SaveDecryptedContentRequest
{
    /// <summary>
    /// Идентификатор расшифрованного контента
    /// </summary>
    public Guid ContentId { get; set; }
}