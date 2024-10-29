using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class CargoDocument
{
    public int Id { get; set; }

    public int CargoId { get; set; }

    public int DocumentId { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
