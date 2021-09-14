using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.Drafts.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    partial struct PfrLetterType
    {
        /// <summary>
        /// Письмо
        /// Обычное письмо
        /// </summary>
        public static readonly PfrLetterType Letter = "urn:pfr-letter-category:letter";
        
        /// <summary>
        /// Ретроконверсия	Перевод бумажных архивов ранее представленных сведений в электронный вид. Подробнее здесь.
        /// </summary>
        // ReSharper disable once StringLiteralTypo
        public static readonly PfrLetterType RetroConversion= "urn:pfr-letter-category:retroconversion";
        
        /// <summary>
        /// Рассылка
        /// Входящий ДО из ПФР
        /// </summary>
        public static readonly PfrLetterType News= "urn:pfr-letter-category:news";
        
        /// <summary>
        /// Администрирование (Документы по Федеральному закону от 24.07.2009 N 212-ФЗ)
        /// Требования ПФР
        /// </summary>
        public static readonly PfrLetterType Administrative= "urn:pfr-letter-category:administrative";
        
        /// <summary>
        /// Макет пенсионного дела.
        /// Страхователь (работодатель) ежегодно представляет в УПФР по месту регистрации списки лиц, уходящих на пенсию в следующем году, и макет пенсионного дела на каждого будущего пенсионера.
        /// Макет включает в себя набор документов: паспорт, СНИЛС, трудовая книжка, военный билет и т.д.. Более полный список можно уточнить в УПФР.
        /// Важно! В некоторых регионах макеты необходимо отправлять в другие коды УПФР, отличные от тех, где организация стоит на учете
        /// </summary>
        public static readonly PfrLetterType PensionPrototype = "urn:pfr-letter-category:pension-prototype";

        /// <summary>
        /// Заявление об установлении пенсии
        /// Отдельный документооборот, в рамках которого организация направляет в ПФР заявление пенсионера об установлении пенсии.
        /// </summary>
        public static readonly PfrLetterType PensionApplication = "urn:pfr-letter-category:pension-application";
    }
}