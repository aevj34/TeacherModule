using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class MenuListDto
    {
        public Int32 menuID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public Int32 orderMenu { get; set; }
        public Boolean active { get; set; }
        public List<ActionListDto> actions { get; set; }
        public List<ModuleListDto> modules { get; set; }
        public String school_name { get; set; }

    }
}
