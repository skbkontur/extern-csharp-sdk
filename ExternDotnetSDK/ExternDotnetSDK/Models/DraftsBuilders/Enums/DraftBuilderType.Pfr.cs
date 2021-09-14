using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums
{
    partial struct DraftBuilderType
    {
        /// <summary>
        /// ПФР
        /// </summary>
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static class Pfr
        {
            /// <summary>
            /// Сведения ПФР
            /// </summary>
            public static readonly DraftBuilderType Report = "urn:drafts-builder:pfr-report";
            
            /// <summary>
            /// Текст письма
            /// </summary>
            public static readonly DraftBuilderType Letter = "urn:drafts-builder:pfr-letter";
            
            /// <summary>
            /// Уточнение платежей
            /// </summary>
            public static readonly DraftBuilderType Ios = "urn:drafts-builder:pfr-ios";
        }
    }
}