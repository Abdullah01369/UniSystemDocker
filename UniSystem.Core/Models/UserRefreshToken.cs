using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.Models
{
    public class UserRefreshToken
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
