using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_DataEntry.Entities
{
    public class QuestionOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOption { get; set; }
        public string? Option { get; set; }
        public int QuestionId { get; set; }
        public bool? IsSelected { get; set; }

        [ForeignKey("QuestionId")]
        public virtual SurveyQuestion? Question { get; set; }
    }
}
