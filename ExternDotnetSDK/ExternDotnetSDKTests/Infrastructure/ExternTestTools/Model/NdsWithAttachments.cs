using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Фуф НДС с приложениями - книгой покупок и книгой продаж
    /// </summary>
    [DataContract]
    public class NdsWithAttachments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NdsWithAttachments" /> class.
        /// </summary>
        /// <param name="mainDocument">Главный документ.</param>
        /// <param name="fnsBookPurchases">Книга покупок.</param>
        /// <param name="fnsBookSales">Книга продаж.</param>
        public NdsWithAttachments(string mainDocument = default, string fnsBookPurchases = default, string fnsBookSales = default)
        {
            MainDocument = mainDocument;
            FnsBookPurchases = fnsBookPurchases;
            FnsBookSales = fnsBookSales;
        }

        /// <summary>
        /// Главный документ
        /// </summary>
        /// <value>Главный документ</value>
        [DataMember(Name = "mainDocument", EmitDefaultValue = false)]
        public string MainDocument { get; set; }

        /// <summary>
        /// Книга покупок
        /// </summary>
        /// <value>Книга покупок</value>
        [DataMember(Name = "fnsBookPurchases", EmitDefaultValue = false)]
        public string FnsBookPurchases { get; set; }

        /// <summary>
        /// Книга продаж
        /// </summary>
        /// <value>Книга продаж</value>
        [DataMember(Name = "fnsBookSales", EmitDefaultValue = false)]
        public string FnsBookSales { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NdsWithAttachments {\n");
            sb.Append("  MainDocument: ").Append(MainDocument).Append("\n");
            sb.Append("  FnsBookPurchases: ").Append(FnsBookPurchases).Append("\n");
            sb.Append("  FnsBookSales: ").Append(FnsBookSales).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}