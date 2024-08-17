namespace Survey_DataTransfer.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SurveyCode { get; set; }
        public bool? IsActivated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ICollection<QuestionDto>? Questions { get; set; }
    }
}