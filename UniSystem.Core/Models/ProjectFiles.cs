namespace UniSystem.Core.Models
{
    public class ProjectFiles
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
