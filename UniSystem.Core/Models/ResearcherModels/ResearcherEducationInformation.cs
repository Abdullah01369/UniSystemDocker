namespace UniSystem.Core.Models.ResearcherModels
{
    public class ResearcherEducationInformation
    {

        public int Id { get; set; }
        public string Doctorate { get; set; }
        public string DoctorateDate { get; set; }
        public string Postgraduate { get; set; }
        public string PostgraduateDate { get; set; }
        public string Undergraduate { get; set; }
        public string UndergraduateDate { get; set; }

        public string ForegnLang { get; set; }
        public string ForegnLangII { get; set; }

        public Researcher Researcher { get; set; }



    }
}
