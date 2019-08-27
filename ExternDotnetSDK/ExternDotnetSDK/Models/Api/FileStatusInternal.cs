using System;
using KeApiOpenSdk.Models.Api.Enums;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Api
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FileStatusInternal
    {
        public Guid FileId { get; set; }
        public FileTaskStatusInternal Status { get; set; }
        public Guid? ErrorId { get; set; }
        public ErrorCodeInternal ErrorCode { get; set; }
        public long ResultSize { get; set; }
        public string ResultMd5 { get; set; }
        public Guid? ResultId { get; set; }
    }
}