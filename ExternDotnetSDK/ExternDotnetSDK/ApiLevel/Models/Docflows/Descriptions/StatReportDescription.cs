using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class StatReportDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string Recipient { get; set; }
        public string Okpo { get; set; }
        public string Okud { get; set; }
        public DateTime PeriodBegin { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int PeriodCode { get; set; }
        public int CorrectionNumber { get; set; }
    }
}