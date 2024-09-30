namespace LogifyBackEnd.Models;

public class Attachment
{
    public int Id { get; set; } // Primary Key
    public int MessageId { get; set; } // Foreign Key to Message
    public int DocumentId { get; set; } // Foreign Key to Document
    public Message Message { get; set; }
    public Document Document { get; set; }
    //test
}
