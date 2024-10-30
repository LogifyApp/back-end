using LogifyBackEnd.Data.DTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IChatService
{
    Task<ChatDto> CreateChat(int ownerUserId, int driverUserId);
    Task<bool> DeleteChat(int chatId);
}