using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Car
{
    public string Number { get; set; } = null!;

    public string? Model { get; set; }

    public string? Brand { get; set; }

    public bool Status { get; set; }

    public bool IsDeleted { get; set; }

    public int EmployerUserId { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual Employer EmployerUser { get; set; } = null!;
}
