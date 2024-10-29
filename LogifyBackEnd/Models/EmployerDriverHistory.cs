using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class EmployerDriverHistory
{
    public int Id { get; set; }

    public int EmployerUserId { get; set; }

    public int DriverUserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Driver DriverUser { get; set; } = null!;

    public virtual Employer EmployerUser { get; set; } = null!;
}
