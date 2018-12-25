using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class RoleInActionListDto
    {
        public Int32 roleInActionID { get; set; }
        public Int32 actionID { get; set; }
        public Int32 roleID { get; set; }
        public Int32 orderNumber { get; set; }
        public Boolean active { get; set; }
        public String action_name { get; set; }

        public String action_entryURL { get; set; }
        public String action_imageName { get; set; }
        public String action_imageInvert { get; set; }
        public String menu_name { get; set; }
        public String menu_description { get; set; }
        public string module_name { get; set; }

    }
}
