namespace WorkerService.Models;

public partial class UserRefreshToken
{
    public string Id { get; set; } = null!;

    public string? Code { get; set; }

    public DateTime Expiration { get; set; }
}
