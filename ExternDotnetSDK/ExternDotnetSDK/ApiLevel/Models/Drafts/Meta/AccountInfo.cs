using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    /// <summary>
    /// Учетная запись организации
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AccountInfo
    {
        private OrganizationInfo organization;

        public AccountInfo() => organization = new OrganizationInfo();

        /// <summary>
        ///     ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Данные о ЮЛ
        /// </summary>
        public OrganizationInfo Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfo();
        }

        /// <summary>
        /// Регистрационный номер ФСС
        /// </summary>
        public string RegistrationNumberFss { get; set; }

        /// <summary>
        /// Регистрационный номер ПФР
        /// </summary>
        public string RegistrationNumberPfr { get; set; }

        /// <summary>
        /// ОКПО (для писем в росстат)
        /// </summary>
        public string Okpo { get; set; }
    }
}