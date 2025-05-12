using Microsoft.EntityFrameworkCore;

namespace WorkerService.Models;

public partial class Unisystem3Context : DbContext
{
    public Unisystem3Context()
    {
    }

    public Unisystem3Context(DbContextOptions<Unisystem3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }
    public virtual DbSet<GradutedStatusStudent>  GradutedStatusStudents { get; set; }

    public virtual DbSet<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; }

    public virtual DbSet<AcademicianDepartment> AcademicianDepartments { get; set; }

    public virtual DbSet<AcademicianProject> AcademicianProjects { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FlagModel> FlagModels { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectFile> ProjectFiles { get; set; }

    public virtual DbSet<ProjectStudent> ProjectStudents { get; set; }

    public virtual DbSet<StudentDepartment> StudentDepartments { get; set; }

    public virtual DbSet<StudentDocument> StudentDocuments { get; set; }

    public virtual DbSet<StudentLesson> StudentLessons { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AGTR9NE\\SQLEXPRESS;Initial Catalog=unisystem3;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicianCanGiveLesson>(entity =>
        {
            entity.HasIndex(e => e.AcademicYearId, "IX_AcademicianCanGiveLessons_AcademicYearId");

            entity.HasIndex(e => e.AppUserId, "IX_AcademicianCanGiveLessons_AppUserId");

            entity.HasIndex(e => e.LessonId, "IX_AcademicianCanGiveLessons_LessonId");

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.AcademicianCanGiveLessons).HasForeignKey(d => d.AcademicYearId);

            entity.HasOne(d => d.AppUser).WithMany(p => p.AcademicianCanGiveLessons).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Lesson).WithMany(p => p.AcademicianCanGiveLessons).HasForeignKey(d => d.LessonId);
        });

        modelBuilder.Entity<AcademicianDepartment>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_AcademicianDepartments_AppUserId");

            entity.HasIndex(e => e.DepartmentId, "IX_AcademicianDepartments_DepartmentId");

            entity.HasOne(d => d.AppUser).WithMany(p => p.AcademicianDepartments).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Department).WithMany(p => p.AcademicianDepartments).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<AcademicianProject>(entity =>
        {
            entity.HasIndex(e => e.AcademicianId1, "IX_AcademicianProjects_AcademicianId1");

            entity.HasIndex(e => e.ProjectId, "IX_AcademicianProjects_ProjectId");

            entity.HasOne(d => d.AcademicianId1Navigation).WithMany(p => p.AcademicianProjects).HasForeignKey(d => d.AcademicianId1);

            entity.HasOne(d => d.Project).WithMany(p => p.AcademicianProjects).HasForeignKey(d => d.ProjectId);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.AddressId, "IX_AspNetUsers_AddressId");

            entity.HasIndex(e => e.CityId, "IX_AspNetUsers_CityId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Gpa).HasColumnName("GPA");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Tc).HasColumnName("TC");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Address).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.AddressId);

            entity.HasOne(d => d.City).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.CityId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.FacultyId, "IX_Departments_FacultyId");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Departments).HasForeignKey(d => d.FacultyId);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_UpdateFinalScoreAndCalculateAverage"));

            entity.HasIndex(e => e.AcademicianCanGiveLessonId, "IX_Exams_AcademicianCanGiveLessonId");

            entity.HasIndex(e => e.AppUserId, "IX_Exams_AppUserId");

            entity.HasIndex(e => e.FlagModelId, "IX_Exams_FlagModelId");

            entity.HasOne(d => d.AcademicianCanGiveLesson).WithMany(p => p.Exams).HasForeignKey(d => d.AcademicianCanGiveLessonId);

            entity.HasOne(d => d.AppUser).WithMany(p => p.Exams).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.FlagModel).WithMany(p => p.Exams).HasForeignKey(d => d.FlagModelId);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Lessons_DepartmentId");

            entity.HasOne(d => d.Department).WithMany(p => p.Lessons).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_Projects_AppUserId");

            entity.Property(e => e.IsComplate)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.IsPublish)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.AppUser).WithMany(p => p.Projects).HasForeignKey(d => d.AppUserId);
        });

        modelBuilder.Entity<ProjectFile>(entity =>
        {
            entity.HasIndex(e => e.ProjectId, "IX_ProjectFiles_ProjectId");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectFiles).HasForeignKey(d => d.ProjectId);
        });

        modelBuilder.Entity<ProjectStudent>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_ProjectStudents_AppUserId");

            entity.HasIndex(e => e.ProjectId, "IX_ProjectStudents_ProjectId");

            entity.HasOne(d => d.AppUser).WithMany(p => p.ProjectStudents).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectStudents).HasForeignKey(d => d.ProjectId);
        });

        modelBuilder.Entity<StudentDepartment>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_StudentDepartments_AppUserId");

            entity.HasIndex(e => e.DepartmentId, "IX_StudentDepartments_DepartmentId");

            entity.HasOne(d => d.AppUser).WithMany(p => p.StudentDepartments).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Department).WithMany(p => p.StudentDepartments).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<StudentDocument>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ExpireAt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
        });

        modelBuilder.Entity<StudentLesson>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_StudentLessons_AppUserId");

            entity.HasIndex(e => e.LessonId, "IX_StudentLessons_LessonId");

            entity.HasOne(d => d.AppUser).WithMany(p => p.StudentLessons).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Lesson).WithMany(p => p.StudentLessons).HasForeignKey(d => d.LessonId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
