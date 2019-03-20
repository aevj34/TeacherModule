using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class LessonDto
    {
        public Int32 assistanceID { get; set; }
        public String classTheme { get; set; }
        public String dateClassShow { get; set; }
        public Int32 programmingID { get; set; }
        public Int32 schoolID { get; set; }
    }
}
