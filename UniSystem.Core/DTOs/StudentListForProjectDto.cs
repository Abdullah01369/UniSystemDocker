namespace UniSystem.Core.DTOs
{
    public class StudentListForProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string No { get; set; }
        public string Email { get; set; }

        public string? PhotoBase64Text { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AddingDate { get; set; }


    }
}
