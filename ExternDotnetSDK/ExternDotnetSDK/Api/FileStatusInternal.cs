using System;
using ExternDotnetSDK.Api.Enums;
using ExternDotnetSDK.Api.Statuses;

namespace ExternDotnetSDK.Api
{
    public class FileStatusInternal
    {
        public Guid FileId { get; set; }
        public FileTaskStatusInternal Status { get; set; }
        public Guid? ErrorId { get; set; }
        public ErrorCodeInternal ErrorCode { get; set; }
        public long ResultSize { get; set; }
        public string ResultMD5 { get; set; }
        public Guid? ResultId { get; set; }
    }
}