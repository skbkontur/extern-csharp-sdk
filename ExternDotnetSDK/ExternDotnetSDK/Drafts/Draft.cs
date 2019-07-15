using System;
using System.Collections.Generic;
using System.Text;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Meta;

namespace ExternDotnetSDK.Drafts
{
    public class Draft
    {
        public Guid Id { get; set; }
        public Link[] Docflows { get; set; }
        public Link[] Documents { get; set; }
        public DraftMeta Meta { get; set; }
        public DraftStatus Status { get; set; }
        public Link[] Links { get; set; }
    }
}