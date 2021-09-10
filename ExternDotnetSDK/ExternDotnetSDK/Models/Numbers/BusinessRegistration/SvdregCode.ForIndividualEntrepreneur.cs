using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Numbers.BusinessRegistration
{
    partial struct SvdregCode
    {
        /// <summary>
        /// Индивидуальный предприниматель
        /// </summary>
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static class ForIndividualEntrepreneur
        {
            /// <summary>
            /// Заявление о государственной регистрации физического лица в качестве индивидуального предпринимателя (по форме Р21001)
            /// </summary>
            public static readonly SvdregCode Code_011011 = "011011";

            /// <summary>
            /// Заявление о государственной регистрации крестьянского (фермерского) хозяйства (по форме Р21002)
            /// </summary>
            public static readonly SvdregCode Code_011012 = "011012";

            /// <summary>
            /// Заявление о внесении изменений в сведения об индивидуальном предпринимателе, содержащиеся в Едином государственном реестре индивидуальных предпринимателей (по форме Р24001)
            /// </summary>
            public static readonly SvdregCode Code_011020 = "011020";

            /// <summary>
            /// 	Заявление о внесении изменений в сведения о главе крестьянского (фермерского) хозяйства, содержащиеся в Едином государственном реестре индивидуальных предпринимателей (по форме Р24002)
            /// </summary>
            public static readonly SvdregCode Code_011030 = "011030";

            /// <summary>
            /// Заявление о государственной регистрации прекращения физическим лицом деятельности в качестве индивидуального предпринимателя в связи с принятием им решения о прекращении данной деятельности (по форме Р26001)
            /// </summary>
            public static readonly SvdregCode Code_011040 = "011040";

            /// <summary>
            /// Заявление о государственной регистрации прекращения крестьянского (фермерского) хозяйства, в связи с решением членов крестьянского (фермерского) хозяйства (по форме Р26002)
            /// </summary>
            public static readonly SvdregCode Code_011051 = "011051";

            /// <summary>
            /// Решение о реорганизации юридического лица
            /// </summary>
            public static readonly SvdregCode Code_021024 = "021024";

            /// <summary>
            /// Копия основного документа, удостоверяющего личность гражданина Российской Федерации ИЛИ
            /// </summary>
            public static readonly SvdregCode Code_022011 = "022011";

            /// <summary>
            /// Копия документа, установленного федеральным законом или признаваемого в соответствии с международным договором Российской Федерации
            /// в качестве документа, удостоверяющего личность иностранного гражданина ИЛИ
            /// </summary>
            public static readonly SvdregCode Code_022012 = "022012";

            /// <summary>
            /// Копия документа, предусмотренного федеральным законом или признаваемого в соответствии с международным договором Российской Федерации
            /// в качестве документа, удостоверяющего личность лица без гражданства
            /// </summary>
            public static readonly SvdregCode Code_022013 = "022013";

            /// <summary>
            ///Решение членов крестьянского (фермерского) хозяйства о его прекращении
            /// </summary>
            public static readonly SvdregCode Code_022063 = "022063";

            /// <summary>
            /// Селфи с документом, удостоверяющим личность гражданина РФ
            /// </summary>
            public static readonly SvdregCode Code_X22011 = "X22011";

            /// <summary>
            /// Селфи с документом, удостоверяющим личность иностранного гражданина
            /// </summary>
            public static readonly SvdregCode Code_X22012 = "X22012";

            /// <summary>
            /// Селфи с документом, удостоверяющим личность лица без гражданства
            /// </summary>
            public static readonly SvdregCode Code_X22013 = "X22013";
        }
    }
}