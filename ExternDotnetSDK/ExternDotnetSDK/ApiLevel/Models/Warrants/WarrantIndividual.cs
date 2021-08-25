#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantIndividual
    {
        public Fio? Fio { get; set; }
        public string? Inn { get; set; }
        public IdentityCard? Document { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Ogrnip { get; set; }
        public string? Citizenship { get; set; }
        public string? Address { get; set; }
    }
}