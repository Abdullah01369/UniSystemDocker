namespace UniSystem.Core.Models
{
    public class AcademicianProject
    {
        public int Id { get; set; }
        public int AcademicianId { get; set; }
        public AppUser Academician { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
