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
            /// <summary>
            /// Декларация и 2-НДФЛ
            /// </summary>
            [PublicAPI]
            [SuppressMessage("ReSharper", "CommentTypo")]
            public class Fns534
            {
                /// <summary>
                /// Письмо в ФНС
                /// </summary>
                public static readonly DraftBuilderType Letter = "urn:drafts-builder:fns534-letter";
                
                /// <summary>
                /// Ответ на требование
                /// </summary>
                public static readonly DraftBuilderType Inventory = "urn:drafts-builder:fns534-inventory";
            }

            /// <summary>
            /// Регистрация бизнеса
            /// </summary>
            [PublicAPI]
            [SuppressMessage("ReSharper", "CommentTypo")]
            public static class BusinessRegistration 
            {
                public static readonly DraftBuilderType RegistrationLegacy = "urn:externapi:business-registration:registration"; 
                public static readonly DraftBuilderType Registration = "urn:drafts-builder:business-registration";
            }
        }
    }
}