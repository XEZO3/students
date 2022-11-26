using Microsoft.AspNetCore.Identity;

namespace students.Models
{
    public class userAuth : IdentityUser
    {
        public string Name { get; set; }
        public string Major { get; set; }
        public string NationalID { get; set; }
        public ICollection<Courses> Courses { get; set; }
    }
}
