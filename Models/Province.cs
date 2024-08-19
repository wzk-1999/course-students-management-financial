using System;
using System.Collections.Generic;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
