using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSystem.Core.Models
{
    public class Internship
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsitMandatory { get; set; }
        public string Period { get; set; }
        public string DayCount { get; set; }
        public bool weeklywork  { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public bool SGKStatu { get; set; }
    }
}
