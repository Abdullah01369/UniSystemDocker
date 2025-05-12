namespace UniSystem.Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Credit { get; set; }
        public bool Status { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; }
        public ICollection<StudentLessons> StudentLessons { get; set; }

    }
}
