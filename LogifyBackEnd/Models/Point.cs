namespace LogifyBackEnd.Models;

public class Point
{
    public int Id { get; set; } // Primary Key
    public string Label { get; set; }
    public int Latitude { get; set; }
    public int Longitude { get; set; }
    public int Order { get; set; }
    public int CargoId { get; set; } // Foreign Key to Cargo
    public Cargo Cargo { get; set; }
}
