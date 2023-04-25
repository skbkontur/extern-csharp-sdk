﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoDemandReplyResultDocumentRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Результат проверки ответа на требование является положительным
        /// </summary>
        public bool IsPositive { get; set; }
    }
}