using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniSystem.Core.Models;
using UniSystem.Core.Models.ResearcherModels;

namespace UniSystem.Data.ConnectionDB
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AcademicianCanGiveLesson> AcademicianCanGiveLessons { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<AcademicianDepartment> AcademicianDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<StudentDepartment> StudentDepartments { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<StudentLessons> StudentLessons { get; set; }
        public DbSet<FlagModel> FlagModels { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFiles> ProjectFiles { get; set; }
        public DbSet<AcademicianProject> AcademicianProjects { get; set; }
        public DbSet<ProjectStudent> ProjectStudents { get; set; }
        public DbSet<GradutedStatusStudent>  GradutedStatusStudents { get; set; }
        public DbSet<Internship>   Internships { get; set; }





        public DbSet<PublicationMember> PublicationMembers { get; set; }
        public DbSet<ResearchArea> ResearchAreas { get; set; }
        public DbSet<Researcher> Researchers { get; set; }
        public DbSet<ResearcherEducationInformation> ResearcherEducationInformations { get; set; }
        public DbSet<ResearcherExp> ResearcherExps { get; set; }
        public DbSet<ResearcherMetric> ResearcherMetrics { get; set; }
        public DbSet<ResearcherTheses> ResearcherTheses { get; set; }
        public DbSet<ResearcherPublications> ResearcherPublications { get; set; }
        public DbSet<ResearcherContact> ResearcherContacts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }


    }



}
