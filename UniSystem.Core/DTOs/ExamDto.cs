namespace UniSystem.Core.DTOs
{
    public class ExamDto
    {


        public int AcademicYearId { get; set; }
        public string AcademicYearPeriod { get; set; }
        public string? AcademicYear { get; set; }
        public string LessonName { get; set; }
        public double? MidtermExamScore { get; set; }               // VİZE
        public DateTime? ExamDateDeclareMidterm { get; set; }
        public bool? IsChangeableMidterm { get; set; }
        public bool? IsTakenMidterm { get; set; }
        public double? FinalExamScore { get; set; }                 // FİNAL
        public DateTime? ExamDateDeclareFinal { get; set; }
        public bool? IsChangeableFinal { get; set; }
        public bool? IsTakenFinal { get; set; }
        public double? ButExamScore { get; set; }                   // BÜT
        public DateTime? ExamDateDeclareBut { get; set; }
        public bool? IsChangeableBut { get; set; }
        public bool? CanTakeBut { get; set; }
        public bool? IsTakenBut { get; set; }
        public char? FlagAbc { get; set; }
        public bool? IsConstant { get; set; }
        public bool? IsPassed { get; set; }
        public double? Score { get; set; }

    }
}
