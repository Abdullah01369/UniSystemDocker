namespace WorkerService.Models;

public partial class StudentLesson
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public int LessonId { get; set; }

    public virtual AspNetUser? AppUser { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;
}
