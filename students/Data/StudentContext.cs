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

        public DbSet<CoursesVideo> CoursesVideo { get; set; }
        
        //public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //DESKTOP-JD76U9C
            //LAPTOP-BFFJ9SQ9
            builder.UseSqlServer("Server=DESKTOP-JD76U9C;Database=Students;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Courses>().HasOne(x => x.userAuth).WithMany(z => z.Courses).HasForeignKey(p=>p.CreaterId);
            builder.Entity<userAuth>().HasMany(x => x.Courses).WithOne(z => z.userAuth).OnDelete(DeleteBehavior.SetNull).HasForeignKey(p => p.CreaterId);
            base.OnModelCreating(builder);
        }
        //public DbSet<OrderItem> orderItems { get; set; }


    }
}
