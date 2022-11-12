using System.ComponentModel.DataAnnotations.Schema;

namespace students.Models
{
    public class cart:Main
    {
        [ForeignKey("userAuth")]
        public string UserId { get; set; }
        public userAuth user { get;set; }
        public List<cartItem> cartItem  { get; set; }
        public cart() { 
        cartItem = new List<cartItem>();
        }
    }
}
