namespace UniSystem.Core.Requests
{
    public class LessonListByStudentAndDateRequest
    {
        public string Mail { get; set; }
        public string AcademicPeriodId { get; set; }
        public string AcademicYear { get; set; }
    }
}
