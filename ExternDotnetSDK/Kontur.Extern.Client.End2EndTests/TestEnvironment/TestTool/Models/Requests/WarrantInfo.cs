using System;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests
{
    /// <summary>
    /// Сведения о доверенности
    /// </summary>
    public class WarrantInfo
    {
        private readonly DateTime? dateBegin;

        public WarrantInfo(string? number = null, DateTime? dateBegin = null)
        {
            this.dateBegin = dateBegin;
            Number = number;
        }

        public string? Number { get; }

        /// <summary>
        /// Дата начала действия. Дата в формате ДД.ММ.ГГГГ
        /// </summary>
        /// <value>Дата начала действия. Дата в формате ДД.ММ.ГГГГ</value>
        public string? DateBegin => dateBegin?.ToString("dd.MM.yyyy");
    }
}