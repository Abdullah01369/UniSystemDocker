namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearcherPublications
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResearcherId { get; set; }
        public Researcher Researcher { get; set; }
        public string PublishedArea { get; set; }
        public ICollection<PublicationMember> PublicationMembers { get; set; }
    }
}
