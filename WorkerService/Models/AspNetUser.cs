namespace WorkerService.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? No { get; set; }

    public string? Tc { get; set; }

    public int? CityId { get; set; }

    public DateTime Birthdate { get; set; }

    public bool IsActive { get; set; }

    public string? PhotoBase64Text { get; set; }

    public byte? Gender { get; set; }

    public int? AddressId { get; set; }

    public bool IsStudent { get; set; }

    public bool IsGradudated { get; set; }

    public DateTime BeginningDate { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? SpecialEmail { get; set; }

    public double? Gpa { get; set; }

    public virtual ICollection<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; } = new List<AcademicianCanGiveLesson>();

    public virtual ICollection<AcademicianDepartment> AcademicianDepartments { get; set; } = new List<AcademicianDepartment>();

    public virtual ICollection<AcademicianProject> AcademicianProjects { get; set; } = new List<AcademicianProject>();

    public virtual Address? Address { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual City? City { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<ProjectStudent> ProjectStudents { get; set; } = new List<ProjectStudent>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<StudentDepartment> StudentDepartments { get; set; } = new List<StudentDepartment>();

    public virtual ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
