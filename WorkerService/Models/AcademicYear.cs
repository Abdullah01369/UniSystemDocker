namespace WorkerService.Models;

public partial class AcademicYear
{
    public int Id { get; set; }

    public string? YearOfEducation { get; set; }

    public string? Period { get; set; }

    public virtual ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; } = new List<AcademicianCanGiveLesson>();
}
