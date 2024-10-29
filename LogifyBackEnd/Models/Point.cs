using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Point
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public int Latitude { get; set; }

    public int Longitude { get; set; }

    public int Order { get; set; }

    public int CargoId { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;
}
