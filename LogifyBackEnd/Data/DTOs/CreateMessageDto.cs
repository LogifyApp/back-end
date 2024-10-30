namespace LogifyBackEnd.Data.DTOs;

public class CreateMessageDto
{
    public string Content { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
}