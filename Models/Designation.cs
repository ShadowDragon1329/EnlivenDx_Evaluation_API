using System;
using System.Collections.Generic;

namespace EnlivenDX_Evaluation.Models;

public partial class Designation
{
    public int DesignationId { get; set; }

    public string? DesignationName { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
