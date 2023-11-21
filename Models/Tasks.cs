using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestLatviaProject.Models
{
    public class Tasks
    {
        public int? Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Status { get; set; }
    }
}
