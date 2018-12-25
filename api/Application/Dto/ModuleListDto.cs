using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class ModuleListDto
    {
        public Int32 moduleID { get; set; }
        public String name { get; set; }
        public Boolean active { get; set; }
        public String school_name { get; set; }
        public List<RoleInActionListDto> actions { get; set; }
        public List<ActionListDto> actionsInsert { get; set; }
    }
}
