using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации требования
    /// </summary>
    [UsedImplicitly]
    public class GenerateDemandRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateDemandRequest" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи, на которую придет требование (required).</param>
        /// <param name="sender">Организация, которая будет отвечать на требование в налоговый орган (required).</param>
        /// <param name="payer">Организация, на которую приходит письмо и за которую будет отправляться отчетность (required).</param>
        /// <param name="knds">Перечисление кодов налоговой декларации требований, которые будут содержаться в документообороте (required).</param>
        /// <param name="ifnsCode">Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702.</param>
        public GenerateDemandRequest(Guid? accountId = default, Sender sender = default, Payer payer = default, List<string> knds = default, string ifnsCode = default)
        {
            if (accountId == null)
            {
                throw new InvalidDataException("accountId is a required property for GenerateDemandRequest and cannot be null");
            }

            AccountId = accountId;
            Sender = sender ?? throw new InvalidDataException("sender is a required property for GenerateDemandRequest and cannot be null");
            Payer = payer ?? throw new InvalidDataException("payer is a required property for GenerateDemandRequest and cannot be null");
            Knds = knds ?? throw new InvalidDataException("knds is a required property for GenerateDemandRequest and cannot be null");
            IfnsCode = ifnsCode;
        }

        /// <summary>
        /// Id учетной записи, на которую придет требование
        /// </summary>
        /// <value>Id учетной записи, на которую придет требование</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Организация, которая будет отвечать на требование в налоговый орган
        /// </summary>
        /// <value>Организация, которая будет отвечать на требование в налоговый орган</value>
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, на которую приходит письмо и за которую будет отправляться отчетность
        /// </summary>
        /// <value>Организация, на которую приходит письмо и за которую будет отправляться отчетность</value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Перечисление кодов налоговой декларации требований, которые будут содержаться в документообороте
        /// </summary>
        /// <value>Перечисление кодов налоговой декларации требований, которые будут содержаться в документообороте</value>
        public List<string> Knds { get; set; }

        /// <summary>
        /// Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702
        /// </summary>
        /// <value>Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702</value>
        public string IfnsCode { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateDemandRequest {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  Knds: ").Append(Knds).Append("\n");
            sb.Append("  IfnsCode: ").Append(IfnsCode).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}