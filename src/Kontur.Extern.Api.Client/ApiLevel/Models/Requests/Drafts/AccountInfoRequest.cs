using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    /// <summary>
    /// Учетная запись организации
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AccountInfoRequest
    {
        private OrganizationInfoRequest? organization;

        /// <summary>
        /// ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; } = null!;

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
        public PfrRegNumber? RegistrationNumberPfr { get; set; }

        /// <summary>
        /// Регистрационный номер СФР
        /// </summary>
        public SfrRegNumber? RegistrationNumberSfr { get; set; }

        /// <summary>
        /// Регистрационный номер ФСС
        /// </summary>
        public FssRegNumber? RegistrationNumberFss { get; set; }

        /// <summary>
        /// ОКПО (для писем в росстат)
        /// </summary>
        public Okpo? Okpo { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public Ogrn? Ogrn { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public Snils? Snils { get; set; }
    }
}