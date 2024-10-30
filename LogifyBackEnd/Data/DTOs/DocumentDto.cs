namespace LogifyBackEnd.Data.DTOs;

public class DocumentDto
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string Filetype { get; set; }
    public string FileUrl { get; set; }
    public byte[]? Content { get; set; }  // For retrieval, only
    public DateTime UploadedAt { get; set; }
}