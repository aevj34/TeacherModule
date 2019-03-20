using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class SectionDto
    {
        public int sectionID { get; set; }
        public string name { get; set; }
        public List<ProgrammingListDto> courses { get; set; }
    }
}
