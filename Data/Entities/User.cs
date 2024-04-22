using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace otpsystemback.Data.Entities
{
    public class User
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string Email { get; set; } = string.Empty;

        [DataMember]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [DataMember]
        [Required]
        public string Salt { get; set; } = string.Empty;
    }
}
