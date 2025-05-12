namespace WorkerService.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Num { get; set; }

    public string? TelNumber { get; set; }

    public int? FacultyId { get; set; }

    public virtual ICollection<AcademicianDepartment> AcademicianDepartments { get; set; } = new List<AcademicianDepartment>();

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<StudentDepartment> StudentDepartments { get; set; } = new List<StudentDepartment>();
}
