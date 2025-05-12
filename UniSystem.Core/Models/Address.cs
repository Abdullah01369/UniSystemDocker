namespace UniSystem.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Declaration { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
