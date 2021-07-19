using System;
using System.Text;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.HttpLevel.Constants;
using Kontur.Extern.Client.HttpLevel.Models;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.HttpLevel.Contents
{
    internal class FormUrlEncodedContent : IHttpContent
    {
        private readonly StringBuilder contentBuilder = new();

        public FormUrlEncodedContent AddEntryIfNotEmpty(in UrlEncodedString name, in UrlEncodedString value)
            => value.IsEmpty ? this : AddEntryCore(name.ToString(), value.ToString());

        public FormUrlEncodedContent AddEntry(in UrlEncodedString name, byte[] value) => 
            AddEntryCore(name.ToString(), Convert.ToBase64String(value));

        public FormUrlEncodedContent AddEntry(in UrlEncodedString name, in UrlEncodedString value)
        {
            AddEntryCore(name.ToString(), value.ToString());
            return this;
        }

        private FormUrlEncodedContent AddEntryCore(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(name));
            if (contentBuilder.Length > 0)
                contentBuilder.Append('&');

            contentBuilder.Append(name).Append('=').Append(value);

            return this;
        }

        Request IHttpContent.Apply(Request request, IJsonSerializer serializer) => 
            request.WithContent(contentBuilder.ToString()).WithContentTypeHeader(ContentTypes.FormUrlEncoded);
    }
}