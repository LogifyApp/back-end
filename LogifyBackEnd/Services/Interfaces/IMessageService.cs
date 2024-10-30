using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.MessagesDTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IMessageService
{
    Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto);
    Task<bool> DeleteMessage(int messageId);
    Task<bool> UpdateMessageContent(int messageId, UpdateMessageDto updateMessageDto);
}