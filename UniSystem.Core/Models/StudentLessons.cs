namespace UniSystem.Core.Models
{
    public class StudentLessons
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
