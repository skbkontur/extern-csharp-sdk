using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Docflows;

[PublicAPI]
public class SaveDecryptedContentResult
{
    /// <summary>
    /// Идентификатор расшифрованного контента
    /// </summary>
    public Guid ContentId { get; set; }
    /// <summary>
    /// Идентификатор расшифрованного контента
    /// </summary>
    public Link[] Links { get; set; }
}