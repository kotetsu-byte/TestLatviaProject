using AutoMapper;
using TestLatviaProject.Dtos;
using TestLatviaProject.Models;

namespace TestLatviaProject.Helpers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<UserAdmin, UserAdminDto>();
            CreateMap<UserAdminDto, UserAdmin>();
            CreateMap<Tasks, TasksDto>();
            CreateMap<TasksDto, Tasks>();
        }
    }
}
