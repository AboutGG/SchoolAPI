using AutoMapper;
using SchoolAPI.DTO;
using SchoolAPI.Models;

namespace SchoolAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Registry, RegistryDTO>();
            CreateMap<RegistryDTO,Registry>();
            CreateMap<TeacherSubject, TeacherSubjectDTO>();
            CreateMap<TeacherSubjectDTO, TeacherSubject>();
            CreateMap<Exam, ExamDTO>();
            CreateMap<ExamDTO, Exam>();
        }
    }
}
