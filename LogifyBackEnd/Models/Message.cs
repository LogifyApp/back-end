using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int UserId { get; set; }

    public int ChatId { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual Chat Chat { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
