namespace LogifyBackEnd.Models;

public class Chat
{
    public int Id { get; set; } // Primary Key
    public DateTime StartDate { get; set; }
    public int OwnerUserId { get; set; } // Foreign Key to User
    public int DriverUserId { get; set; } // Foreign Key to Driver
    public User Owner { get; set; }
    public Driver Driver { get; set; }
}
