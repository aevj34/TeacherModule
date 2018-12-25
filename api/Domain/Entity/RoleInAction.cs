using System;

namespace api.Domain.Entity
{
    public class RoleInAction
    {
            public Int32 roleInActionID { get; set; }
            public Int32 actionID { get; set; }
            public Int32 roleID { get; set; }
            public Int32 orderNumber { get; set; }
            public Boolean active { get; set; }
            public Int32 schoolID { get; set; }
        
    }
}
