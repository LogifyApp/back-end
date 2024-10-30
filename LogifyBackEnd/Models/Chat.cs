using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Chat
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public int EmployerUserId { get; set; }

    public int DriverUserId { get; set; }

    public virtual Driver DriverUser { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Employer EmployerUser { get; set; } = null!;
}
