namespace UniSystem.Core.Models
{
    public class StudentDepartment
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
