using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestLatviaProject.Models
{
    public class UserAdmin
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public User? User { get; set; }
        public List<string>? TasksTitle { get; set; }
        public IEnumerable<Tasks>? AllTasks { get; set; }
    }
}
