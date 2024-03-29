﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Kontur.Extern.Api.Client.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Signature
    {
        /// <summary>
        /// Идентификатор подписи
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Название подписи
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        public string SignatureCertificateThumbprint { get; set; } = null!;

        /// <summary>
        /// Ссылка на контент подписи
        /// </summary>
        public Link ContentLink { get; set; } = null!;

        /// <summary>
        /// Ссылки для работы с подписью
        /// </summary>
        public Link[] Links { get; set; } = null!;

        /// <summary>
        /// Тип подписанта:
        /// organizationRepresentative — представитель организации,
        /// thirdPartyRepresentative — представитель третьей стороны,
        /// providerRepresentative — представитель оператора ЭДО,
        /// cuRepresentative — представитель инспекции,
        /// cuLocalRepresentative — представитель конечной инспекции
        /// </summary>
        public SignerType SignerType { get; set; }
    }
}