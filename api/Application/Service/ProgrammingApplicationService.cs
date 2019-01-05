
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
 namespace api.Application.Service
{
 public class ProgrammingApplicationService: BaseApplication
 {

 private readonly ProgrammingRepository programmingRepository;
 public ProgrammingApplicationService(ProgrammingRepository programmingRepository) : base()
 {
 this.programmingRepository = programmingRepository;
 }


        

 }
 }

 
