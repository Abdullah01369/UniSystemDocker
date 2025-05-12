namespace UniSystem.Core.Models
{
    public class Exam
    {
        public int Id { get; set; }


        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int AcademicianCanGiveLessonId { get; set; }
        public AcademicianCanGiveLesson AcademicianCanGiveLesson { get; set; }

        //public int AcademicYearId { get; set; }
        //public AcademicYear  AcademicYear { get; set; }

        //public int? LessonId { get; set; }
        //public Lesson? Lesson { get; set; }


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

        public int? FlagModelId { get; set; }
        public virtual FlagModel? FlagAbc { get; set; }

        public bool? IsConstant { get; set; }
        public bool? IsPassed { get; set; }
        public double? Score { get; set; }

    }
}
