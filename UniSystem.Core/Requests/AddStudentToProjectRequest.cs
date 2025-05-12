using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.Requests
{
    public class AddStudentToProjectRequest
    {
        [Required]
        public string StudentNo { get; set; }
        [Required]
        public string ProjectId { get; set; }
    }
}
