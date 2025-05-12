using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSystem.Core.DTOs
{
    public class GradutedDto
    {
        public bool IsGpaGreatherThanTwo { get; set; }
        public string GPA { get; set; }

        public bool IsPassedAllCourse { get; set; }
        public string IsPassedAllCourseDesc { get; set; }

        public bool ISOkeyIntern { get; set; }
        public string ISOkeyInternDesc { get; set; }

        public string CreditDesc { get; set; }
        public bool CreditGreather240 { get; set; }

        public bool AllExamsScoreEntered { get; set; }
        public string AllExamsScoreEnteredDec { get; set; }
    }
}
