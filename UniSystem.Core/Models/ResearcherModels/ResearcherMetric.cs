namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearcherMetric
    {
        public int Id { get; set; }
        public string Publication { get; set; }
        public string CitationWoS { get; set; }
        public string HIndexWoS { get; set; }
        public string CitationScopus { get; set; }
        public string HIndexScopus { get; set; }
        public string CitationScholar { get; set; }
        public string HIndexScholar { get; set; }
        public string CitiationTrDizin { get; set; }
        public string HIndexTrDizin { get; set; }
        public string CitiationSumOther { get; set; }
        public string Project { get; set; }
        public string ThesisAdvisory { get; set; }
        public string OpenAccess { get; set; }

        public int ResearcherId { get; set; }
        public Researcher Researcher { get; set; }



    }
}
