using System.ComponentModel.DataAnnotations.Schema;

namespace students.Models
{
    public class UserCourses:Main

    {
        [ForeignKey("userAuth")]
        public string UserId { get; set; }
        public userAuth userAuth { get; set; }
        [ForeignKey("courses")]
        public int CoursesId { get; set; }
        public Courses courses { get; set; }
    }
}
