namespace LogifyBackEnd.Models;

public class Car
{
    public string Number { get; set; } // Primary Key
    public string Model { get; set; }
    public string Brand { get; set; }
    public bool Status { get; set; }
    public bool IsDeleted { get; set; }
    public int EmployerUserId { get; set; } // Foreign Key to Employer
    public Employer Employer { get; set; }
}
