using System;
using System.Collections.Generic;

namespace EnlivenDX_Evaluation.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string DepartmentLocation { get; set; } = null!;

    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();
}
