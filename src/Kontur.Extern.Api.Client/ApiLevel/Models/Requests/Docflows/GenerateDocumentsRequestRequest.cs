using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows
{
    [PublicAPI]
    public class GenerateDocumentsRequestRequest
    {
        /// <summary>
        /// Сертификат в формате base64
        /// </summary>
        public byte[] CertificateBase64 { get; set; } = null!;

        /// <summary>
        /// Идентификатор машиночитаемой доверенности
        /// </summary>
        public Guid? MachineReadableWarrantId { get; set; }
    }
}