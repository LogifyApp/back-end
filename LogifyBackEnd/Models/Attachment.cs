using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Attachment
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public int DocumentId { get; set; }

    public virtual Document Document { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
