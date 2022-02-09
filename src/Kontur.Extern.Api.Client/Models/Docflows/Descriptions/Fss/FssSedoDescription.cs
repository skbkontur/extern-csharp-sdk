using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public FssRegNumber RegistrationNumber { get; set; } = null!;


        /// <summary>
        /// Идентификатор запроса в СЭДО ФСС
        /// </summary>
        public string? RequestId { get; set; }
    }
}