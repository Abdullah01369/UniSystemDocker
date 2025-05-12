namespace UniSystem.Core.Models
{
    public class AcademicianCanGiveLesson
    {
        public int Id { get; set; }


        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }

        public bool IsActive { get; set; }
    }
}
