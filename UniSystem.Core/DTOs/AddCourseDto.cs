using System.ComponentModel.DataAnnotations;

namespace UniSystem.Core.DTOs
{
    public class AddCourseDto
    {
        [Required(ErrorMessage = "name can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "code can not be null")]

        public string Code { get; set; }
        [Required(ErrorMessage = "credit can not be null")]

        public string Credit { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "department can not be null")]
        public int DepartmentId { get; set; }

    }
}
