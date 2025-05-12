using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.DTOs
{
    public class ResetPasswordFinalModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
