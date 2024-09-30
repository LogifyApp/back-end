namespace LogifyBackEnd.Models;

public class Driver
{
    public int UserId { get; set; } // Foreign Key to User
    public string Status { get; set; }
    public User User { get; set; }
}
