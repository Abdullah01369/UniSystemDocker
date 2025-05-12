namespace UniSystem.Core.Requests
{
    public class SendMultiMailModel
    {
        public string AcademicianMail { get; set; }
        public int LessonId { get; set; }
        public string messagearea { get; set; }
        public string subject { get; set; }
        public string FileName { get; set; }
        public string MessageFileTxt { get; set; }
    }
}
