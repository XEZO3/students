using System.ComponentModel;

using System.ComponentModel.DataAnnotations.Schema;

namespace students.Models
{
    public class Courses :Main
    {
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Name_en { get; set; }
        [ForeignKey("Category")]
        [DisplayName("Category")]

        public int CategoryId { get; set; }
        public string Description { get; set; }
        //in the month
        public int Duration { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public Category category { get; set; }
        [ForeignKey("userAuth")]
        public string? CreaterId { get; set; }
        public userAuth userAuth { get; set; }
    }
}
