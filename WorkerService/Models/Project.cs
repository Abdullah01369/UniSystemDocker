namespace WorkerService.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public string? Name { get; set; }

    public string? Subject { get; set; }

    public bool? IsPublish { get; set; }

    public bool? IsComplate { get; set; }

    public string? Statu { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CrearedDate { get; set; }

    public virtual ICollection<AcademicianProject> AcademicianProjects { get; set; } = new List<AcademicianProject>();

    public virtual AspNetUser? AppUser { get; set; }

    public virtual ICollection<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();

    public virtual ICollection<ProjectStudent> ProjectStudents { get; set; } = new List<ProjectStudent>();
}
