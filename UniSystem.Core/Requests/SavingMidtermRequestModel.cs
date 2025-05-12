using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.Requests
{
    public class SavingMidtermRequestModel
    {
        [Required]
        public string ExamId { get; set; }
        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string MidtermScore { get; set; }


    }
}
