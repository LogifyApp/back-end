using LogifyBackEnd.Data.DTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IAttachmentService
{
    Task<AttachmentDto> AddAttachment(CreateAttachmentDto createAttachmentDto);
    Task<bool> DeleteAttachment(int attachmentId);
}