using Survey_Managment.Actions;

namespace Survey_Managment.ViewModel
{
    public class SurveyModel : SurveyActions
    {
        public string Title { get; set; }
        public string SurveyCode { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateFrom { get; set; } = DateTime.Now;
        public DateTime DateTo { get; set; } = DateTime.Now.AddDays(3);
        public bool IsActivated { get; set; }
    }
}
