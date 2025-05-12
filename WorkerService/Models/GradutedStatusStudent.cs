using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSystem.Core.Models;

namespace WorkerService.Models
{
    public class GradutedStatusStudent
    {
        public int Id { get; set; }
        public bool IsGpaGreatherThanTwo { get; set; }
        public string GPA { get; set; }

        public bool IsPassedAllCourse { get; set; }

        public virtual AspNetUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public bool ISOkeyIntern { get; set; }

        public string CreditDesc { get; set; }
        public bool CreditGreather240 { get; set; }

        public bool AllExamsScoreEntered { get; set; }
    }
}
