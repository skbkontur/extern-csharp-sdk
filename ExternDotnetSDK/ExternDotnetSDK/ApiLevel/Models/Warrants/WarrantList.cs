#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantList
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public Warrant?[] Warrants { get; set; }
        public OrganizationWarrantInformation[] OrganizationWarrantInformations { get; set; }
        public Link[] Links { get; set; }
    }
}