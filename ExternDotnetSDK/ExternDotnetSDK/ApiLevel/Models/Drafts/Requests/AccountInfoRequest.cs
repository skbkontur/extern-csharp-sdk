using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    /// <summary>
    /// Учетная запись организации
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AccountInfoRequest
    {
        private OrganizationInfoRequest organization;

        /// <summary>
        /// ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; }

        /// <summary>
        /// Данные ЮЛ
        /// </summary>
        //[Required]
        public OrganizationInfoRequest Organization
        {
            get => organization ??= new OrganizationInfoRequest();
            set => organization = value;
        }

        /// <summary>
        /// Регистрационный номер ПФР
        /// </summary>
        public string RegistrationNumberPfr { get; set; }

        /// <summary>
        /// Регистрационный номер ФСС
        /// </summary>
        public string RegistrationNumberFss { get; set; }

        /// <summary>
        /// ОКПО (для писем в росстат)
        /// </summary>
        public string Okpo { get; set; }
    }
}