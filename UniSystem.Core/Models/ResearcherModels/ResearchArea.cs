namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearchArea
    {
        public int Id { get; set; }
        public int ResearcherId { get; set; }
        public Researcher Researcher { get; set; }
        public string Area { get; set; }
    }
}
