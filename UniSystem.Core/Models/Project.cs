namespace UniSystem.Core.Models
{
    public class Project
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; } // Academician
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool IsPublish { get; set; }
        public bool IsComplate { get; set; }
        public string Statu { get; set; }
        public bool IsActive { get; set; }
        public DateTime CrearedDate { get; set; }
        public ICollection<ProjectFiles> ProjectFiles { get; set; }
        public ICollection<ProjectStudent> ProjectStudents { get; set; }
    }
}
