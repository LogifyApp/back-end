using System;
using System.Collections.Generic;
using LogifyBackEnd.Models.Enums;

namespace LogifyBackEnd.Models;

public partial class Driver
{
    public int UserId { get; set; }

    public DriverStatus Status { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<EmployerDriverHistory> EmployerDriverHistories { get; set; } = new List<EmployerDriverHistory>();

    public virtual User User { get; set; } = null!;
}
