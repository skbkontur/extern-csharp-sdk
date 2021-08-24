using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantIndividual
    {
        [CanBeNull]
        public Fio Fio { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public IdentityCard Document { get; set; }

        [CanBeNull]
        public DateTime? BirthDate { get; set; }

        [CanBeNull]
        public string Ogrnip { get; set; }

        [CanBeNull]
        public string Citizenship { get; set; }

        [CanBeNull]
        public string Address { get; set; }
    }
}