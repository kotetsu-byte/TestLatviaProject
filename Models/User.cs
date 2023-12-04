using Microsoft.AspNetCore.Identity;

namespace TestLatviaProject.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public IEnumerable<Tasks>? Tasks { get; set; }
    }
}
