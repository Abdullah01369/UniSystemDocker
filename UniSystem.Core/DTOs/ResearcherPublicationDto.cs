namespace UniSystem.Core.DTOs
{
    public class ResearcherPublicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishedArea { get; set; }
        public ICollection<PublicationMemberDto> PublicationMembers { get; set; }
    }
}
