using System.IO;
using System.Text;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Учетная запись налогоплательщика - организация, за которую отправляется отчетность. Payer и Sender могут совпадать.
    /// </summary>
    [UsedImplicitly]
    public class Payer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Payer" /> class.
        /// </summary>
        /// <param name="inn">ИНН налогоплательщика (required).</param>
        /// <param name="name">Название организации.</param>
        /// <param name="organization">Данные, специфичные для ЮЛ (КПП).</param>
        /// <param name="chiefFio">ФИО руководителя.</param>
        public Payer(string inn = default, string name = default, OrganizationInfo organization = default, Fio chiefFio = default)
        {
            Inn = inn ?? throw new InvalidDataException("inn is a required property for Payer and cannot be null");
            Name = name;
            Organization = organization;
            ChiefFio = chiefFio;
        }

        /// <summary>
        /// ИНН налогоплательщика
        /// </summary>
        /// <value>ИНН налогоплательщика</value>
        public string Inn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        /// <value>Название организации</value>
        public string Name { get; set; }

        /// <summary>
        /// Данные, специфичные для ЮЛ (КПП)
        /// </summary>
        /// <value>Данные, специфичные для ЮЛ (КПП)</value>
        public OrganizationInfo Organization { get; set; }

        /// <summary>
        /// ФИО руководителя
        /// </summary>
        /// <value>ФИО руководителя</value>
        public Fio ChiefFio { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Payer {\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  ChiefFio: ").Append(ChiefFio).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}