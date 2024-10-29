using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
