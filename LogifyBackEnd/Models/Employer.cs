using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Employer
{
    public int UserId { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<EmployerDriverHistory> EmployerDriverHistories { get; set; } = new List<EmployerDriverHistory>();

    public virtual User User { get; set; } = null!;
}
