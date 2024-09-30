namespace LogifyBackEnd.Models;

public class CargoDocument
{
    public int Id { get; set; } // Primary Key
    public int CargoId { get; set; } // Foreign Key to Cargo
    public int DocumentId { get; set; } // Foreign Key to Document
    public Cargo Cargo { get; set; }
    public Document Document { get; set; }
}
