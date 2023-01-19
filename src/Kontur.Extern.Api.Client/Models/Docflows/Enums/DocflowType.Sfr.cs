namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    public partial struct DocflowType
    {
        /// <summary>
        /// Тип документооборота: Отчет в СФР, отчетность по форме ЕФС-1
        /// Основной документооборот по обмену электронными документами Абонентов с СФР
        /// </summary>
        public static readonly DocflowType SfrReport = "urn:docflow:sfr-report";
    }
}