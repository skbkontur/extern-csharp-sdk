using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.TransportPackage;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class TransportPackage
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Контент ответного документа
    /// </summary>
    public byte[] Content { get; set; }
}