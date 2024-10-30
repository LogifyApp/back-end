using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;

namespace LogifyBackEnd.Services;

public class MessageService(DBContext context) : IMessageService
{
    public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto)
    {
        var message = new Message
        {
            Content = createMessageDto.Content,
            DateTime = DateTime.Now,
            UserId = createMessageDto.UserId,
            ChatId = createMessageDto.ChatId
        };

        context.Messages.Add(message);
        await context.SaveChangesAsync();

        return new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            DateTime = message.DateTime,
            UserId = message.UserId,
            ChatId = message.ChatId
        };
    }

    public async Task<bool> DeleteMessage(int messageId)
    {
        var message = await context.Messages.FindAsync(messageId);
        if (message == null) return false;

        context.Messages.Remove(message);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateMessageContent(int messageId, UpdateMessageDto updateMessageDto)
    {
        var message = await context.Messages.FindAsync(messageId);
        if (message == null) return false;

        message.Content = updateMessageDto.NewContent;
        await context.SaveChangesAsync();
        return true;
    }
}