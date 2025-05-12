namespace UniSystem.Core.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string YearOfEducation { get; set; }
        public string Period { get; set; } // Y, G, B

        public ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; }
    }
}
