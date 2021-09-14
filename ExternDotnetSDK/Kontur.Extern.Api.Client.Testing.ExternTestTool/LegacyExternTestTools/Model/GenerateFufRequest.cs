using System.IO;
using System.Text;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации ФУФ
    /// </summary>
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
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, за которую отправляется документ
        /// </summary>
        /// <value>Организация, за которую отправляется документ</value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Сведения о доверенности
        /// </summary>
        /// <value>Сведения о доверенности</value>
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
}