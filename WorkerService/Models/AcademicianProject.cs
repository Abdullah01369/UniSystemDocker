namespace WorkerService.Models;

public partial class AcademicianProject
{
    public int Id { get; set; }

    public int AcademicianId { get; set; }

    public string? AcademicianId1 { get; set; }

    public int ProjectId { get; set; }

    public virtual AspNetUser? AcademicianId1Navigation { get; set; }

    public virtual Project Project { get; set; } = null!;
}
