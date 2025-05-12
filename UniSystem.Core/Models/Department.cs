namespace UniSystem.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Num { get; set; }
        public string TelNumber { get; set; }


        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }


        public ICollection<AcademicianDepartment> AcademicianDepartments { get; set; }
        public ICollection<StudentDepartment> StudentDepartments { get; set; }
        public ICollection<Lesson> Lessons { get; set; }

    }
}
