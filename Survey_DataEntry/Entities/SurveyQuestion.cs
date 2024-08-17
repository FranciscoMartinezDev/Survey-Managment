using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_DataEntry.Entities
{
    public class SurveyQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSurveyQuestion { get; set; }
        public string? Question { get; set; }
        public string? QuestionType { get; set; }
        public int SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey? Survey { get; set; }
        public ICollection<QuestionOption>? Options { get; set; }
    }
}
