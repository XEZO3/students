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
        //public DbSet<Cart> Cart { get; set; }
        //public DbSet<CartItems> cartItems { get; set; }
        //public DbSet<Order> Order { get; set; }
        //public DbSet<OrderItem> orderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //DESKTOP-JD76U9C
            //LAPTOP-BFFJ9SQ9
            builder.UseSqlServer("Server=LAPTOP-BFFJ9SQ9;Database=Students;Trusted_Connection=True");
        }
    }
}
