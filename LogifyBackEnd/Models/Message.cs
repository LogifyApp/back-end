namespace LogifyBackEnd.Models;

public class Message
{
    public int Id { get; set; } // Primary Key
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; } // Foreign Key to User
    public int ChatId { get; set; } // Foreign Key to Chat
    public User User { get; set; }
    public Chat Chat { get; set; }
}
