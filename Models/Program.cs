using System;
using System.Collections.Generic;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public partial class Program
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    // Navigation property to represent the relationship with students
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
