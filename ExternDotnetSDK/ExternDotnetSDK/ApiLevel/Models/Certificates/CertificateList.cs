using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Certificates
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CertificateList
    {
        /// <summary>
        /// Данные сертификатов
        /// </summary>
        public CertificateDto[] Certificates { get; set; }
        
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
        public Link[] Links { get; set; }
    }
}