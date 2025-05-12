namespace UniSystem.Core.Models
{
    public class ProjectStudent
    {
        public int Id { get; set; }

        public AppUser AppUser { get; set; }
        public DateTime AddingDate { get; set; }
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
