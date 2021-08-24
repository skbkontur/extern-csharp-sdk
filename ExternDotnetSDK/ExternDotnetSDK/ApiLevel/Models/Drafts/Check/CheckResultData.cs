using System;
using System.Collections.Generic;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Check
{
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; }
        public CheckError[] CommonErrors { get; set; }
    }
}