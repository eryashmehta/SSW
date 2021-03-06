﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public class Instructor : BaseEntity
    {
        public User User { get; set; }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
