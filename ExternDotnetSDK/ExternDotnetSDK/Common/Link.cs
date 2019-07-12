// Decompiled with JetBrains decompiler
// Type: Kontur.Api.Link
// Assembly: Kontur.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AA280C05-1D50-4E0C-9593-3BE7096E3298
// Assembly location: C:\Users\trubitsin\Desktop\Not My Projects\ke.api\libapi\Kontur.Api\bin\Release\Kontur.Api.dll

using System;
using System.Text;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Common
{
    public class Link
    {
        [JsonProperty("href")]
        public readonly Uri Href;

        [JsonProperty("rel")]
        public readonly string Rel;

        [JsonProperty("name")]
        public readonly string Name;

        [JsonProperty("title")]
        public readonly string Title;

        [JsonProperty("profile")]
        public readonly string Profile;

        [JsonProperty("templated")]
        public readonly bool Templated;

        public Link(Uri href = default(Uri), string rel = null, string name = null, string title = null, string profile = null, bool templated = false)
        {
            Href = href;
            Rel = rel;
            Name = name;
            Title = title;
            Profile = profile;
            Templated = templated;
        }
    }
}
