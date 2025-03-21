using System;
using System.Collections.Generic;

namespace EnlivenDX_Evaluation.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpFirstName { get; set; } = null!;

    public string? EmpLastName { get; set; }

    public string? EmpTechSkills { get; set; }

    public DateOnly? EmpDateOfJoining { get; set; }

    public int? EmpDesignation { get; set; }

    public int? EmpMgrId { get; set; }

    public int EmpSalary { get; set; }

    public virtual Designation? EmpDesignationNavigation { get; set; }

    public virtual Employee? EmpMgr { get; set; }

    public virtual EmployeePersonalDetail? EmployeePersonalDetail { get; set; }

    public virtual ICollection<Employee> InverseEmpMgr { get; set; } = new List<Employee>();
}
