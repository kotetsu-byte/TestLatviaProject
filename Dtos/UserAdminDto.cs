using Microsoft.AspNetCore.Mvc.Rendering;
using TestLatviaProject.Models;

namespace TestLatviaProject.Dtos
{
    public class UserAdminDto
    {
        public int? Id { get; set; }
        public IEnumerable<SelectListItem>? UsersList { get; set; }
        public string? UserName { get; set; }
        public IEnumerable<Tasks>? AllTasks { get; set; }
        public IEnumerable<string>? TasksTitle { get; set; }
    }
}
