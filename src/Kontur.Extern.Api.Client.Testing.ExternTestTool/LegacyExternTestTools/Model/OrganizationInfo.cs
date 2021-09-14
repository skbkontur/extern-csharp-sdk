using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Реквизиты, специфичные для ЮЛ
    /// </summary>
    [UsedImplicitly]
    public class OrganizationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationInfo" /> class.
        /// </summary>
        /// <param name="kpp">КПП.</param>
        public OrganizationInfo(string kpp = default)
        {
            Kpp = kpp;
        }

        /// <summary>
        /// КПП
        /// </summary>
        /// <value>КПП</value>
        public string Kpp { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrganizationInfo {\n");
            sb.Append("  Kpp: ").Append(Kpp).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}