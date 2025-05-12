namespace UniSystem.Core.DTOs
{
    public class InBoxListDto
    {
        public int Id { get; set; }
        public string ReceiverMail { get; set; }
        public string SenderMail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string? FileName { get; set; }
        public bool IsDraft { get; set; }


        public bool IsDeleted { get; set; }


    }
}
