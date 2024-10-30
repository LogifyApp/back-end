namespace LogifyBackEnd.Data.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
}