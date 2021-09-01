using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.DraftBuilders
{
    partial struct DraftBuilderType
    {
        /// <summary>
        /// ФНС
        /// </summary>
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static class Fns
        {
            [PublicAPI]
            [SuppressMessage("ReSharper", "CommentTypo")]
            public class Fns534
            {
                /// <summary>
                /// Ответ на требование
                /// </summary>
                public static readonly DraftBuilderType Inventory = "urn:drafts-builder:fns534-inventory";
            }

            /// <summary>
            /// Регистрация бизнеса
            /// </summary>
            public static readonly DraftBuilderType BusinessRegistration = "urn:drafts-builder:business-registration";
        }
    }
}