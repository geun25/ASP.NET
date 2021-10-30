using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineSchool.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int CourseID { get; set; } //기본키 속성
        public string Title { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}