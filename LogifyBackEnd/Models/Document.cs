using System;
using System.Collections.Generic;

namespace LogifyBackEnd.Models;

public partial class Document
{
    public int Id { get; set; }

    public string Filename { get; set; } = null!;

    public string MongoId { get; set; } = null!;

    public string Filetype { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<CargoDocument> CargoDocuments { get; set; } = new List<CargoDocument>();
}
