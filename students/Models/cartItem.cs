using System.ComponentModel.DataAnnotations.Schema;

namespace students.Models
{
    public class cartItem:Main
    {
        [ForeignKey("cart")]
        public int cartId { get; set; }
        public cart cart { get; set; }
        [ForeignKey("Courses")]
        public int CoursesId { get; set; }
        public Courses Courses { get; set; }

    }
}
