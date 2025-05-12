namespace UniSystem.Core.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddingDate { get; set; }
        public bool IsActive { get; set; }
    }
}
