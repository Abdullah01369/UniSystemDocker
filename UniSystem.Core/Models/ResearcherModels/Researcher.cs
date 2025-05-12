namespace UniSystem.Core.Models.ResearcherModels
{
    public class Researcher
    {
        public int Id { get; set; }
        public string InstitutionalInformation { get; set; }
        public string WoSResearchAreas { get; set; }
        public string SpecialResearchAreas { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public int? ResearcherEducationInformationId { get; set; }
        public ResearcherEducationInformation ResearcherEducationInformation { get; set; }


        public ICollection<ResearchArea> ResearchAreas { get; set; }
        public ICollection<ResearcherContact> ResearcherContacts { get; set; }
        public ICollection<ResearcherExp> ResearcherExps { get; set; }
        public ICollection<ResearcherTheses> ResearcherTheses { get; set; }
    }
}
