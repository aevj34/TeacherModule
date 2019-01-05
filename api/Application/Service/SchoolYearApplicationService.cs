
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
 namespace api.Application.Service
{
 public class SchoolYearApplicationService: BaseApplication
 {

    private readonly SchoolYearRepository schoolYearRepository;

    public SchoolYearApplicationService(SchoolYearRepository schoolYearRepository) : base()
    {
    this.schoolYearRepository = schoolYearRepository;
    }





    }
}

 
