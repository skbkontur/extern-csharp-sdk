using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    [UsedImplicitly]
    public class ExternAccountDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternAccountDto" /> class.
        /// </summary>
        /// <param name="inn">ИНН (сгенерирован, если в запросе не задан).</param>
        /// <param name="kpp">КПП (сгенерирован, если в запросе не задан).</param>
        /// <param name="firstName">Имя владельца сертификата (сгенерировано, если в запросе не задано).</param>
        /// <param name="surname">Фамилия владельца сертификата (сгенерирована, если в запросе не задана).</param>
        /// <param name="patronymic">Отчество владельца сертификата.</param>
        /// <param name="organizationName">Название организации (сгенерировано, если в запросе не задано).</param>
        public ExternAccountDto(string inn = default, string kpp = default, string firstName = default, string surname = default, string patronymic = default, string organizationName = default)
        {
            Inn = inn;
            Kpp = kpp;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            OrganizationName = organizationName;
        }

        /// <summary>
        /// ИНН (сгенерирован, если в запросе не задан)
        /// </summary>
        /// <value>ИНН (сгенерирован, если в запросе не задан)</value>
        public string Inn { get; set; }

        /// <summary>
        /// КПП (сгенерирован, если в запросе не задан)
        /// </summary>
        /// <value>КПП (сгенерирован, если в запросе не задан)</value>
        public string Kpp { get; set; }

        /// <summary>
        /// Имя владельца сертификата (сгенерировано, если в запросе не задано)
        /// </summary>
        /// <value>Имя владельца сертификата (сгенерировано, если в запросе не задано)</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия владельца сертификата (сгенерирована, если в запросе не задана)
        /// </summary>
        /// <value>Фамилия владельца сертификата (сгенерирована, если в запросе не задана)</value>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество владельца сертификата
        /// </summary>
        /// <value>Отчество владельца сертификата</value>
        public string Patronymic { get; set; }

        /// <summary>
        /// Название организации (сгенерировано, если в запросе не задано)
        /// </summary>
        /// <value>Название организации (сгенерировано, если в запросе не задано)</value>
        public string OrganizationName { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ExternAccountDto {\n");
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