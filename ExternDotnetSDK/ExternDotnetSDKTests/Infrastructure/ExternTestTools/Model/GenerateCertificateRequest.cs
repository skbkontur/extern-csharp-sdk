using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации сертификата, если не передать, будут сгенерированы
    /// </summary>
    [DataContract]
    public class GenerateCertificateRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCertificateRequest" /> class.
        /// </summary>
        /// <param name="inn">ИНН организации.</param>
        /// <param name="kpp">КПП организации.</param>
        /// <param name="organizationName">Наименование организации.</param>
        /// <param name="firstName">Имя владельца сертификата.</param>
        /// <param name="surname">Фамилия владельца сертификата.</param>
        /// <param name="patronymic">Отчество владельца сертификата.</param>
        /// <param name="email">Адрес электронной почты владельца сертификата.</param>
        public GenerateCertificateRequest(string inn = default, string kpp = default, string organizationName = default, string firstName = default, string surname = default, string patronymic = default, string email = default)
        {
            Inn = inn;
            Kpp = kpp;
            OrganizationName = organizationName;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            Email = email;
            Encoding = "Unicode";
        }

        /// <summary>
        /// Кодировка reg-файла
        /// </summary>
        /// <value>Кодировка reg-файла</value>
        [DataMember(Name = "encoding", EmitDefaultValue = false)]
        public string Encoding { get; set; }

        /// <summary>
        /// ИНН организации
        /// </summary>
        /// <value>ИНН организации</value>
        [DataMember(Name = "inn", EmitDefaultValue = false)]
        public string Inn { get; set; }

        /// <summary>
        /// КПП организации
        /// </summary>
        /// <value>КПП организации</value>
        [DataMember(Name = "kpp", EmitDefaultValue = false)]
        public string Kpp { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        /// <value>Наименование организации</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Имя владельца сертификата
        /// </summary>
        /// <value>Имя владельца сертификата</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия владельца сертификата
        /// </summary>
        /// <value>Фамилия владельца сертификата</value>
        [DataMember(Name = "surname", EmitDefaultValue = false)]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество владельца сертификата
        /// </summary>
        /// <value>Отчество владельца сертификата</value>
        [DataMember(Name = "patronymic", EmitDefaultValue = false)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Адрес электронной почты владельца сертификата
        /// </summary>
        /// <value>Адрес электронной почты владельца сертификата</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateCertificateRequest {\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  Kpp: ").Append(Kpp).Append("\n");
            sb.Append("  OrganizationName: ").Append(OrganizationName).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  Patronymic: ").Append(Patronymic).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Encoding: ").Append(Encoding).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}