using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.DTOs
{
    public class SendMailRequestDto
    {

        [Required]
        public string ReceiverMail { get; set; }
        [Required]
        public string SenderMail { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string FileName { get; set; }
        public string? MessageFileTxt { get; set; }
        public bool IsSended { get; set; }
        public bool IsDraft { get; set; }
    }
}
