namespace WorkerService.Models;

public partial class Lesson
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Credit { get; set; }

    public int DepartmentId { get; set; }

    public virtual ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; } = new List<AcademicianCanGiveLesson>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
}
