namespace WorkerService.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Declaration { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();
}
