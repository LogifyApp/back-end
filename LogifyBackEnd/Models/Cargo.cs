using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Cargo
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Description { get; set; } = null!;

    public string CarId { get; set; } = null!;

    public int DriverUserId { get; set; }

    public int EmployerUserId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<CargoDocument> CargoDocuments { get; set; } = new List<CargoDocument>();

    public virtual Driver DriverUser { get; set; } = null!;

    public virtual Employer EmployerUser { get; set; } = null!;

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();
}
