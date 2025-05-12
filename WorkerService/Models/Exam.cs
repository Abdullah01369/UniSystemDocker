namespace WorkerService.Models;

public partial class Exam
{
    public int Id { get; set; }

    public string? AppUserId { get; set; }

    public int AcademicianCanGiveLessonId { get; set; }

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

    public int? FlagModelId { get; set; }

    public bool? IsConstant { get; set; }

    public bool? IsPassed { get; set; }

    public double? Score { get; set; }

    public virtual AcademicianCanGiveLesson AcademicianCanGiveLesson { get; set; } = null!;

    public virtual AspNetUser? AppUser { get; set; }

    public virtual FlagModel? FlagModel { get; set; }
}
