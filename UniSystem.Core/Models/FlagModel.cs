namespace UniSystem.Core.Models
{
    public class FlagModel
    {

        public int Id { get; set; }
        public string Flag { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
