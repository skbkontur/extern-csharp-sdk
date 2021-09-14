using System.Text;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Информация о сгенерированном календаре отчетности.
    /// </summary>
    public class XmlCalendar
    {
        /// <param name="version">Версия календаря отчетности..</param>
        /// <param name="hash">Хеш SHA256 от содержимого файла XML календаря в кодировке UTF-8, может быть нужен для определения и проверки версии..</param>
        /// <param name="timeStamp">Метка времени календаря отчетности..</param>
        /// <param name="xml">Содержимое XML календаря отчетности..</param>
        public XmlCalendar(string version = default, string hash = default, string timeStamp = default, string xml = default)
        {
            Version = version;
            Hash = hash;
            TimeStamp = timeStamp;
            Xml = xml;
        }

        /// <summary>
        /// Версия календаря отчетности.
        /// </summary>
        /// <value>Версия календаря отчетности.</value>
        public string Version { get; set; }

        /// <summary>
        /// Хеш SHA256 от содержимого файла XML календаря в кодировке UTF-8, может быть нужен для определения и проверки версии.
        /// </summary>
        /// <value>Хеш SHA256 от содержимого файла XML календаря в кодировке UTF-8, может быть нужен для определения и проверки версии.</value>
        public string Hash { get; set; }

        /// <summary>
        /// Метка времени календаря отчетности.
        /// </summary>
        /// <value>Метка времени календаря отчетности.</value>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Содержимое XML календаря отчетности.
        /// </summary>
        /// <value>Содержимое XML календаря отчетности.</value>
        public string Xml { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class XmlCalendar {\n");
            sb.Append("  Version: ").Append(Version).Append("\n");
            sb.Append("  Hash: ").Append(Hash).Append("\n");
            sb.Append("  TimeStamp: ").Append(TimeStamp).Append("\n");
            sb.Append("  Xml: ").Append(Xml).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}