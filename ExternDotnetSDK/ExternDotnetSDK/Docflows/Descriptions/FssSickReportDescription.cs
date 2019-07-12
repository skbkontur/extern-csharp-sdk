using System;
using ExternDotnetSDK.Docflows.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Docflows.Descriptions
{
    [JsonObject]
    public class FssSickReportDescription : DocflowDescription
    {
        public FormVersion FormVersion { get; set; }
        public string RegistrationNumber { get; set; }
        public string FssId { get; set; }
        public string FssStageDescription { get; set; }
        public string FssStageErrorCode { get; set; }
        public string FssStageErrorExtend { get; set; }
        public string FssStageType { get; set; }
        public string FssStageStatus { get; set; }
        public DateTime? FssStageDate { get; set; }
    }
}