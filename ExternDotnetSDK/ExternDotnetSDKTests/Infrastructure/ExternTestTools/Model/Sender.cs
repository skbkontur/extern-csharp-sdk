using System.IO;
using System.Text;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Организация, которая общается с налоговым органом (отправляет и получает документы).  Payer и Sender могут совпадать.
    /// </summary>
    public class Sender
    {
        /// <param name="inn">ИНН (required).</param>
        /// <param name="kpp">КПП.</param>
        /// <param name="name">Название организации.</param>
        /// <param name="certificate">Сертификат (required).</param>
        public Sender(string inn = default, string kpp = default, string name = default, Certificate certificate = default)
        {
            Inn = inn ?? throw new InvalidDataException("inn is a required property for Sender and cannot be null");
            Certificate = certificate ?? throw new InvalidDataException("certificate is a required property for Sender and cannot be null");
            Kpp = kpp;
            Name = name;
        }

        /// <summary>
        /// ИНН
        /// </summary>
        /// <value>ИНН</value>
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        /// <value>КПП</value>
        public string Kpp { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        /// <value>Название организации</value>
        public string Name { get; set; }

        /// <summary>
        /// Сертификат
        /// </summary>
        /// <value>Сертификат</value>
        public Certificate Certificate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Sender {\n");
            sb.Append("  Inn: ").Append(Inn).Append("\n");
            sb.Append("  Kpp: ").Append(Kpp).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Certificate: ").Append(Certificate).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}