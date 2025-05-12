namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearcherTheses
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Researcher Researcher { get; set; }
        public int ResearcherId { get; set; }
        public string Date { get; set; }
        public string Supporter { get; set; }
        public bool Status { get; set; }
    }
}
