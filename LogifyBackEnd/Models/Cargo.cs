using LogifyBackEnd.Models.Enums;

namespace LogifyBackEnd.Models;

public class Cargo
{
    public int Id { get; set; } // Primary Key
    public CargoStatus Status { get; set; }

    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public string CarId { get; set; } // Foreign Key to Car
    public int DriverUserId { get; set; } // Foreign Key to Driver
    public int EmployerUserId { get; set; } // Foreign Key to Employer
    public Car Car { get; set; }
    public Driver Driver { get; set; }
    public Employer Employer { get; set; }
    
    public List<Point> Points { get; set; }
}
