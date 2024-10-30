using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.ChatsDTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IChatService
{
    Task<ChatDto> CreateChat(int ownerUserId, int driverUserId);
    Task<bool> DeleteChat(int chatId);
}