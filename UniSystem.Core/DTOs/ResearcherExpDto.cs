using UniSystem.Core.Models.ResearcherModels;

namespace UniSystem.Core.DTOs
{
    public class ResearcherExpDto
    {
        public int Id { get; set; }
        public int ResearcherId { get; set; }
        public Researcher Researcher { get; set; }
        public string Name { get; set; }
        public string DateBeg { get; set; }
        public string DateEnd { get; set; }
        public string Universty { get; set; }
        public string Duty { get; set; }
    }
}
