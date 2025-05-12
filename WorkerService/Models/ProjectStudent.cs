namespace WorkerService.Models;

public partial class ProjectStudent
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public DateTime? AddingDate { get; set; }

    public bool IsActive { get; set; }

    public int ProjectId { get; set; }

    public virtual AspNetUser? AppUser { get; set; }

    public virtual Project Project { get; set; } = null!;
}
