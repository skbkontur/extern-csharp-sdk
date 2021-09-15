#nullable disable
using System;
using System.Text;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Данные сгенерированной учетной записи экстерна и сертификата
    /// </summary>
    public class CreateExternAccountResponseDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExternAccountResponseDto" /> class.
        /// </summary>
        /// <param name="refinedRequest">Данные из запроса. Для незаполненных полей значения были сгенерированы..</param>
        /// <param name="portalLogin">Портальный логин.</param>
        /// <param name="portalPassword">Портальный пароль.</param>
        /// <param name="certificateInfo">Информация о созданном сертификате.</param>
        /// <param name="portalUserId">Портальный пользователь.</param>
        /// <param name="organizationId">Организация (или ИП).</param>
        /// <param name="accountId">Лицевой счет.</param>
        /// <param name="userId">gO Группа организаций (или устар. Пользователь экстерна Uкэ).</param>
        /// <param name="abonId">gU Группа портальных пользователей (или устар. Абонент экстерна А).</param>
        public CreateExternAccountResponseDto(ExternAccountDto refinedRequest = default, string portalLogin = default, string portalPassword = default, CertificateInfo certificateInfo = default, Guid? portalUserId = default, Guid? organizationId = default, Guid? accountId = default, Guid? userId = default, Guid? abonId = default)
        {
            RefinedRequest = refinedRequest;
            PortalLogin = portalLogin;
            PortalPassword = portalPassword;
            CertificateInfo = certificateInfo;
            PortalUserId = portalUserId;
            OrganizationId = organizationId;
            AccountId = accountId;
            UserId = userId;
            AbonId = abonId;
        }

        /// <summary>
        /// Данные из запроса. Для незаполненных полей значения были сгенерированы.
        /// </summary>
        /// <value>Данные из запроса. Для незаполненных полей значения были сгенерированы.</value>
        public ExternAccountDto RefinedRequest { get; set; }

        /// <summary>
        /// Портальный логин
        /// </summary>
        /// <value>Портальный логин</value>
        public string PortalLogin { get; set; }

        /// <summary>
        /// Портальный пароль
        /// </summary>
        /// <value>Портальный пароль</value>
        public string PortalPassword { get; set; }

        /// <summary>
        /// Информация о созданном сертификате
        /// </summary>
        /// <value>Информация о созданном сертификате</value>
        public CertificateInfo CertificateInfo { get; set; }

        /// <summary>
        /// Портальный пользователь
        /// </summary>
        /// <value>Портальный пользователь</value>
        public Guid? PortalUserId { get; set; }

        /// <summary>
        /// Организация (или ИП)
        /// </summary>
        /// <value>Организация (или ИП)</value>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        /// <value>Лицевой счет</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// gO Группа организаций (или устар. Пользователь экстерна Uкэ)
        /// </summary>
        /// <value>gO Группа организаций (или устар. Пользователь экстерна Uкэ)</value>
        public Guid? UserId { get; set; }

        /// <summary>
        /// gU Группа портальных пользователей (или устар. Абонент экстерна А)
        /// </summary>
        /// <value>gU Группа портальных пользователей (или устар. Абонент экстерна А)</value>
        public Guid? AbonId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateExternAccountResponseDto {\n");
            sb.Append("  RefinedRequest: ").Append(RefinedRequest).Append("\n");
            sb.Append("  PortalLogin: ").Append(PortalLogin).Append("\n");
            sb.Append("  PortalPassword: ").Append(PortalPassword).Append("\n");
            sb.Append("  CertificateInfo: ").Append(CertificateInfo).Append("\n");
            sb.Append("  PortalUserId: ").Append(PortalUserId).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  AbonId: ").Append(AbonId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}