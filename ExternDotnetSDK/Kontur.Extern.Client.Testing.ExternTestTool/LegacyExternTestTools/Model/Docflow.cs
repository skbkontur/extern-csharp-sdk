using System;
using System.Text;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Информация о документообороте
    /// </summary>
    public class Docflow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Docflow" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи.</param>
        /// <param name="docflowId">Id документооборота.</param>
        public Docflow(Guid? accountId = default, Guid? docflowId = default)
        {
            AccountId = accountId;
            DocflowId = docflowId;
        }

        /// <summary>
        /// Id учетной записи
        /// </summary>
        /// <value>Id учетной записи</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Id документооборота
        /// </summary>
        /// <value>Id документооборота</value>
        public Guid? DocflowId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Docflow {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  DocflowId: ").Append(DocflowId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}