using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_DataTransfer.Dtos
{
    public class OptionDto
    {
        public int IdOption { get; set; }
        public string? Option { get; set; }
        public int QuestionId { get; set; }
        public bool? IsSelected { get; set; }
    }
}
