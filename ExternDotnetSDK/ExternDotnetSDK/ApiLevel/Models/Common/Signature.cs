using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public class Signature
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SignatureCertificateThumbprint { get; set; }
        public Link ContentLink { get; set; }
        public Link[] Links { get; set; }
    }
}