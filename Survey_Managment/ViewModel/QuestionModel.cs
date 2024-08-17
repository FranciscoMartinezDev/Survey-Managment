using Survey_Managment.Actions;

namespace Survey_Managment.ViewModel
{
    public class QuestionModel : QuestionActions
    {
        public int QuestionId { get; set; }
        public string? Question { get; set; }
        public string? Type { get; set; }
    }
}
