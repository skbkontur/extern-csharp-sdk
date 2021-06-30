using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Учетная запись налогоплательщика - организация, за которую отправляется отчетность. Payer и Sender могут совпадать.
    /// </summary>
    [DataContract]
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
        [DataMember(Name = "inn", EmitDefaultValue = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        /// <value>Название организации</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Данные, специфичные для ЮЛ (КПП)
        /// </summary>
        /// <value>Данные, специфичные для ЮЛ (КПП)</value>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public OrganizationInfo Organization { get; set; }

        /// <summary>
        /// ФИО руководителя
        /// </summary>
        /// <value>ФИО руководителя</value>
        [DataMember(Name = "chiefFio", EmitDefaultValue = false)]
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