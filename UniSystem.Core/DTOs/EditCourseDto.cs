namespace UniSystem.Core.DTOs
{
    public class EditCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Credit { get; set; }
        public bool Status { get; set; }
        public int DepartmentId { get; set; }
    }
}
