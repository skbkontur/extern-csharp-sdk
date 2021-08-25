using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации набора документооборотов из налогового органа
    /// </summary>
    [UsedImplicitly]
    public class GenerateDocflowsSuiteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateDocflowsSuiteRequest" /> class.
        /// </summary>
        /// <param name="accountId">Id учетной записи, которая получит документооборот (required).</param>
        /// <param name="sender">Организация, которая будет отвечать на письмо и требование в налоговый орган (required).</param>
        /// <param name="payer">Организация, на которую приходит письмо и требование и за которую будет отправляться отчетность (required).</param>
        /// <param name="docflowCount">Количество входящих документооборотов (required).</param>
        public GenerateDocflowsSuiteRequest(Guid? accountId = default, Sender sender = default, Payer payer = default, int? docflowCount = default)
        {
            if (accountId == null)
            {
                throw new InvalidDataException("accountId is a required property for GenerateDocflowsSuiteRequest and cannot be null");
            }

            AccountId = accountId;

            Sender = sender ?? throw new InvalidDataException("sender is a required property for GenerateDocflowsSuiteRequest and cannot be null");
            Payer = payer ?? throw new InvalidDataException("payer is a required property for GenerateDocflowsSuiteRequest and cannot be null");

            if (docflowCount == null)
            {
                throw new InvalidDataException("docflowCount is a required property for GenerateDocflowsSuiteRequest and cannot be null");
            }

            DocflowCount = docflowCount;
        }

        /// <summary>
        /// Id учетной записи, которая получит документооборот
        /// </summary>
        /// <value>Id учетной записи, которая получит документооборот</value>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Организация, которая будет отвечать на письмо и требование в налоговый орган
        /// </summary>
        /// <value>Организация, которая будет отвечать на письмо и требование в налоговый орган</value>
        public Sender Sender { get; set; }

        /// <summary>
        /// Организация, на которую приходит письмо и требование и за которую будет отправляться отчетность
        /// </summary>
        /// <value>Организация, на которую приходит письмо и требование и за которую будет отправляться отчетность</value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Количество входящих документооборотов
        /// </summary>
        /// <value>Количество входящих документооборотов</value>
        public int? DocflowCount { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateDocflowsSuiteRequest {\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  DocflowCount: ").Append(DocflowCount).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}