using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.Requests
{
    public class SavingFinalRequestModel
    {
        [Required]
        public string ExamId { get; set; }
        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string FinalScore { get; set; }
    }
}
