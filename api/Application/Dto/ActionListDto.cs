using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class ActionListDto
    {
        public Int32 actionID { get; set; }
        public String name { get; set; }
        public String entryURL { get; set; }
        public String imageName { get; set; }
        public Boolean active { get; set; }
        public Int32 orderNumber { get; set; }
        public string module_name { get; set; }
        public String menu_name { get; set; }
        public String school_name { get; set; }
    }
}
