using api.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Entity
{
    public class Students
    {
        public Int32 studentID { get; set; }
        public Int32 enrollmentDetailID { get; set; }
        public Int32 enrollmentID { get; set; }
        public Int32 schoolID { get; set; }
        public Int32 programmingID { get; set; }
        public String student_code { get; set; }
        public String student_name { get; set; }
        public String student_firstName { get; set; }
        public String student_lastName { get; set; }
        public Boolean active { get; set; }
        public String EndPoint { get; set; }
        public String ImageKey { get; set; }
        public List<NoteListDto> notes { get; set; }

    }
}
