﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSchool.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}