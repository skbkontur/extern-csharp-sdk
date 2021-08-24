using System;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Events
{
    public class ApiEvent
    {
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public Urn DocflowType { get; set; }
        public Link DocflowLink { get; set; }
        public Urn NewState { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Id { get; set; }
    }
}