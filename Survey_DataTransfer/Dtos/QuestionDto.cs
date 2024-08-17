using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_DataTransfer.Dtos
{
    public class QuestionDto
    {
        public int IdSurveyQuestion { get; set; }
        public string? Question { get; set; }
        public string? Type { get; set; }
        public ICollection<OptionDto>? Options { get; set; }
    }
}
