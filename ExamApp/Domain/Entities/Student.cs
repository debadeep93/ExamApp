using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamApp.Domain.Entities
{
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        // One stdent can have multiple courses. Should be a collection. 
    }
}
