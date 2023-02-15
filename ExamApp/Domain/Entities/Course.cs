using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExamApp.Domain.Entities
{
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual Language Language { get; set; }
    }
}
