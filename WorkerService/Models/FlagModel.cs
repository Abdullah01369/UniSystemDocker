namespace WorkerService.Models;

public partial class FlagModel
{
    public int Id { get; set; }

    public string? Flag { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
