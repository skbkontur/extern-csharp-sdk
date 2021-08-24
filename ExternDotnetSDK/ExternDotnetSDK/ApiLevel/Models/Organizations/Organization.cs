using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    public class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}