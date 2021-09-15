#nullable disable
using System.IO;
using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Сведения о доверенности
    /// </summary>
    [UsedImplicitly]
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
        public string Number { get; set; }

        /// <summary>
        /// Дата начала действия. Дата в формате ДД.ММ.ГГГГ
        /// </summary>
        /// <value>Дата начала действия. Дата в формате ДД.ММ.ГГГГ</value>
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