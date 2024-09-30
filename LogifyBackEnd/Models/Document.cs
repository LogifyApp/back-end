namespace LogifyBackEnd.Models;

public class Document
{
    public int Id { get; set; } // Primary Key
    public string Filename { get; set; }
    public int MongoId { get; set; }
    public string Filetype { get; set; }
    public string FileURL { get; set; }
}
