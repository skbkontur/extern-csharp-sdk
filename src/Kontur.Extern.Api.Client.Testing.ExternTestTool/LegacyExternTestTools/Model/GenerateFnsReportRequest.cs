#nullable disable
using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации исходящего ДО (отчет ФНС)
    /// </summary>
    [UsedImplicitly]
    public class GenerateFnsReportRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateFnsReportRequest" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи, для которой необходимо сформировать ДО (required).</param>
        /// <param name="sender">Организация, которая будет отправителем отчета (required).</param>
        /// <param name="payer">Организация, за которую отправляется отчетность (required).</param>
        /// <param name="ifnsCode">Код тестовой инспекции, в которую отправляется отчет.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702.</param>
        /// <param name="state">Статус документооборота. Подробнее о значении статуса читай в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)  Возможные значения: \&quot;urn:docflow-common-status:sent\&quot;, \&quot;urn:docflow-common-status:response-arrived\&quot;, \&quot;urn:docflow-common-status:response-processed\&quot;, \&quot;urn:docflow-common-status:finished\&quot; (required).</param>
        public GenerateFnsReportRequest(Guid? accountId = default, Sender sender = default, Payer payer = default, string ifnsCode = default, string state = default)
        {
            if (accountId == null)
            {
                throw new InvalidDataException("accountId is a required property for GenerateFnsReportRequest and cannot be null");
            }

            AccountId = accountId;
            Sender = sender ?? throw new InvalidDataException("sender is a required property for GenerateFnsReportRequest and cannot be null");
            Payer = payer ?? throw new InvalidDataException("payer is a required property for GenerateFnsReportRequest and cannot be null");
            State = state ?? throw new InvalidDataException("state is a required property for GenerateFnsReportRequest and cannot be null");
            IfnsCode = ifnsCode;
        }

        /// <summary>
        /// Id учетной записи, для которой необходимо сформировать ДО
        /// </summary>
        /// <value>Id учетной записи, для которой необходимо сформировать ДО</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Организация, которая будет отправителем отчета
        /// </summary>
        /// <value>Организация, которая будет отправителем отчета</value>
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, за которую отправляется отчетность
        /// </summary>
        /// <value>Организация, за которую отправляется отчетность</value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Код тестовой инспекции, в которую отправляется отчет.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702
        /// </summary>
        /// <value>Код тестовой инспекции, в которую отправляется отчет.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702</value>
        public string IfnsCode { get; set; }

        /// <summary>
        /// Статус документооборота. Подробнее о значении статуса читай в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)  Возможные значения: \&quot;urn:docflow-common-status:sent\&quot;, \&quot;urn:docflow-common-status:response-arrived\&quot;, \&quot;urn:docflow-common-status:response-processed\&quot;, \&quot;urn:docflow-common-status:finished\&quot;
        /// </summary>
        /// <value>Статус документооборота. Подробнее о значении статуса читай в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)  Возможные значения: \&quot;urn:docflow-common-status:sent\&quot;, \&quot;urn:docflow-common-status:response-arrived\&quot;, \&quot;urn:docflow-common-status:response-processed\&quot;, \&quot;urn:docflow-common-status:finished\&quot;</value>
        public string State { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateFnsReportRequest {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  IfnsCode: ").Append(IfnsCode).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}