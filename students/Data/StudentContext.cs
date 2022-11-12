using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using students.Models;

namespace students.Data
{
    public class StudentContext : IdentityDbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }
        public DbSet<userAuth> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<cart> Cart { get; set; }
        public DbSet<cartItem> cartItem { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }

        public DbSet<UserCourses> CoursesVideo { get; set; }
        
        //public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //DESKTOP-JD76U9C
            //LAPTOP-BFFJ9SQ9
            builder.UseSqlServer("Server=DESKTOP-JD76U9C;Database=Students;Trusted_Connection=True");
        }
        //public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<students.Models.CoursesVideo> CoursesVideo { get; set; }
    }
}
