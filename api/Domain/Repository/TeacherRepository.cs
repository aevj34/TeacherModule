using api.Application.Dto;
using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Repository
{
    public interface TeacherRepository
    {

      Teacher GetByDni(String Dni);
      TeacherDto GetByTeacherID(int UserID);

  }
}
