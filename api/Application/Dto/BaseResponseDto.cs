using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class BaseResponseDto<T>
    {
     public List<T> Data { get; set; }
     public T single { get; set; }
    public T singleLeft { get; set; }
    public int totalRows { get; set; }
     public int totalPages { get; set; }
     public int totalFooters { get; set; }
     public int[] pages { get; set; }
     public bool leftFooter { get; set; }
     public bool rightFooter { get; set; }

     public int firstSchoolID { get; set; }
     public int firstCategoryRolID { get; set; }
     public int firstModuleID { get; set; }
     public int firstMenuID { get; set; }

    }
}
