using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class ChatService(DBContext context) : IChatService
{
    public async Task<ChatDto> CreateChat(int ownerUserId, int driverUserId)
    {
        // Check if employer and driver exist in the database
        var employerExists = await context.Employers.AnyAsync(e => e.UserId == ownerUserId);
        var driverExists = await context.Drivers.AnyAsync(d => d.UserId == driverUserId);

        if (!employerExists || !driverExists)
            throw new Exception("Either the employer or driver does not exist.");

        // Create a new chat entry
        var chat = new Chat
        {
            StartDate = DateTime.UtcNow,
            EmployerUserId = ownerUserId,
            DriverUserId = driverUserId
        };

        context.Chats.Add(chat);
        await context.SaveChangesAsync();

        // Return the created chat as a DTO
        return new ChatDto
        {
            Id = chat.Id,
            StartDate = chat.StartDate,
            EmployerUserId = chat.EmployerUserId,
            DriverUserId = chat.DriverUserId
        };
    }

    public async Task<bool> DeleteChat(int chatId)
    {
        var chat = await context.Chats.FindAsync(chatId);
        if (chat == null)
            return false;

        context.Chats.Remove(chat);
        await context.SaveChangesAsync();

        return true;
    }
}