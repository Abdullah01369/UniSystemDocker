using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.Requests
{
    public class SavingButRequestModel
    {
        [Required]
        public string ExamId { get; set; }
        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string ButScore { get; set; }
    }
}
