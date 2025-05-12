namespace WorkerService.Models;

public partial class StudentDocument
{
    public int Id { get; set; }

    public string? StudentNo { get; set; }

    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpireAt { get; set; }
}
