using Microsoft.AspNetCore.Mvc.Rendering;
using TestLatviaProject.Enums;

namespace TestLatviaProject.Dtos
{
    public class TasksDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Status { get; set; }
        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
}
