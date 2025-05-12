namespace UniSystem.Core.Models.ResearcherModels
{
    public class PublicationMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Universty { get; set; }
        public ResearcherPublications ResearcherPublication { get; set; }
        public int ResearcherPublicationsId { get; set; }
    }
}
