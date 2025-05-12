namespace UniSystem.Core.DTOs
{
    public class StudentsExamsListForAcademicianByLesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNo { get; set; }
        public string? Level { get; set; }
        public string LessonName { get; set; }
        public double? MidtermExamScore { get; set; }
        public DateTime? ExamDateDeclareMidterm { get; set; }
        public bool? IsChangeableMidterm { get; set; }
        public bool? IsTakenMidterm { get; set; }
        public double? FinalExamScore { get; set; }
        public DateTime? ExamDateDeclareFinal { get; set; }
        public bool? IsChangeableFinal { get; set; }
        public bool? IsTakenFinal { get; set; }
        public double? ButExamScore { get; set; }
        public DateTime? ExamDateDeclareBut { get; set; }
        public bool? IsChangeableBut { get; set; }
        public bool? CanTakeBut { get; set; }
        public bool? IsTakenBut { get; set; }
        public string? Flag { get; set; }
        public bool? IsConstant { get; set; }
        public bool? IsPassed { get; set; }
        public double? Score { get; set; }
    }
}
