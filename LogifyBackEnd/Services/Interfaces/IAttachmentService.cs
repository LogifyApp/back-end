using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.AttachmentsDTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IAttachmentService
{
    Task<AttachmentDto> AddAttachment(CreateAttachmentDto createAttachmentDto);
    Task<bool> DeleteAttachment(int attachmentId);
}