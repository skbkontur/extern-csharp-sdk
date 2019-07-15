using System;

namespace ExternDotnetSDK.Organizations
{
    class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}