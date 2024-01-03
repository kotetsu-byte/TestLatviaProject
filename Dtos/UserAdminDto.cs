using Microsoft.AspNetCore.Mvc.Rendering;
using TestLatviaProject.Models;

namespace TestLatviaProject.Dtos
{
    public class UserAdminDto
    {
        public int? Id { get; set; }
        public ICollection<SelectListItem>? UsersList { get; set; }
        public string? UserName { get; set; }
        public ICollection<Tasks>? AllTasks { get; set; }
        public ICollection<string>? TasksTitle { get; set; }
    }
}
