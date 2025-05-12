namespace WorkerService.Models;

public partial class AcademicianCanGiveLesson
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public int LessonId { get; set; }

    public int AcademicYearId { get; set; }

    public bool IsActive { get; set; }

    public virtual AcademicYear AcademicYear { get; set; } = null!;

    public virtual AspNetUser? AppUser { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Lesson Lesson { get; set; } = null!;
}
