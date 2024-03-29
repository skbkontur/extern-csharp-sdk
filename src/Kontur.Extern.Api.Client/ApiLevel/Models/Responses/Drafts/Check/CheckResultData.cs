﻿using System;
using System.Collections.Generic;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check
{
    public class CheckResultData
    {
        public Dictionary<Guid, CheckError[]> DocumentsErrors { get; set; } = null!;
        public CheckError[] CommonErrors { get; set; } = null!;
    }
}