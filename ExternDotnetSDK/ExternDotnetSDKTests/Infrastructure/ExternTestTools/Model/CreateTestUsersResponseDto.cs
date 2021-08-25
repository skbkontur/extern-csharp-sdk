using System;
using System.IO;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные сгенерированного пользователя
    /// </summary>
    public class CreateTestUsersResponseDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTestUsersResponseDto" /> class.
        /// </summary>
        /// <param name="phone">Телефон (required).</param>
        /// <param name="inn">ИНН организации (если не был передан, сгенерирован рандомно).</param>
        /// <param name="kpp">КПП организации (если не был передан, сгенерирован рандомно).</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="surname">Фамилия.</param>
        /// <param name="patronymic">Отчество.</param>
        /// <param name="organizationName">Наименование организации.</param>
        /// <param name="portalUserId">Идентификатор пользователя.</param>
        public CreateTestUsersResponseDto(string phone = default, string inn = default, string kpp = default, string firstName = default, string surname = default, string patronymic = default, string organizationName = default, Guid? portalUserId = default)
        {
            // to ensure "phone" is required (not null)

            Phone = phone ?? throw new InvalidDataException("phone is a required property for CreateTestUsersResponseDto and cannot be null");
            Inn = inn;
            Kpp = kpp;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            OrganizationName = organizationName;
            PortalUserId = portalUserId;
        }

        /// <summary>
        /// Телефон
        /// </summary>
        /// <value>Телефон</value>
        public string Phone { get; set; }

        /// <summary>
        /// ИНН организации (если не был передан, сгенерирован рандомно)
        /// </summary>
        /// <value>ИНН организации (если не был передан, сгенерирован рандомно)</value>
        public string Inn { get; set; }

        /// <summary>
        /// КПП организации (если не был передан, сгенерирован рандомно)
        /// </summary>
        /// <value>КПП организации (если не был передан, сгенерирован рандомно)</value>
        public string Kpp { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        /// <value>Имя</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        /// <value>Фамилия</value>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        /// <value>Отчество</value>
        public string Patronymic { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        /// <value>Наименование организации</value>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        /// <value>Идентификатор пользователя</value>
        public Guid? PortalUserId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateTestUsersResponseDto {\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  Kpp: ").Append(Kpp).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  Patronymic: ").Append(Patronymic).Append("\n");
            sb.Append("  OrganizationName: ").Append(OrganizationName).Append("\n");
            sb.Append("  PortalUserId: ").Append(PortalUserId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}