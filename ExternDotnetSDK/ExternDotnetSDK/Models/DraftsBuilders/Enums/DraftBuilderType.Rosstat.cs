using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Enums
{
    partial struct DraftBuilderType
    {
        /// <summary>
        /// Росстат
        /// </summary>
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static class Rosstat
        {
            /// <summary>
            /// Письмо в Росстат
            /// </summary>
            public static readonly DraftBuilderType Letter = "urn:drafts-builder:stat-letter";
        }
    }
}