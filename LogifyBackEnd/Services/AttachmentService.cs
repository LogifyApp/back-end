using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;

namespace LogifyBackEnd.Services;

public class AttachmentService : IAttachmentService
{
    private readonly DBContext _context;

    public AttachmentService(DBContext context)
    {
        _context = context;
    }

    public async Task<AttachmentDto> AddAttachment(CreateAttachmentDto createAttachmentDto)
    {
        var attachment = new Attachment
        {
            MessageId = createAttachmentDto.MessageId,
            DocumentId = createAttachmentDto.DocumentId
        };

        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync();

        return new AttachmentDto
        {
            Id = attachment.Id,
            MessageId = attachment.MessageId,
            DocumentId = attachment.DocumentId
        };
    }

    public async Task<bool> DeleteAttachment(int attachmentId)
    {
        var attachment = await _context.Attachments.FindAsync(attachmentId);
        if (attachment == null) return false;

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();
        return true;
    }
}