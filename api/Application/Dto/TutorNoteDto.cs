using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class TutorNoteDto
    {
        public string note { get; set; }
        public Int32 noteID { get; set; }
        public Int32 courseID { get; set; }
        public Int32 schoolID { get; set; }
        public Int32 schoolYearID { get; set; }
        public Int32 evaluationFormulaID { get; set; }
        public Int32 evaluationID { get; set; }
        public Boolean isAverage { get; set; }
        public Boolean isApproved { get; set; }
    }
}
