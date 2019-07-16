using System;

namespace ExternDotnetSDK.Organizations
{
    internal class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}