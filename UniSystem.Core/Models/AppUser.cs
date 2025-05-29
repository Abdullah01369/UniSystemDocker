using Microsoft.AspNetCore.Identity;

namespace UniSystem.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string No { get; set; }
        public string TC { get; set; } // kontrol ettirt
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsActive { get; set; }
        public string PhotoBase64Text { get; set; }
        public Gender? Gender { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public bool IsStudent { get; set; }
        public bool IsGradudated { get; set; }
        public double? GPA { get; set; }
        public DateTime BeginningDate { get; set; }
        public string SpecialEmail { get; set; }
        public ICollection<AcademicianDepartment> AcademicianDepartments { get; set; }
        public ICollection<StudentDepartment> StudentDepartments { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; }
        public ICollection<StudentLessons> StudentLessons { get; set; }
        public ICollection<ProjectStudent> ProjectStudents { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<GradutedStatusStudent> GradutedStatusStudents { get; set; }
        public ICollection<Internship>  Internships { get; set; }


    }
}
