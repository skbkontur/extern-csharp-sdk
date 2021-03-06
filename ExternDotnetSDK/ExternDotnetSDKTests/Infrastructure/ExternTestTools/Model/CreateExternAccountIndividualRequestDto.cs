using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Информация для генерации учетной записи экстерна и сертификата
    /// </summary>
    [DataContract]
    public class CreateExternAccountIndividualRequestDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExternAccountIndividualRequestDto" /> class.
        /// </summary>
        /// <param name="inn">ИНН ИП. Оставьте пустым для случайного значения.</param>
        /// <param name="firstName">Имя владельца сертификата. Оставьте пустым для случайного значения.</param>
        /// <param name="surname">Фамилия владельца сертификата. Оставьте пустым для случайного значения.</param>
        /// <param name="patronymic">Отчество владельца сертификата. Оставьте пустым для случайного значения.</param>
        /// <param name="organizationName">Название организации. Оставьте пустым для случайного значения.</param>
        public CreateExternAccountIndividualRequestDto(string inn = default, string firstName = default, string surname = default, string patronymic = default, string organizationName = default)
        {
            Inn = inn;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            OrganizationName = organizationName;
        }

        /// <summary>
        /// ИНН ИП. Оставьте пустым для случайного значения
        /// </summary>
        /// <value>ИНН ИП. Оставьте пустым для случайного значения</value>
        [DataMember(Name = "inn", EmitDefaultValue = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Имя владельца сертификата. Оставьте пустым для случайного значения
        /// </summary>
        /// <value>Имя владельца сертификата. Оставьте пустым для случайного значения</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия владельца сертификата. Оставьте пустым для случайного значения
        /// </summary>
        /// <value>Фамилия владельца сертификата. Оставьте пустым для случайного значения</value>
        [DataMember(Name = "surname", EmitDefaultValue = false)]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество владельца сертификата. Оставьте пустым для случайного значения
        /// </summary>
        /// <value>Отчество владельца сертификата. Оставьте пустым для случайного значения</value>
        [DataMember(Name = "patronymic", EmitDefaultValue = false)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Название организации. Оставьте пустым для случайного значения
        /// </summary>
        /// <value>Название организации. Оставьте пустым для случайного значения</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateExternAccountIndividualRequestDto {\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  Patronymic: ").Append(Patronymic).Append("\n");
            sb.Append("  OrganizationName: ").Append(OrganizationName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}