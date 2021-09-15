using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Certificates;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Certificates
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CertificateList
    {
        /// <summary>
        /// Данные сертификатов
        /// </summary>
        public Certificate[] Certificates { get; set; } = null!;

        /// <summary>
        /// Общее количество сертификатов
        /// </summary>
        public long TotalCount { get; set; }
        
        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        public long Skip { get; set; }
        
        /// <summary>
        /// Количество считанных записей
        /// </summary>
        public long Take { get; set; }
        
        /// <summary>
        /// Ссылки для работы с сертификатами
        /// </summary>
        public Link[] Links { get; set; } = null!;
    }
}