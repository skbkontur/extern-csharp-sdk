using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Информация о наборе документооборотов, которые были отправлены на учетную запись
    /// </summary>
    [DataContract]
    public class DocflowsSuite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocflowsSuite" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи.</param>
        /// <param name="docflowIds">Перечисление Id документооборотов.</param>
        public DocflowsSuite(Guid? accountId = default, List<Guid?> docflowIds = default)
        {
            AccountId = accountId;
            DocflowIds = docflowIds;
        }

        /// <summary>
        /// Id учетной записи
        /// </summary>
        /// <value>Id учетной записи</value>
        [DataMember(Name = "accountId", EmitDefaultValue = false)]
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Перечисление Id документооборотов
        /// </summary>
        /// <value>Перечисление Id документооборотов</value>
        [DataMember(Name = "docflowIds", EmitDefaultValue = false)]
        public List<Guid?> DocflowIds { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DocflowsSuite {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  DocflowIds: ").Append(DocflowIds).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}