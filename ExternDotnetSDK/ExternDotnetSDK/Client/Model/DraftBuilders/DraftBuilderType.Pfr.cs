using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.DraftBuilders
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
        }
    }
}