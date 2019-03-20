using api.Application.Dto;
using api.Application.NotificationPattern;
using api.Common.Infrastructure.Security;
using api.Domain.Entity;
using api.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Service
{

    public class TeacherLoginApplicationService : BaseApplication
    {

        private readonly TeacherRepository teacherRepository;
        private readonly RoleInActionRepository roleInActionRepository;
        private JwtTokenProvider jwtTokenProvider;

        public TeacherLoginApplicationService(TeacherRepository teacherRepository, RoleInActionRepository roleInActionRepository) : base()
        {
            this.teacherRepository = teacherRepository;
            this.roleInActionRepository = roleInActionRepository;
            this.jwtTokenProvider = new JwtTokenProvider();
        }

        public Object validateTeacher(TeacherDto teacherDto)
        {

            BaseResponseDto<UserAuthDto> baseResponseDto = new BaseResponseDto<UserAuthDto>();
            Notification notification = new Notification();

            Teacher autTheacher = null;
            autTheacher = this.teacherRepository.GetByDni(teacherDto.Dni,teacherDto.SchoolID);

            if (autTheacher.Dni == null)
            {
                notification.addError("El DNI: " + teacherDto.Dni + " no existe o aún no está registrado");
                return this.getApplicationErrorResponse(notification.getErrors());
            }

            if (!Hashing.CheckMatch(autTheacher.password, teacherDto.Password))
            {
                notification.addError("La contraseña es incorrecta");
                return this.getApplicationErrorResponse(notification.getErrors());
            }

            UserAuthDto userAuthDto = null;
            userAuthDto = this.buildUserAuthDto(autTheacher);

            List<UserAuthDto> usersAuthDto = new List<UserAuthDto>();
            usersAuthDto.Add(userAuthDto);

            baseResponseDto.Data = usersAuthDto;
            return baseResponseDto;

        }


        private UserAuthDto buildUserAuthDto(Teacher authTeacher)
        {
            UserAuthDto userAuthDto = new UserAuthDto();
            userAuthDto.id = authTeacher.teacherID;
            userAuthDto.name = authTeacher.Dni;
            userAuthDto.isAuthenticated = true;
            userAuthDto.roleID = authTeacher.roleID;
            userAuthDto.schoolID = authTeacher.schoolID;
            userAuthDto.fullName = authTeacher.name;
            userAuthDto.shortName = authTeacher.shortName;
            userAuthDto.bearerToken = jwtTokenProvider.BuildJwtToken(userAuthDto);

            return userAuthDto;
        }


        public List<MenuListDto> getMenus(string dni, int schoolID)

        {
            Teacher teacher = new Teacher();
            teacher = this.teacherRepository.GetByDni(dni, schoolID);

            int roleID = teacher.roleID;
            bool active = true;

            List<RoleInActionListDto> actionsxRole = this.roleInActionRepository.GetByroleIDByschoolIDByactive(roleID, schoolID, active);

            IEnumerable<String> menusString = actionsxRole.Select(x => x.menu_name).Distinct();
            //IEnumerable<String> iconsString = actionsxRole.Select(x => x.menu_description).Distinct();

            List<MenuListDto> menus = new List<MenuListDto>();

            foreach (String menu in menusString)
            {
                MenuListDto menuDto = new MenuListDto();
                menuDto.name = menu; ;

                string icon = "";
                RoleInActionListDto obj = new RoleInActionListDto();
                obj = actionsxRole.Where(x => x.menu_name == menu).FirstOrDefault();
                if ( obj != null)
                {
                    icon = obj.menu_description;
                }

                menuDto.description = icon;
                menuDto.actions = actionsxRole.Where(e => e.menu_name == menu).ToList();
                menus.Add(menuDto);
            }
            return menus;
        }


    }
}
