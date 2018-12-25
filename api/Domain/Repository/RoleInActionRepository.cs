using api.Application.Dto;
using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Repository
{
    public interface RoleInActionRepository
    {

        List<RoleInActionListDto> GetByRoleIDByactive(Int32 roleID, Boolean active, int schoolID);
        int GetByActionIDByRoleIDCount(Int32 ActionID, Int32 RoleID, int schoolID);
        List<RoleInActionListDto> GetByroleIDByschoolIDByactive(Int32 roleID, Int32 schoolID, Boolean active);

    }
}
