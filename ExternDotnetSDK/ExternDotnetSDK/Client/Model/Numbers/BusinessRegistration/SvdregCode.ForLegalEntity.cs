using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Numbers.BusinessRegistration
{
    partial struct SvdregCode
    {
        /// <summary>
        /// Юридическое лицо
        /// </summary>
        [PublicAPI]
        [SuppressMessage("ReSharper", "CommentTypo")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static class ForLegalOrg
        {
            /// <summary>
            /// Заявление о государственной регистрации юридического лица (по форме Р11001)
            /// </summary>
            public static readonly SvdregCode Code_010011 = "010011";

            /// <summary>
            /// Заявление о государственной регистрации юридического лица, создаваемого путем реорганизации  (по форме Р12001)
            /// </summary>
            public static readonly SvdregCode Code_010020 = "010020";

            /// <summary>
            /// Заявление о государственной регистрации в связи с завершением реорганизации юридического лица (юридических лиц) (по форме Р12016)
            /// </summary>
            public static readonly SvdregCode Code_010028 = "010028";

            /// <summary>
            /// Заявление о государственной регистрации изменений, вносимых в учредительные документы юридического лица (по форме Р13001)
            /// </summary>
            public static readonly SvdregCode Code_010031 = "010031";

            /// <summary>
            /// Заявление о государственной регистрации изменений, внесенных в учредительный документ юридического лица, и (или) о внесении изменений в сведения о юридическом лице,
            /// содержащиеся в Едином государственном реестре юридических лиц (по форме Р13014)
            /// </summary>
            public static readonly SvdregCode Code_010030 = "010030";

            /// <summary>
            /// Заявление о внесении в Единый государственный реестр юридических лиц изменений в сведения о юридическом лице, не связанных с внесением изменений в учредительные документы (по форме Р14001)
            /// </summary>
            public static readonly SvdregCode Code_010040 = "010040";

            /// <summary>
            /// Заявление о внесении в Единый государственный реестр юридических лиц сведений о том, что хозяйственное общество находится в процессе уменьшения уставного капитала (по форме Р14002)
            /// </summary>
            public static readonly SvdregCode Code_010044 = "010044";

            /// <summary>
            /// Уведомление о ликвидации юридического лица (по форме Р15001)
            /// </summary>
            public static readonly SvdregCode Code_010051 = "010051";

            /// <summary>
            /// Заявление (уведомление) о ликвидации юридического лица (по форме Р15016)
            /// </summary>
            public static readonly SvdregCode Code_010050 = "010050";

            /// <summary>
            /// Заявление о государственной регистрации юридического лица в связи с его ликвидацией (по форме Р16001)
            /// </summary>
            public static readonly SvdregCode Code_010060 = "010060";

            /// <summary>
            /// Заявление о государственной регистрации прекращения унитарного предприятия в связи с продажей его имущественного комплекса (по форме Р16002)
            /// </summary>
            public static readonly SvdregCode Code_010071 = "010071";

            /// <summary>
            /// Заявление о внесении записи о прекращении деятельности присоединенного юридического лица (по форме Р16003)
            /// </summary>
            public static readonly SvdregCode Code_010083 = "010083";

            /// <summary>
            /// Заявление - уведомление о начале процедуры реорганизации (по форме Р12003)
            /// </summary>
            public static readonly SvdregCode Code_010120 = "010120";

            /// <summary>
            /// Заявление (уведомление) о государственной регистрации международной компании, международного фонда (по форме Р18002)
            /// </summary>
            public static readonly SvdregCode Code_010094 = "010094";

            /// <summary>
            /// Устав юридического лица
            /// </summary>
            public static readonly SvdregCode Code_021001 = "021001";

            /// <summary>
            /// Положение об организации
            /// </summary>
            public static readonly SvdregCode Code_021003 = "021003";

            /// <summary>
            /// Решение о создании юридического лица
            /// </summary>
            public static readonly SvdregCode Code_021023 = "021023";

            /// <summary>
            /// Решение съезда
            /// </summary>
            public static readonly SvdregCode Code_021027 = "021027";

            /// <summary>
            /// Решение конференции
            /// </summary>
            public static readonly SvdregCode Code_021028 = "021028";

            /// <summary>
            /// Решение общего собрания
            /// </summary>
            public static readonly SvdregCode Code_021029 = "021029";

            /// <summary>
            /// Изменения к уставу юридического лица
            /// </summary>
            public static readonly SvdregCode Code_021004 = "021004";

            /// <summary>
            /// Устав юридического лица в новой редакции
            /// </summary>
            public static readonly SvdregCode Code_021005 = "021005";

            /// <summary>
            /// Учредительный документ юридического лица 
            /// </summary>
            public static readonly SvdregCode Code_021000 = "021000";

            /// <summary>
            /// Документ, подтверждающий государственную регистрацию (создание) иностранного юридического лица 
            /// </summary>
            public static readonly SvdregCode Code_021093 = "021093";

            /// <summary>
            /// Решение высшего органа управления или иного уполномоченного органа иностранного юридического лица об изменении его личного закона и утверждении устава международной компании 
            /// </summary>
            public static readonly SvdregCode Code_021058 = "021058";
        }
    }
}