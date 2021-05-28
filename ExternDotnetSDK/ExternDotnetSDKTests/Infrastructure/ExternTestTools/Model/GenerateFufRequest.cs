using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации ФУФ
    /// </summary>
    [DataContract]
    public class GenerateFufRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateFufRequest" /> class.
        /// </summary>
        /// <param name="sender">Организация, которая будет оправлять документ в налоговый орган (required).</param>
        /// <param name="payer">Организация, за которую отправляется документ.</param>
        /// <param name="warrant">Сведения о доверенности.</param>
        public GenerateFufRequest(Sender sender = default, Payer payer = default, WarrantInfo warrant = default)
        {
            Sender = sender ?? throw new InvalidDataException("sender is a required property for GenerateFufRequest and cannot be null");
            Payer = payer;
            Warrant = warrant;
        }

        /// <summary>
        /// Организация, которая будет оправлять документ в налоговый орган
        /// </summary>
        /// <value>Организация, которая будет оправлять документ в налоговый орган</value>
        [DataMember(Name = "sender", EmitDefaultValue = false)]
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, за которую отправляется документ
        /// </summary>
        /// <value>Организация, за которую отправляется документ</value>
        [DataMember(Name = "payer", EmitDefaultValue = false)]
        public Payer Payer { get; set; }

        /// <summary>
        /// Сведения о доверенности
        /// </summary>
        /// <value>Сведения о доверенности</value>
        [DataMember(Name = "warrant", EmitDefaultValue = false)]
        public WarrantInfo Warrant { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateFufRequest {\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  Warrant: ").Append(Warrant).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Сведения о доверенности
    /// </summary>
    [DataContract]
    public class WarrantInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarrantInfo" /> class.
        /// </summary>
        /// <param name="number">Номер.</param>
        /// <param name="dateBegin">Дата начала действия. Дата в формате ДД.ММ.ГГГГ (required).</param>
        public WarrantInfo(string number = default, string dateBegin = default)
        {
            DateBegin = dateBegin ?? throw new InvalidDataException("dateBegin is a required property for WarrantInfo and cannot be null");
            Number = number;
        }

        /// <summary>
        /// Номер
        /// </summary>
        /// <value>Номер</value>
        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        /// <summary>
        /// Дата начала действия. Дата в формате ДД.ММ.ГГГГ
        /// </summary>
        /// <value>Дата начала действия. Дата в формате ДД.ММ.ГГГГ</value>
        [DataMember(Name = "dateBegin", EmitDefaultValue = false)]
        public string DateBegin { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class WarrantInfo {\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  DateBegin: ").Append(DateBegin).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}