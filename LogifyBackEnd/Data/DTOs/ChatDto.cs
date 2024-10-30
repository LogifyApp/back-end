namespace LogifyBackEnd.Data.DTOs;

public class ChatDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public int EmployerUserId { get; set; }
    public int DriverUserId { get; set; }
}