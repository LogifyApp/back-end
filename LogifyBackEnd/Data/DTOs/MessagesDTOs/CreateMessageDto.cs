namespace LogifyBackEnd.Data.DTOs.MessagesDTOs;

public class CreateMessageDto
{
    public string Content { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
}