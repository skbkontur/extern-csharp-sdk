using System;
using System.IO;
using System.Text;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации входящего письма
    /// </summary>
    public class GenerateCuLetterRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCuLetterRequest" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи, на которую будет направлено письмо (required).</param>
        /// <param name="sender">Организация, которая будет отвечать на письмо в налоговый орган (required).</param>
        /// <param name="payer">Организация, на которую приходит письмо и за которую будет отправляться отчетность (required).</param>
        /// <param name="text">Текст письма (required).</param>
        /// <param name="ifnsCode">Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702.</param>
        public GenerateCuLetterRequest(Guid? accountId = default, Sender sender = default, Payer payer = default, string text = default, string ifnsCode = default)
        {
            if (accountId == null)
            {
                throw new InvalidDataException("accountId is a required property for GenerateCuLetterRequest and cannot be null");
            }

            AccountId = accountId;
            Sender = sender ?? throw new InvalidDataException("sender is a required property for GenerateCuLetterRequest and cannot be null");
            Payer = payer ?? throw new InvalidDataException("payer is a required property for GenerateCuLetterRequest and cannot be null");
            Text = text ?? throw new InvalidDataException("text is a required property for GenerateCuLetterRequest and cannot be null");
            IfnsCode = ifnsCode;
        }

        /// <summary>
        /// Id учетной записи, на которую будет направлено письмо
        /// </summary>
        /// <value>Id учетной записи, на которую будет направлено письмо</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Организация, которая будет отвечать на письмо в налоговый орган
        /// </summary>
        /// <value>Организация, которая будет отвечать на письмо в налоговый орган</value>
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, на которую приходит письмо и за которую будет отправляться отчетность
        /// </summary>
        /// <value>Организация, на которую приходит письмо и за которую будет отправляться отчетность</value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Текст письма
        /// </summary>
        /// <value>Текст письма</value>
        public string Text { get; set; }

        /// <summary>
        /// Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702
        /// </summary>
        /// <value>Код тестовой инспекции, от которой придет требование.  Возможные значения: 0007, 0008, 0084, 0085, 0087, 0088, 0093, 0094, 0096, 9979, 7702</value>
        public string IfnsCode { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateCuLetterRequest {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  IfnsCode: ").Append(IfnsCode).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}