namespace WorkerService.Models;

public partial class ProjectFile
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string? Name { get; set; }

    public string? Extention { get; set; }

    public string? Path { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Project Project { get; set; } = null!;
}
