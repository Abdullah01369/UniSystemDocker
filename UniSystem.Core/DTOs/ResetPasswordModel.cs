namespace UniSystem.Core.DTOs
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
    }
}
