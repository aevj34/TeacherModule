using api.Application.Dto;
using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
  public class FactoryPaginator<T>
  {

    public static void setPaginator(int totalRows, int pageSize, int selectedFooter, int footerSize, BaseResponseDto<T> baseResponseDto)
    {

      baseResponseDto.totalRows = totalRows;
      decimal totalPages=0;
            if (pageSize > 0)
            {
                totalPages = (decimal)(totalRows) / pageSize;
            }
  
      baseResponseDto.totalPages = (int)Math.Ceiling(totalPages);

      decimal totalFooters =0;
      if (footerSize>0)
       totalFooters = (decimal)(totalPages) / footerSize;

      baseResponseDto.totalFooters = (int)Math.Ceiling(totalFooters);

      int firstPage = (selectedFooter - 1) * footerSize + 1;
      int lastPage = (selectedFooter) * footerSize;

      int[] pages = new int[footerSize];
      int j = 0;
      for (int i = firstPage; i <= lastPage; i++)
      {
        if (i <= baseResponseDto.totalPages)
        {
          pages[j] = i;
          j = j + 1;
        }
      }

      baseResponseDto.pages = pages.Where( e => e>0).ToArray();
      baseResponseDto.leftFooter = selectedFooter > 1;
      baseResponseDto.rightFooter = selectedFooter < totalFooters;

    }

  }
}
