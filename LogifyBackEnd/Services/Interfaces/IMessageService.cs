using LogifyBackEnd.Data.DTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IMessageService
{
    Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto);
    Task<bool> DeleteMessage(int messageId);
    Task<bool> UpdateMessageContent(int messageId, UpdateMessageDto updateMessageDto);
}