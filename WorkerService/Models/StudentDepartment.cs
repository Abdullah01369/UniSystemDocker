namespace WorkerService.Models;

public partial class StudentDepartment
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public int DepartmentId { get; set; }

    public virtual AspNetUser? AppUser { get; set; }

    public virtual Department Department { get; set; } = null!;
}
