namespace UniSystem.Core.Models
{
    public class Faculty
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Num { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
