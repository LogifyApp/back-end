namespace LogifyBackEnd.Models;

public class Employer
{
    public int UserId { get; set; } // Foreign Key to User
    public User User { get; set; }
}
