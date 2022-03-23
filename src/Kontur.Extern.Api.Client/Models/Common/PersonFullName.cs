using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PersonFullName
    {
        public PersonFullName(string surname, string name, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(surname));
            
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(name));
            
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        [JsonPropertyName("surname")]
        public string Surname { get; }

        /// <summary>
        /// Имя
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        [JsonPropertyName("patronymic")]
        public string Patronymic { get; }

        public void Deconstruct(out string surname, out string name, out string patronymic)
        {
            surname = Surname;
            name = Name;
            patronymic = Patronymic;
        }
    }
}