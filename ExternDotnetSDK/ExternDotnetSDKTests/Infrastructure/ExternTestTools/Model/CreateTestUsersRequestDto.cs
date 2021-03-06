using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Информация для генерации пользователя
    /// </summary>
    [DataContract]
    public class CreateTestUsersRequestDto
    {
        /// <param name="phone">Номер телефона. Параметр обязательный, так как на данный номер будет приходить смс сообщение для подтверждения подписания документов (required).</param>
        /// <param name="inn">ИНН организации. Нужен для выпуска сертификата. Если не передать, он будет сгенерирован.</param>
        /// <param name="kpp">КПП. Нужен для выпуска сертификата, если его нет, он будет сгенерирован.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="surname">Фамилия.</param>
        /// <param name="patronymic">Отчество.</param>
        /// <param name="organizationName">Наименование организации пользователя.</param>
        public CreateTestUsersRequestDto(string phone = default, string inn = default, string kpp = default, string firstName = default, string surname = default, string patronymic = default, string organizationName = default)
        {
            Phone = phone ?? throw new InvalidDataException("phone is a required property for CreateTestUsersRequestDto and cannot be null");
            Inn = inn;
            Kpp = kpp;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            OrganizationName = organizationName;
        }

        /// <summary>
        /// Номер телефона. Параметр обязательный, так как на данный номер будет приходить смс сообщение для подтверждения подписания документов
        /// </summary>
        /// <value>Номер телефона. Параметр обязательный, так как на данный номер будет приходить смс сообщение для подтверждения подписания документов</value>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// ИНН организации. Нужен для выпуска сертификата. Если не передать, он будет сгенерирован
        /// </summary>
        /// <value>ИНН организации. Нужен для выпуска сертификата. Если не передать, он будет сгенерирован</value>
        [DataMember(Name = "inn", EmitDefaultValue = false)]
        public string Inn { get; set; }

        /// <summary>
        /// КПП. Нужен для выпуска сертификата, если его нет, он будет сгенерирован
        /// </summary>
        /// <value>КПП. Нужен для выпуска сертификата, если его нет, он будет сгенерирован</value>
        [DataMember(Name = "kpp", EmitDefaultValue = false)]
        public string Kpp { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        /// <value>Имя</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        /// <value>Фамилия</value>
        [DataMember(Name = "surname", EmitDefaultValue = false)]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        /// <value>Отчество</value>
        [DataMember(Name = "patronymic", EmitDefaultValue = false)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Наименование организации пользователя
        /// </summary>
        /// <value>Наименование организации пользователя</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateTestUsersRequestDto {\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  Kpp: ").Append(Kpp).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  Patronymic: ").Append(Patronymic).Append("\n");
            sb.Append("  OrganizationName: ").Append(OrganizationName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}