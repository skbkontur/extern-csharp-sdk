using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    public class UlInfo
    {
        /// <summary>
        /// ОГРН
        /// </summary>
        [Required]
        [DataMember]
        public string Ogrn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}