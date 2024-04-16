using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UlInfo
    {
        public UlInfo(string ogrn, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.RequiredJsonPropertyIsMissed(nameof(name));
            
            Ogrn = ogrn;
            Name = name;
        }
        
        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; }
        
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; }
    }
}