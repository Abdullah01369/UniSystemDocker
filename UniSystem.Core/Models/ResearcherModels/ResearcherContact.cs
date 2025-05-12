namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearcherContact
    {
        public int Id { get; set; }
        public Researcher Researcher { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
    }
}
