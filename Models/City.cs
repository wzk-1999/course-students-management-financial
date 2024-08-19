using System;
using System.Collections.Generic;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ProvinceId { get; set; }

    public virtual Province Province { get; set; } = null!;

    // Navigation property to represent the relationship with students
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
