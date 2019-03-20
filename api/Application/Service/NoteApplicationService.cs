
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
using api.Domain.Entity;

namespace api.Application.Service
{
 public class NoteApplicationService: BaseApplication
 {
 private readonly NoteRepository noteRepository;

 public NoteApplicationService(NoteRepository noteRepository) : base()
 {
 this.noteRepository = noteRepository;
 }


 public Object Insert(Note noteDto)
 {
 try
 {
 BaseResponseDto<Note > baseResponseDto = new BaseResponseDto<Note>();
 int noteID = this.noteRepository.Insert(noteDto);
 //baseResponseDto.single = this.noteRepository.Obtain(noteID); 
 return baseResponseDto;
 }
 catch (Exception ex)
 {
 return this.getExceptionErrorResponse2(ex.Message);
 }
 }

 }
 }

 
