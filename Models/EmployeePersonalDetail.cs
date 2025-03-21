using System;
using System.Collections.Generic;

namespace EnlivenDX_Evaluation.Models;

public partial class EmployeePersonalDetail
{
    public int EmpId { get; set; }

    public DateOnly? EmpDateOfBirth { get; set; }

    public string? EmpNationality { get; set; }

    public string? EmpGender { get; set; }

    public string? EmpMaritalStatus { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
