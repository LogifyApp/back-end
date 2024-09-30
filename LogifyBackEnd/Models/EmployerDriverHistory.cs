namespace LogifyBackEnd.Models;

public class EmployerDriverHistory
{
    public int Id { get; set; } // Primary Key
    public int EmployerUserId { get; set; } // Foreign Key to Employer
    public int DriverUserId { get; set; } // Foreign Key to Driver
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Employer Employer { get; set; }
    public Driver Driver { get; set; }
}
