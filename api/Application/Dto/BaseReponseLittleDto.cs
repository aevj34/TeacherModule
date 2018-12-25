using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{

  public class BaseReponseLittleDto<T>
  {

    public List<T> Data { get; set; }

  }

  }
