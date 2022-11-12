using System.ComponentModel.DataAnnotations.Schema;

namespace students.Models
{
    public class CoursesVideo:Main
    {
        [ForeignKey("Courses")]
        public int CoursesId { get; set; }
        public string title { get; set; }
        public string videoUrl { get; set; }

        public Courses courses { get; set; }
    }
}
