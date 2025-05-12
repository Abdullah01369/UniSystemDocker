namespace WorkerService.Models;

public partial class Faculty
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Num { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
